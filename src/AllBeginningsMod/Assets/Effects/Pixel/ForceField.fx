float time;
float size;
float variation = 0.025;
float intensity = 2.0;
float fishEye;
float sampleOpacity = 0.2;

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

float4 PixelShaderFunction(float2 coords : TEXCOORD0, float4 color : COLOR0) : COLOR0
{
    coords = (coords - 0.5) * 2.0;
    
    float clampSize = 1.0 - variation;
    float len = length(coords);
    
    float coordsAtan = atan2(coords.y, coords.x);

    float lenMaxClamp = step(len, clampSize + sin(time * 0.5) * variation * sin(coordsAtan * 3.0 + time));
    float lenMinClamp = smoothstep(clampSize - size, clampSize, len);
    
    float2 direction = coords / len;
    float2 sampleCoords = direction * tan(len * fishEye) * 0.707106781 / tan(fishEye * 0.707106781);
    sampleCoords *= 0.66;
    sampleCoords = sampleCoords / 2.0 + 0.5;
    
    float angle = 3.14 / 4.0;
    sampleCoords *= 0.35;
    sampleCoords.y *= 3.0;
    
    float sampleAlpha = tex2D(sampleTextureSampler, sampleCoords + time * 0.025).r;
    
    float alpha = lenMinClamp * lenMaxClamp + sampleAlpha * sampleOpacity * lenMaxClamp;
    
    color.rgb *= intensity * alpha * 1.25;
    return float4(color.rgb, color.a * alpha);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
};