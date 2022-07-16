sampler uImage0 : register(s0);

texture noiseTexture;
sampler noise = sampler_state
{
    Texture = noiseTexture;
};

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 noiseColor = tex2D(noise, coords);
    float brightness = ((noiseColor.r + noiseColor.g + noiseColor.b) / 3) / 255;
    
    if (brightness > 0.5)
    {
        return color;
    }
    
    return float4(0, 0, 0, 0);
}

technique Technique1
{
    pass WitheringPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
