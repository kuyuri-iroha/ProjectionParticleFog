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
        
        private VisualElement _root;
        private VisualElement _basicModeContainer;
        private VisualElement _advancedModeContainer;

        public override VisualElement CreateInspectorGUI()
        {
            var proceduralFloorFog = target as ProceduralFloorFog;

            _root = inspectorUxml.CloneTree();
            _root.name = "ProceduralFloorFog";
            _root.Bind(serializedObject);
            
            _basicModeContainer = _root.Q<VisualElement>("BasicModeContainer");
            _advancedModeContainer = _root.Q<VisualElement>("AdvancedModeContainer");
            
            // Mode Switcher
            InitModeSwitchButtons(proceduralFloorFog);

            // Min-Max Sliders
            InitMinMaxSlider(_basicModeContainer, "LifeTimeMinMaxSliderWithValue");
            InitMinMaxSlider(_basicModeContainer, "SizeMinMaxSliderWithValue");
            InitMinMaxSlider(_advancedModeContainer, "LifeTimeMinMaxSliderWithValue");
            InitMinMaxSlider(_advancedModeContainer, "SizeMinMaxSliderWithValue");

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

        private void InitModeSwitchButtons(ProceduralFloorFog proceduralFloorFog)
        {
            var basicModeButton = _root.Q<Button>("BasicMode");
            var advancedModeButton = _root.Q<Button>("AdvancedMode");
            _basicModeContainer.style.display = proceduralFloorFog.isAdvanceMode ? DisplayStyle.None : DisplayStyle.Flex;
            _advancedModeContainer.style.display = proceduralFloorFog.isAdvanceMode ? DisplayStyle.Flex : DisplayStyle.None;
            basicModeButton.RegisterCallback<ClickEvent>(evt =>
            {
                if (proceduralFloorFog.isAdvanceMode)
                {
                    _basicModeContainer.style.display = DisplayStyle.Flex;
                    _advancedModeContainer.style.display = DisplayStyle.None;
                    proceduralFloorFog.isAdvanceMode = false;
                }
            });
            advancedModeButton.RegisterCallback<ClickEvent>(evt =>
            {
                if (!proceduralFloorFog.isAdvanceMode)
                {
                    _basicModeContainer.style.display = DisplayStyle.None;
                    _advancedModeContainer.style.display = DisplayStyle.Flex;
                    proceduralFloorFog.isAdvanceMode = true;
                }
            });
        }
    }
}