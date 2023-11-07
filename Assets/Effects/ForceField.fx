float time;
float size;
float variation = 0.025;
float intensity = 2.0;
float fishEye;

float hitAngle;
float hitAlpha;

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
    float2 funnyCoords = (coords - 0.5) * 2.0;
    
    float clampSize = 1.0 - variation;
    float len = length(funnyCoords);
    
    float coordsAtan = atan2(funnyCoords.y, funnyCoords.x);

    float lenMaxClamp = step(len, clampSize + sin(time * 0.5) * variation * sin(coordsAtan * 3.0 + time));
    float lenMinClamp = smoothstep(clampSize - size, clampSize, len);
    float sampleAlpha = tex2D(sampleTextureSampler, (funnyCoords + fishEye * (funnyCoords / len)) / 2.0 + 0.5 + time * 0.05).r;
    
    float alpha = lenMinClamp * lenMaxClamp + sampleAlpha * 0.35 * lenMaxClamp;
    float angleAlpha = smoothstep(0.9, 1.0, cos(coordsAtan - hitAngle));
    
    color.rgb *= (intensity + angleAlpha) * alpha;
    return float4(color.rgb, color.a * alpha);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};