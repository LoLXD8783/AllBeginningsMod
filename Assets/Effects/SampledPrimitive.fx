matrix worldViewProjection;

texture sampleTexture;

sampler sampleTextureSampler = sampler_state
{
    texture = sampleTexture;
};

struct VertexShaderOutput
{
    float4 Position : POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

struct VertexShaderInput
{
    float4 Position : POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input) 
{
    VertexShaderOutput output;
    
    output.Position = mul(input.Position, worldViewProjection);
    output.Color = input.Color;
    output.TextureCoordinates = input.TextureCoordinates;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
     float4 color = tex2D(sampleTextureSampler, input.TextureCoordinates);
     return input.Color * color;
}

technique Technique1
{
    pass SampledPrimitivePass
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};