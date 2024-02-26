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

float radius = 1.0;
float smooth = 0.1;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    
    coords = (coords - 0.5) * 2.0;
    coords.y *= uScreenResolution.y / uScreenResolution.x;
    
    float len = length(coords);
    float grayScaleValue = color.r * 0.299 + color.g * 0.587 + color.b * 0.114;
    float4 grayScaleColor = float4(grayScaleValue, grayScaleValue, grayScaleValue, color.a);

    return lerp(lerp(color, grayScaleColor, smoothstep(radius - smooth, radius, len) * uOpacity), 0.0, smoothstep(0.9, 1.2, len) * uOpacity);
}

technique Technique1
{
    pass NightgauntPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};
