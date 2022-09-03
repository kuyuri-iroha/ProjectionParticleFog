//UNITY_SHADER_NO_UPGRADE
#ifndef COLORUTILS_INCLUDE
#define COLORUTILS_INCLUDE

void ChangeColorIntensity_float(float3 color, float intensity, out float3 newColor)
{
    newColor = color;
    for(int i = 0; i < floor(intensity); i++)
    {
        newColor += color;
    }
    newColor += color * (intensity - floor(intensity));
}

void ColorLowCutFilter_float(float4 color, float4 threshold, out float4 newColor)
{
    for(int i = 0; i < 4; i++)
    {
        newColor[i] = color[i] < threshold[i] ? 0.0f : color[i];
    }
}

#endif //COLORUTILS_INCLUDE