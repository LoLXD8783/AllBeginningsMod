matrix transformationMatrix;
float time;
float amp = 0.1;
float smooth = 0.2;
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

struct VSInput
{
    float4 position : POSITION;
    float2 coords : TEXCOORD;
    float4 color : COLOR;
};

struct VSOutput
{
    float4 position : POSITION;
    float2 coords : TEXCOORD;
    float4 color : COLOR;
};

VSOutput VertexShaderFunction(VSInput input)
{
    VSOutput output;
    output.color = input.color;
    output.position = mul(input.position, transformationMatrix);
    output.coords = input.coords;
    
    return output;
}

const float PI = 3.14;

float WaveValue(float x)
{
    return (0.3 * sin(time * 10.0 + x * 15.0) + 0.7 * cos(time * 24.0 + x * 13.0)) * amp * (1.0 - x);
}

float4 PixelShaderFunction(VSOutput output) : COLOR0
{
    float2 uv = output.coords;
    uv.x = cos(uv.x * PI / 2.0);
    uv.y = (uv.y - 0.5) * 2.0;
    
    float alphaYValue = abs(uv.y + WaveValue(uv.x));
    float alpha = 1.0 - smoothstep(
        uv.x - smooth,
        uv.x,
        alphaYValue
    );
    
    float sampleAlpha = smoothstep(0.25, 0.5, tex2D(samplerTexture, uv * 0.25 + float2(time, 0.0)).r);
    return output.color * (alpha + sampleAlpha * uv.x * (1.0 - abs(uv.y)));
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}