matrix transformationMatrix;

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

VSOutput VertexFunction(VSInput input)
{
    VSOutput output;
    output.color = input.color;
    output.position = mul(input.position, transformationMatrix);
    output.textureCoordinates = input.textureCoordinates;
    
    return output;
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexFunction();
    }
}