float smoothInterpolate(float v0, float v1, float delta)
{
	delta = pow(delta, 2) * (3 - 2*delta);
	return lerp(v0, v1,delta);
}

float cosineInterpolate(float v0, float v1, float delta)
{
	delta = (1 - cos(delta*3.14159265f))/2;
	return lerp(v0, v1, delta);
}

sampler s0;

texture heightmap;  
sampler heightmap_sampler = sampler_state
{
	Texture = heightmap;
	Filter = Point;
};

float vectorHorizontalDistance;

float4 PixelShaderFunction(float2 textureCoords :TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, textureCoords);
	float2 lastPosition = tex2D(heightmap_sampler, textureCoords);
	float2 nextPosition = tex2D(heightmap_sampler, float2(textureCoords.x + .1, textureCoords.y));
	float delta = (textureCoords.x - lastPosition.x)/(nextPosition.x - lastPosition.x);
	if(smoothInterpolate(lastPosition.y, nextPosition.y, delta) > textureCoords.y)
		color.rgba = 0;
	return color;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader	= compile ps_2_0 PixelShaderFunction();
	}
}
