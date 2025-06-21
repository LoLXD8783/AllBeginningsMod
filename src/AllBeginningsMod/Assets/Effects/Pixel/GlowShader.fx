float resolution = 0.05;
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
    float4 color = tex2D(samplerTexture, coords);
    
    for (int i = -1; i < 2; i++)
    {
        for (int j = -1; j < 2; j++)
        {
            if (i == 0 && j == 0)
            {
                continue;
            }
            
            color += tex2D(samplerTexture, coords + float2(i * resolution, j * resolution)) / 8.0;
        }
    }
    
    return color;
}

technique Technique1
{
    pass VignettePass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};