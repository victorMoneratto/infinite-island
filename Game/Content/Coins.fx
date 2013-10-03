sampler s0 : register(s0);

float2 center;
float aspectRatio;

float4 PixelShaderFunction(float2 texCoords : TEXCOORD0) : COLOR
{
	float4 color = tex2D(s0, texCoords);
	float distance = length(((texCoords - center)* float2(aspectRatio, 1)) * 3);

	return float4(color.rgb, min(1 - clamp(pow(distance, 2), 0, 1), color.a));
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
