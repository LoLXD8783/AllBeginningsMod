float time;
float2 textureSize;
float4 sourceRectangle;
texture sampleTexture;
sampler2D sampleTextureSampler = sampler_state
{
    texture = <sampleTexture>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

const float PI = 3.14159265;

float4 PixelShaderFunction(float2 coords : TEXCOORD0, float4 color : COLOR0) : COLOR0
{
    float4 sampleColor = tex2D(sampleTextureSampler, coords);
    
    coords = (coords.y * textureSize.y - sourceRectangle.y) / sourceRectangle.w;
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