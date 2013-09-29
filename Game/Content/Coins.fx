sampler s0 : register(s0);


float4 PixelShaderFunction(float2 texCoords : TEXCOORD0) : COLOR
{
	float4 color = tex2D(s0, texCoords);
	return float4(color.rgb, clamp(pow(length(texCoords), 4), 0, 1)); 
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
