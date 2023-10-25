float time;
float3 color;
texture sampleTexture;
sampler2D samplerTexture = sampler_state
{
    texture = <sampleTexture>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float multiplier = sin(length((coords - 0.5) * 2.0) - time);
    float4 outColor = tex2D(samplerTexture, coords);
    
    outColor.rgb *= color * multiplier;
    
    return outColor;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};