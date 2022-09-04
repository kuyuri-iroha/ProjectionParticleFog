using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

[ExecuteAlways]
public class ParticleFogProjectionManager : MonoBehaviour
{
    [SerializeField] private List<VisualEffect> projectionFogs = new List<VisualEffect>();
    [SerializeField] private Camera beamCylinderCamera;

    [SerializeField] private float size = 6.5f;
    [SerializeField] private float depth = 13;
    [SerializeField, Range(0, 30)] private int blurIteration = 3;
    
    private RenderTexture[] renderTextures = new RenderTexture[30];

    private readonly List<string> beforeLayerMasks = new List<string>();

    private bool isParameterErrorOcurred = true;
    
    private void Update()
    {
        if (isParameterErrorOcurred) return;
        
        if (this.beamCylinderCamera != null)
        {
            this.beamCylinderCamera.Render();
            var targetTexture = this.beamCylinderCamera.targetTexture;
            var source = RenderTexture.GetTemporary(targetTexture.width, targetTexture.height, 0, targetTexture.format);
            Graphics.Blit(targetTexture, source);
            Blur(source, targetTexture);
            RenderTexture.ReleaseTemporary(source);
        }

        if (this.projectionFogs != null && Camera.main != null)
        {
            var cameraTransform = Camera.main.transform;
            var projectionCamera = this.beamCylinderCamera.transform;

            var upCoefficient = Mathf.Abs(Vector3.Dot(cameraTransform.up, projectionCamera.up));
            var rightCoefficient = Mathf.Abs(Vector3.Dot(cameraTransform.right, projectionCamera.right));
            
            foreach (var projectionFog in this.projectionFogs.Where(projectionFog => projectionFog != null))
            {
                projectionFog.SetVector2("InterpolationCoefficient", new Vector2(upCoefficient, rightCoefficient));
                projectionFog.SetVector3("CameraUp", cameraTransform.up);
            }
        }
    }

    private void OnValidate()
    {
        // プロパティのエラーチェック
        isParameterErrorOcurred = false;
        foreach (var projectionFog in this.projectionFogs.Where(projectionFog => projectionFog != null))
        {
            var properties = new List<VFXExposedProperty>();
            projectionFog.visualEffectAsset.GetExposedProperties(properties);
            
            if (!properties.Exists(x => x.name == "InterpolationCoefficient"))
            {
                Debug.LogError("InterpolationCoefficient is not found in " + projectionFog.name);
                isParameterErrorOcurred = true;
            }
            if (!properties.Exists(x => x.name == "CameraUp"))
            {
                Debug.LogError("CameraUp is not found in " + projectionFog.name);
                isParameterErrorOcurred = true;
            }
            if (!properties.Exists(x => x.name == "BoundaryBox_size"))
            {
                Debug.LogError("BoundaryBox_size is not found in " + projectionFog.name);
                isParameterErrorOcurred = true;
            }
        }
        
        if(isParameterErrorOcurred) return;
        if (this.projectionFogs != null && this.beamCylinderCamera != null)
        {
            this.beamCylinderCamera.orthographicSize = size;
            this.beamCylinderCamera.farClipPlane = depth;
            foreach (var projectionFog in this.projectionFogs.Where(projectionFog => projectionFog != null))
            {
                projectionFog.SetVector3("BoundaryBox_size", new Vector3(size * 2, depth, size * 2));
            }
        }
    }

    private void Blur(RenderTexture source, RenderTexture dest)
    {
        var width = source.width;
        var height = source.height;
        var currentSource = source;

        var i = 0;
        RenderTexture currentDest = null;
        // 段階的にダウンサンプリング
        for (; i < this.blurIteration; i++) {
            width /= 2;
            height /= 2;
            if (width < 2 || height < 2) {
                break;
            }
            currentDest = this.renderTextures[i] = RenderTexture.GetTemporary(width, height, 0, source.format);
            Graphics.Blit(currentSource, currentDest);
            currentSource = currentDest;
        }

        // アップサンプリング
        for (i -= 2; i >= 0; i--) {
            currentDest = this.renderTextures[i];
            Graphics.Blit(currentSource, currentDest);
            this.renderTextures[i] = null;
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDest;
        }

        // 最後にdestにBlit
        Graphics.Blit(currentSource, dest);
        RenderTexture.ReleaseTemporary(currentSource);
    }

    [ContextMenu("ExportToPng")]
    private void ExportToPng()
    {
        RenderTextureToPng(this.beamCylinderCamera.targetTexture, $"ProjectionTexture_{DateTime.Now.ToString("yyyyMMddHHmmss")}");
    }
    
    private void RenderTextureToPng(RenderTexture source, string outputFilename)
    {
        var tex = new Texture2D(source.width, source.height, TextureFormat.RGB24, false);
        RenderTexture.active = source;
        tex.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
        tex.Apply();
        
        var bytes = tex.EncodeToPNG();
        Destroy(tex);
        
        File.WriteAllBytes($"{Application.dataPath}/{outputFilename}.png", bytes);
    }
}

