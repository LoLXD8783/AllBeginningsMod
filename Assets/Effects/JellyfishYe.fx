sampler uImage0 : register(s0);
float4 uSourceRect;
float2 uImageSize0;

float time;

const float PI = 3.14159265;

float4 PixelShaderFunction(float2 coords : TEXCOORD0, float4 color : COLOR0) : COLOR0
{
    float4 sampleColor = tex2D(uImage0, coords);
    
    coords = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    float multiplier = abs(sin(PI * coords.y + time));
    
    return float4(sampleColor.rgb * color.rgb * multiplier, sampleColor.a * color.a);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};