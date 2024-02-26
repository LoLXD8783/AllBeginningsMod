sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

int resolution = 3;
float stepMin = 0.3, stepMax = 0.5;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    int2 pixel = (int2) (coords * uScreenResolution);
    int2 topLeftPixel = pixel - pixel % resolution;

    float4 color = float4(0.0, 0.0, 0.0, 0.0);
    [unroll(6)]
    for (int i = 0; i < resolution; i++)
    {
        [unroll(6)]
        for (int j = 0; j < resolution; j++)
        {
            color += tex2D(uImage0, (float2) (topLeftPixel + int2(i, j)) / uScreenResolution);
        }
    }
    
    
    return float4(color.rgb, smoothstep(stepMin, stepMax, color.a));
}

technique Technique1
{
    pass PixelatePass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
};
