sampler uImage0 : register(s0);

float2 uScroll;

texture noiseTexture;
sampler noise = sampler_state
{
    Texture = noiseTexture;
};

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 noiseColor = tex2D(noise, coords + uScroll);

    if (!any(noiseColor))
    {
        return float4(0, 0, 0, 0);
    }

    return color;
}

technique Technique1
{
    pass WitheringPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
