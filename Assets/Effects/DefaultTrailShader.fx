matrix transformation_matrix;
float4 color = float4(1, 1, 1, 1);

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

struct VSInput
{
    float4 position : POSITION;
    float2 texture_coordinates : TEXCOORD;
    float4 color : COLOR;
};

struct VSOutput
{
    float4 position : POSITION;
    float2 texture_coordinates : TEXCOORD;
    float4 color : COLOR;
};

VSOutput vertex_shader_function(VSInput input)
{
    VSOutput output;
    output.color = input.color;
    output.position = mul(input.position, transformation_matrix);
    output.texture_coordinates = input.texture_coordinates;
    
    return output;
}

float4 pixel_shader_function(VSOutput input) : COLOR0
{
    return input.color * tex2D(sampler_texture, input.texture_coordinates) * color;
}

technique technique1
{
    pass pass1
    {
        VertexShader = compile vs_2_0 vertex_shader_function();
        PixelShader = compile ps_2_0 pixel_shader_function();
    }
}