float progress;
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

texture color_sample_texture;
sampler2D color_sampler_texture = sampler_state
{
    texture = <color_sample_texture>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};

const float PI = 3.14;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float2 normalized_coords = (coords - 0.5) * 2.0;
    
    float len = length(normalized_coords);
    float angle = atan2(normalized_coords.y, normalized_coords.x);
    float x = angle * 23.3583 + progress * 2.5321;
    
    float pixelation = floor(100 * (1 - progress));
    float step_value = 0.1 + progress * 0.9;
    float alpha = 1.0 - step(step_value, floor(len * pixelation) / pixelation);
    
    float pixelated_coords = floor(coords * pixelation) / pixelation;
    float noise_value = tex2D(sampler_texture, pixelated_coords).r;
    float3 color_value = tex2D(color_sampler_texture, pixelated_coords);
    return float4(
        color_value * (pow((progress - 0.5 * 2), 2) + 1), 
        noise_value * alpha * (1 - pow(progress, 5))
    );
}

technique Technique1
{
    pass VignettePass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};