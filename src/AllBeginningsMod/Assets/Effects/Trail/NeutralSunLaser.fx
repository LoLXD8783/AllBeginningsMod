matrix transformationMatrix;
float time;
float stretch1 = 0.1;
float stretch2 = 0.2;
float4 color = float4(1.0, 1.0, 1.0, 1.0);

texture noise1;
sampler2D sampler1 = sampler_state
{
    texture = <noise1>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

texture noise2;
sampler2D sampler2 = sampler_state
{
    texture = <noise2>;
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

float4 PixelShaderFunction(VSOutput output) : COLOR0
{
    float funny = abs(output.coords.y - 0.5) + output.coords.x * 0.5;
    
    float2 uv1 = output.coords * float2(stretch1, 1.0) + float2(time, 0.0);
    float x = smoothstep(funny, max(1.0, funny), tex2D(sampler1, uv1).r);
    
    float2 uv2 = output.coords * float2(stretch2, 1.0) + float2(time * 0.35, 0.0);
    float y = smoothstep(funny, max(1.0, funny), tex2D(sampler2, uv2).r);
    
    return color * (x * y + step(abs(output.coords.y - 0.5), 0.05));
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
};