float progress; // should be from 0 to 1 (1 is when the smoke dissapears)
float time;
float smokeCut = 0.3;
float smokeCutSmoothness = 0.1;
float noiseScale1 = 1.0;
float noiseScale2 = 1.0;
float4 edgeColor;

texture smokeTexture;
sampler2D smokeTextureSampler = sampler_state
{
    texture = <smokeTexture>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

texture noiseTexture1;
sampler2D noiseTextureSampler1 = sampler_state
{
    texture = <noiseTexture1>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

texture noiseTexture2;
sampler2D noiseTextureSampler2 = sampler_state
{
    texture = <noiseTexture2>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

float2 rotate(float2 vec, float rotation)
{
    float rotSin = sin(rotation);
    float rotCos = cos(rotation);
    
    return float2(vec.x * rotCos - vec.y * rotSin, vec.y * rotCos + vec.x * rotSin);
}

float sampleAlpha(sampler2D sampl, float2 uv)
{
    return tex2D(sampl, uv).r * (uv.x < 0.0 || uv.x > 1.0 || uv.y < 0.0 || uv.y > 1.0 ? 0.0 : 1.0);
}

float4 PixelShaderFunction(float2 uv : TEXCOORD0, float4 color : COLOR0) : COLOR0
{
    float smokeAlpha = sampleAlpha(smokeTextureSampler, rotate(uv - 0.5, time) + 0.5);
    float noiseAlpha1 = sampleAlpha(noiseTextureSampler1, (rotate(uv - 0.5, -time) + 0.5) * noiseScale1);
    float noiseAlpha2 = sampleAlpha(noiseTextureSampler2, (rotate(uv - 0.5, time * 1.5) + 0.5) * noiseScale2);
    
    smokeAlpha *= smoothstep(smokeCut, smokeCut + smokeCutSmoothness, noiseAlpha1);
    smokeAlpha *= smoothstep(progress - 0.1, progress, noiseAlpha2);
    
    return lerp(edgeColor, color, smoothstep(progress, progress + 0.3, noiseAlpha2)) * smokeAlpha * (1.0 - progress);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
};