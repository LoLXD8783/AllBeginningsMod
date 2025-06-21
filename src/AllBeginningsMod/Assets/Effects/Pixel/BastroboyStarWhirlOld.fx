float time;
float yOffset = 0.0;
float ringSize = 0.3;
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

texture noiseTexture;
sampler2D noiseTextureSampler = sampler_state
{
    texture = <noiseTexture>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

const float PI = 3.14159265;

float4 PixelShaderFunction(float2 coords : TEXCOORD0, float4 color : COLOR0) : COLOR0
{
    coords.x = (coords.x - 0.5) * 2.0;
    coords.y += yOffset;
    
    float len = length(coords);
    float alpha = len > ringSize && len <= 1.0 ? 1.0 : 0.0;
    
    float2 samplePoint;
    samplePoint.x = (((atan2(coords.y, coords.x) + PI) / 8.0 + time) + 1.0) / 2.0;
    samplePoint.y = len * (1.0 + ringSize) - ringSize;
    
    alpha *= tex2D(sampleTextureSampler, samplePoint).r;
    
    float noiseSamplePoint = samplePoint;
    noiseSamplePoint.x *= 0.5;
    
    float intensity = tex2D(noiseTextureSampler, noiseSamplePoint).r;
    color *= 3.0 * intensity;
    
    return float4(color.rgb, alpha * color.a);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};
