using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.VFX;

namespace Kuyuri.ProjectionParticleFog
{
    
    [ExecuteAlways]
    public class ProceduralFloorFog : MonoBehaviour
    {
        public bool isAdvanceMode = false;

        public uint rate;
        public Vector2 lifeTimeMinMax;
        public Vector2 sizeMinMax;
        public Texture beamLightMap;
        public float intensity;
        public bool enableUvCorrection;
        public Color lowCutThreshold;
        public Color baseColor;
        public float baseIntensity;
        public AnimationCurve ageAlphaCurve;
        public Vector4 simpleNoiseAnimation;
        public float simpleNoiseScale;
        public float simpleNoiseAmount;
        public float simpleNoiseRemap;
        public Vector4 simplexNoiseAnimation;
        public float simplexNoiseScale;
        public float simplexNoiseAmount;
        public float simplexNoiseRemap;
        public Vector4 voronoiAnimation; 
        public float voronoiScale;
        public float voronoiAmount;
        public float voronoiRemap;
        public float combinedNoiseRemap;
        public float floorHeight;
        public float floorFadeRatio;
        public float fadeSizeRatio;
        public float cameraFadeOffset;
        public float cameraFadeRange;
        
        public Vector2 lifeTimeLimit = new Vector2(0, 20);
        public Vector2 sizeLimit = new Vector2(0, 10);
        public float noiseDensity;
        public float noiseRoughness;
        public float noiseScale;
        public Vector2 noiseFlowValue;

        private VisualEffect _visualEffect;
        private readonly List<VFXExposedProperty> _vfxExposedProperty = new List<VFXExposedProperty>();
        private FieldInfo[] _fieldInfos;

        private void InstantiateVFX()
        {
            var visualEffect = transform.GetComponentInChildren<VisualEffect>();
            if (visualEffect != null)
            {
                _visualEffect = visualEffect;
                _visualEffect.visualEffectAsset.GetExposedProperties(_vfxExposedProperty);
                return;
            }

            var vfx = Resources.Load<VisualEffectAsset>("ProceduralFloorFog");
            var vfxObj = new GameObject("ProceduralFloorFogVFX");
            visualEffect = vfxObj.AddComponent<VisualEffect>();
            visualEffect.visualEffectAsset = vfx;
            _visualEffect = visualEffect;

            vfxObj.transform.SetParent(transform, false);
            
            _visualEffect.visualEffectAsset.GetExposedProperties(_vfxExposedProperty);
        }

        private void GetInitialProperties()
        {
            foreach (var exposedProperty in _vfxExposedProperty)
            {
                foreach (var fieldInfo in _fieldInfos)
                {
                    if (!string.Equals(exposedProperty.name.ToLower(), fieldInfo.Name.ToLower())) continue;
                    
                    object initialValue = null;
                    switch (exposedProperty.type.ToString())
                    {
                        case "System.UInt32":
                            initialValue = _visualEffect.GetUInt(exposedProperty.name);
                            break;
                            
                        case "System.Single":
                            initialValue = _visualEffect.GetFloat(exposedProperty.name);
                            break;
                            
                        case "UnityEngine.Vector2":
                            initialValue = _visualEffect.GetVector2(exposedProperty.name);
                            break;
                            
                        case "UnityEngine.Texture":
                            initialValue = _visualEffect.GetTexture(exposedProperty.name);
                            break;
                            
                        case "System.Boolean":
                            initialValue = _visualEffect.GetBool(exposedProperty.name);
                            break;
                            
                        case "UnityEngine.AnimationCurve":
                            initialValue = _visualEffect.GetAnimationCurve(exposedProperty.name);
                            break;
                            
                        case "UnityEngine.Vector4":
                            initialValue = _visualEffect.GetVector4(exposedProperty.name);
                            
                            if (fieldInfo.FieldType.ToString() == "UnityEngine.Color")
                            {
                                initialValue = (Color)_visualEffect.GetVector4(exposedProperty.name);
                            }
                            break;
                            
                        default:
                            Debug.LogError($"Unknown type property: {exposedProperty.name}, {exposedProperty.type}");
                            break;
                    }

                    if (initialValue != null)
                    {
                        fieldInfo.SetValue(this, initialValue);
                    }
                    
                }
            }
        }

        private void SetProperties()
        {
            foreach (var exposedProperty in _vfxExposedProperty)
            {
                foreach (var fieldInfo in _fieldInfos)
                {
                    if (!string.Equals(exposedProperty.name.ToLower(), fieldInfo.Name.ToLower())) continue;
                    
                    var value = fieldInfo.GetValue(this);
                    switch (exposedProperty.type.ToString())
                    {
                        case "System.UInt32":
                            _visualEffect.SetUInt(exposedProperty.name, (uint)value);
                            break;
                            
                        case "System.Single":
                            _visualEffect.SetFloat(exposedProperty.name, (float)value);
                            break;
                            
                        case "UnityEngine.Vector2":
                            _visualEffect.SetVector2(exposedProperty.name, (Vector2)value);
                            break;
                            
                        case "UnityEngine.Texture":
                            _visualEffect.SetTexture(exposedProperty.name, (Texture)value);
                            break;
                            
                        case "System.Boolean":
                            _visualEffect.SetBool(exposedProperty.name, (bool)value);
                            break;
                            
                        case "UnityEngine.AnimationCurve":
                            _visualEffect.SetAnimationCurve(exposedProperty.name, (AnimationCurve)value);
                            break;
                            
                        case "UnityEngine.Vector4":
                            if (fieldInfo.FieldType.ToString() == "UnityEngine.Color")
                            {
                                _visualEffect.SetVector4(exposedProperty.name, (Color)value);
                            }
                            else
                            {
                                _visualEffect.SetVector4(exposedProperty.name, (Vector4)value);
                            }
                            break;
                            
                        default:
                            Debug.LogError($"Unknown type property: {exposedProperty.name}, {exposedProperty.type}");
                            break;
                    }
                    
                }
            }
        }

        private void Start()
        {
            _fieldInfos = typeof(ProceduralFloorFog).GetFields(BindingFlags.Public | BindingFlags.Instance);
            InstantiateVFX();
#if UNITY_EDITOR
            GetInitialProperties();
#endif
        }

        public void Update()
        {
            SetProperties();
        }
    }
}
