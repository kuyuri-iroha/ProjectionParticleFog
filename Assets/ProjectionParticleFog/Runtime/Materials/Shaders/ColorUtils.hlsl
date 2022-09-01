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

#endif //COLORUTILS_INCLUDE