float time;
bool topHalf;
float holeSize = 0.3;
float smoothening = 0.1;
float intensity = 2.0;
texture sampleTexture1;
sampler2D sampleTextureSampler1 = sampler_state
{
    texture = <sampleTexture1>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

texture sampleTexture2;
sampler2D sampleTextureSampler2 = sampler_state
{
    texture = <sampleTexture2>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

const float PI = 3.14159265;

float4 PixelShaderFunction(float2 coords : TEXCOORD0, float4 color : COLOR0) : COLOR0
{
    float2 uv = coords;
    uv.x = (uv.x - 0.5) * 2.0;
    if (!topHalf)
    {
        uv.y -= 1.0;
    }
    
    float len = length(uv);
    float rotation = atan2(uv.y, uv.x);
    
    float ringClamp = smoothstep(holeSize, holeSize + smoothening, len)
        * (1.0 - smoothstep(1.0 - smoothening, 1.0, len));
    
    float2 sampleUV = float2(rotation / (2.0 * PI), len * (1.0 + holeSize) - holeSize);
    float sampleAlpha1 = tex2D(
        sampleTextureSampler1,
        sampleUV * float2(0.1, 2.0) + float2(time, 0.0)
    ).r;
    
    float sampleAlpha2 = tex2D(
        sampleTextureSampler2,
        sampleUV * float2(0.1, 1.0) + float2(sin(rotation - time * 3.0), 0.0)
    ).r;
    
    return float4(color.rgb, color.a) * ringClamp * sampleAlpha1 * sampleAlpha2 * ringClamp * intensity;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
};