sampler s0 : register(s0);

float4 PixelShaderFunction(float2 texCoords:TEXCOORD0) : COLOR0
{
	float alpha;
	
	alpha = sin(texCoords.y*1000) < 0? 0 : .2;

    return tex2D(s0, texCoords) - float4(0, 0, 0, alpha);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
