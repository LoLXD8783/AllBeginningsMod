sampler uImage0 : register(s0);

float4 uColor;
float uIntensity;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 overlay = lerp(color, uColor, uIntensity);
    
    if (any(color))
    {
        return overlay;
    }
    
    return color;
}

technique Technique1
{
    pass ColorOverlayPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}