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

float maxLen = 0.2;
float maxLenSmooth = 0.3;
float strength = 0.025;
float2 center;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float2 centerNorm = center / uScreenResolution;
    float2 uv = coords - centerNorm;
    float len = length(uv);
    float sampleValue = tex2D(noiseSampler, uv + uTime * 0.005).r;
    uv = centerNorm + uv * (1.0 - min(strength, 1.0) * sampleValue * (1.0 - min(len, 1.0))
        * uOpacity * (1.0 - smoothstep(maxLen, maxLen + maxLenSmooth, len * uScreenResolution.y / uScreenResolution.x)));

    return tex2D(uImage0, uv);
}

technique Technique1
{
    pass WaterPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};
