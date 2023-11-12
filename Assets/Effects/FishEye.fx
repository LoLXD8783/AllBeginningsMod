sampler uImage0 : register(s0);
float4 uSourceRect;
float2 uImageSize0;

float strength;
float2 center = float2(0.5, 0.5);

float4 PixelShaderFunction(float2 coords : TEXCOORD0, float4 color : COLOR0) : COLOR0
{
    if (strength < 0.0001)
    {
        return tex2D(uImage0, coords) * color;
    }
    
    float2 sourceSize = uSourceRect.zw / uImageSize0;
    float2 sourceXY = uSourceRect.xy / uImageSize0;
    float2 localCoords = (coords - sourceXY) / sourceSize;
        
    float2 difference = (localCoords - center);
    float len = length(difference);
    float2 direction = difference / len;

    float x = sqrt(dot(center, center));
    
    float2 offset = direction * tan(len * strength) * x / tan(strength * x);
    float2 defaultValue = direction * len;
    float t = len * 2.0;
    
    float2 uv = center + offset * (1.0 - t) + defaultValue * t;
    if (uv.x < 0.0 || uv.x > 1.0 || uv.y < 0.0 || uv.y > 1.0)
    {
        return float4(0.0, 0.0, 0.0, 0.0);
    }
    
    return tex2D(uImage0, uv * sourceSize + sourceXY);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};