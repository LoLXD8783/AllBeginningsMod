
float4 PixelShaderFunction(float2 coords : TEXCOORD0, float4 color : COLOR0) : COLOR0
{
    coords = (coords - 0.5) * 2.0;
    float len = length(coords);
    float alpha = smoothstep(0.7, 1.0, len) * step(len, 1.0);
    color *= alpha * alpha;
    return float4(color.rgb, color.a * alpha);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};