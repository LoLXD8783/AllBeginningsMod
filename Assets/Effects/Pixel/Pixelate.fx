int resolution = 2;
float2 size;
float stepMin = 0.3, stepMax = 0.5;

texture sampleTexture;
sampler2D textureSampler = sampler_state
{
    texture = <sampleTexture>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

float closest(float x, int t)
{
    int y = (int) (x * 100.0);
    return (float)(y - (y % t)) / 100.0;
}

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    int2 pixel = (int2) (coords * size);
    int2 topLeftPixel = pixel - pixel % resolution;

    float4 color = float4(0.0, 0.0, 0.0, 0.0);
    [unroll(6)]
    for (int i = 0; i < resolution; i++)
    {
        [unroll(6)]
        for (int j = 0; j < resolution; j++)
        {
            color += tex2D(textureSampler, (float2) (topLeftPixel + int2(i, j)) / size);
        }
    }
    
    // return float4(closest(color.r, 20), closest(color.g, 20), closest(color.b, 20), color.a > 0.3 ? 1.0 : 0.0);
    
    color = float4(closest(color.r, 20), closest(color.g, 20), closest(color.b, 20), closest(color.a, 20));
    return float4(color.rgb, smoothstep(stepMin, stepMax, color.a));
}

technique Technique1
{
    pass PixelatePass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
};