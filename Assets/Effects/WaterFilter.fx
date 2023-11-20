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

texture noise;
sampler2D noiseSampler = sampler_state
{
    texture = <noise>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

float strength = 0.025;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float2 uv = coords - 0.5;
    float len = length(uv);
    float sampleValue = tex2D(noiseSampler, uv + uTime).r;
    uv = float2(0.5, 0.5) + uv * (1.0 + strength * sampleValue * (1.0 - min(len, 1.0)) * uOpacity);

    return tex2D(uImage0, uv);
}

technique Technique1
{
    pass WaterFilterPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};