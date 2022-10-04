using UnityEngine;

namespace Kuyuri.ProjectionParticleFog
{
    public class ProceduralFloorFog : MonoBehaviour
    {
        public bool isAdvanceMode = false;
        
        public float rate = 40;
        public Vector2 lifeTimeLimit = new Vector2(0, 20);
        public Vector2 lifeTimeMinMax = new Vector2(3, 3);
        public Vector2 sizeLimit = new Vector2(0, 10);
        public Vector2 sizeMinMax = new Vector2(3, 3);
        public Texture2D beamLightMap;
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

        public float noiseDensity;
        public float noiseRoughness;
        public float noiseScale;
        public Vector2 noiseFlowValue;
    }
}
