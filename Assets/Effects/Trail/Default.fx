matrix transformationMatrix;
float4 color = float4(1, 1, 1, 1);
texture sampleTexture;
bool blackAsAlpha = false;
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
    float2 textureCoordinates : TEXCOORD;
    float4 color : COLOR;
};

struct VSOutput
{
    float4 position : POSITION;
    float2 textureCoordinates : TEXCOORD;
    float4 color : COLOR;
};

VSOutput vertex_shader_function(VSInput input)
{
    VSOutput output;
    output.color = input.color;
    output.position = mul(input.position, transformationMatrix);
    output.textureCoordinates = input.textureCoordinates;
    
    return output;
}

float4 pixel_shader_function(VSOutput input) : COLOR0
{
    if (blackAsAlpha)
    {
        return input.color * tex2D(samplerTexture, input.textureCoordinates).r * color;
    }
    
    return input.color * tex2D(samplerTexture, input.textureCoordinates) * color;
}

technique technique1
{
    pass pass1
    {
        VertexShader = compile vs_2_0 vertex_shader_function();
        PixelShader = compile ps_2_0 pixel_shader_function();
    }
}