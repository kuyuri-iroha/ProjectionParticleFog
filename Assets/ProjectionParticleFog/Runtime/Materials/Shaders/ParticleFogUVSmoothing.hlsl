#ifndef THINKER_PARTICLEFOG_PARTICLEFOGUVSMOOTHING_INCLUDED
#define THINKER_PARTICLEFOG_PARTICLEFOGUVSMOOTHING_INCLUDED

void SmoothingPosition_float(float3 BasePosition, float2 UV, float Size, float2 Coefficient, float PlaneMode, out float3 Smoothed)
{
    Smoothed = BasePosition;
    float2 uv = (UV * float2(2.0f, 2.0f) - float2(1.0f, 1.0f)) * Size * Coefficient;
    if(PlaneMode < 1)
    {
        Smoothed += float3(uv.x, 0, uv.y);
    }
    else if(PlaneMode < 2)
    {
        Smoothed += float3(uv.x, uv.y, 0);
    }
}

void FloorFade_float(float3 BasePosition, float2 UV, float Size, float3 CameraUp, float FloorHeight, float Ratio, out float FadeValue)
{
    float2 uv = UV * float2(2.0f, 2.0f) - float2(1.0f, 1.0f);
    float3 position = BasePosition;
    position += CameraUp * uv.y * Size;
    FadeValue = saturate((position.y - FloorHeight) * Ratio);
}

#endif