float progress;
texture sample_texture;
sampler2D sampler_texture = sampler_state
{
    texture = <sample_texture>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

const float PI = 3.14;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    coords = (coords - 0.5) * 2.0;
    float len = length(coords);
    float alpha = 1.0 - (step(0.25, abs(len - 0.5) + 0.4 * sin(progress * PI + PI * 0.5)));
    alpha += len * sin(atan2(coords.y, coords.x) * 8.53189 + progress);
    return float4(1.0, 0.0, 0.0, alpha);
}

technique Technique1
{
    pass VignettePass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};