using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kuyuri.ProjectionParticleFog
{
    [CustomEditor(typeof(ProceduralFloorFog))]
    public class ProceduralFloorFogInspector : Editor
    {
        [SerializeField, HideInInspector] private VisualTreeAsset inspectorUxml;

        private ProceduralFloorFog _proceduralFloorFog;
        private VisualElement _root;
        private VisualElement _basicModeContainer;
        private VisualElement _advancedModeContainer;

        public override VisualElement CreateInspectorGUI()
        {
            _proceduralFloorFog = target as ProceduralFloorFog;

            _root = inspectorUxml.CloneTree();
            _root.name = "ProceduralFloorFog";
            _root.Bind(serializedObject);
            
            _basicModeContainer = _root.Q<VisualElement>("BasicModeContainer");
            _advancedModeContainer = _root.Q<VisualElement>("AdvancedModeContainer");
            
            // Mode Switcher
            InitModeSwitchButtons();

            // Min-Max Sliders
            InitMinMaxSlider(_basicModeContainer, "LifeTimeMinMaxSliderWithValue");
            InitMinMaxSlider(_basicModeContainer, "SizeMinMaxSliderWithValue");
            InitMinMaxSlider(_advancedModeContainer, "LifeTimeMinMaxSliderWithValue");
            InitMinMaxSlider(_advancedModeContainer, "SizeMinMaxSliderWithValue");
            
            // Basic Mode
            InitBasicMode();

            return _root;
        }

        private void InitMinMaxSlider(VisualElement root, string sliderContainerName)
        {
            var minMaxContainer = root.Q<VisualElement>(sliderContainerName);
            var minMaxSlider = minMaxContainer.Q<MinMaxSlider>("MinMaxSlider");
            var minLimit = minMaxContainer.Q<FloatField>("MinLimit");
            var maxLimit = minMaxContainer.Q<FloatField>("MaxLimit");
            minMaxSlider.RegisterCallback<GeometryChangedEvent>(evt =>
            {
                minMaxSlider.lowLimit = minLimit.value;
                minMaxSlider.highLimit = maxLimit.value;
            });
            minLimit.RegisterCallback<BlurEvent>(evt =>
            {
                minMaxSlider.lowLimit = minLimit.value;
            });
            maxLimit.RegisterCallback<BlurEvent>(evt =>
            {
                minMaxSlider.highLimit = maxLimit.value;
            });
        }

        private void InitModeSwitchButtons()
        {
            var basicModeButton = _root.Q<Button>("BasicMode");
            var advancedModeButton = _root.Q<Button>("AdvancedMode");
            _basicModeContainer.style.display = _proceduralFloorFog.isAdvanceMode ? DisplayStyle.None : DisplayStyle.Flex;
            _advancedModeContainer.style.display = _proceduralFloorFog.isAdvanceMode ? DisplayStyle.Flex : DisplayStyle.None;
            basicModeButton.RegisterCallback<ClickEvent>(evt =>
            {
                if (_proceduralFloorFog.isAdvanceMode)
                {
                    _basicModeContainer.style.display = DisplayStyle.Flex;
                    _advancedModeContainer.style.display = DisplayStyle.None;
                    _proceduralFloorFog.isAdvanceMode = false;
                }
            });
            advancedModeButton.RegisterCallback<ClickEvent>(evt =>
            {
                if (!_proceduralFloorFog.isAdvanceMode)
                {
                    _basicModeContainer.style.display = DisplayStyle.None;
                    _advancedModeContainer.style.display = DisplayStyle.Flex;
                    _proceduralFloorFog.isAdvanceMode = true;
                }
            });
        }

        private void InitBasicMode()
        {
            var densityValueSliderContainer = _root.Q<VisualElement>("DensityValueSliderContainer");
            var roughnessValueSliderContainer = _root.Q<VisualElement>("RoughnessValueSliderContainer");
            var scaleValueSliderContainer = _root.Q<VisualElement>("ScaleValueSliderContainer");
            var noiseFlowValue = _root.Q<Vector2Field>("NoiseFlowValue");

            const string sliderName = "Slider";
            const string valueName = "Value";
            
            var densitySlider = densityValueSliderContainer.Q<Slider>(sliderName);
            var densityValue = densityValueSliderContainer.Q<FloatField>(valueName);
            densitySlider.RegisterValueChangedCallback(evt => ApplyDensity(evt.newValue));
            densityValue.RegisterValueChangedCallback(evt => ApplyDensity(evt.newValue));

            var roughnessSlider = roughnessValueSliderContainer.Q<Slider>(sliderName);
            var roughnessValue = roughnessValueSliderContainer.Q<FloatField>(valueName);
            roughnessSlider.RegisterValueChangedCallback(evt => ApplyRoughness(evt.newValue));
            roughnessValue.RegisterValueChangedCallback(evt => ApplyRoughness(evt.newValue));

            var scaleSlider = scaleValueSliderContainer.Q<Slider>(sliderName);
            var scaleValue = scaleValueSliderContainer.Q<FloatField>(valueName);
            scaleSlider.RegisterValueChangedCallback(evt => ApplyScale(evt.newValue));
            scaleValue.RegisterValueChangedCallback(evt => ApplyScale(evt.newValue));

            noiseFlowValue.RegisterValueChangedCallback(evt => ApplyNoiseFlowValue(evt.newValue));
        }

        private void ApplyDensity(float value)
        {
            _proceduralFloorFog.simpleNoiseAmount = Mathf.Lerp(0.0f, 1.0f, value);
            _proceduralFloorFog.combinedNoiseRemap = Mathf.Lerp(0.0f, 0.9f, value);
        }

        private void ApplyRoughness(float value)
        {
            _proceduralFloorFog.simplexNoiseScale = Mathf.Lerp(0.0f, 1000.0f, value);
            _proceduralFloorFog.simplexNoiseAmount = Mathf.Lerp(0.0f, 0.3f, value);
        }

        private void ApplyScale(float value)
        {
            _proceduralFloorFog.simpleNoiseScale = Mathf.Lerp(0.0f, 100.0f, value);
        }

        private void ApplyNoiseFlowValue(Vector2 value)
        {
            _proceduralFloorFog.simpleNoiseAnimation = new Vector4(value.x, value.y,
                _proceduralFloorFog.simpleNoiseAnimation.z, _proceduralFloorFog.simpleNoiseAnimation.w);
        }
    }
}