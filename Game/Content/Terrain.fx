float4 smoothInterpolate(float4 v0, float4 v1, float delta)
{
	delta = pow(delta, 2) * (3 - 2*delta);
	return lerp(v0, v1,delta);
}
//
//float cosineInterpolate(float v0, float v1, float delta)
//{
//	delta = (1 - cos(delta*3.14159265f))/2;
//	return lerp(v0, v1, delta);
//}

sampler s0 : register(s0);

texture heightmap;  
sampler heightmap_sampler = sampler_state
{
	Texture = heightmap;
	Filter = Point;
};

int verticesCount;

float4 PixelShaderFunction(float2 texCoords :TEXCOORD0) : COLOR0
{
    float verticesFrequency = 1/(float)verticesCount;
    texCoords *= (float)(verticesCount-1)/verticesCount;

    float lastHeight = tex2D(heightmap_sampler, texCoords);
    float nextHeight = tex2D(heightmap_sampler, float2(texCoords.x + verticesFrequency, texCoords.y));

    float delta = (texCoords.x % verticesFrequency) * verticesCount;
    float height = smoothInterpolate(lastHeight, nextHeight, delta);

    //TODO Replace with texture or something
    float4 color0 = float4(1, 1, 0, 1);
    float4 color1 = float4(0, .5, 0, 1);

    float4 finalColor = smoothInterpolate(color0, color1, (texCoords.y) - (height));
    if(height < texCoords.y)
	{
		return finalColor;
	}

    return float4(0, 0, 0, 0);

    //Replace with this for pixely terrain
    /*float4 finalColor = smoothInterpolate(color0, color1, (texCoords.y - texCoords.y % .04) - (height - height % .04));
    if(height - height %.01 < texCoords.y){return finalColor;}*/
}

technique Technique1
{
	pass Pass1
	{
		PixelShader	= compile ps_2_0 PixelShaderFunction();
	}
}
