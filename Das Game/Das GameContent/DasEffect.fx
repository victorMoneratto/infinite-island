float2 viewport;
//Pass through pixel shader for SM3.0 support
void VertexShaderFunction(inout float4 color    : COLOR0,  
						  inout float2 texCoord : TEXCOORD0,  
						  inout float4 position : POSITION0) 
{ 
	//Texel centering
	position.xy -= 0.5;  

	// Viewport adjustment.  
	position.xy = position.xy / viewport;  
	position.xy *= float2(2, -2);  
	position.xy -= float2(1, -1);  
}

float linearInterpolate(float v0, float v1, float delta)
{
	return v0 + (v1-v0)* delta;
}

float smoothInterpolate(float v0, float v1, float delta)
{
	delta = delta * delta * (3-2*delta);
	return v0 * (1-delta) + v1 * delta;
}

float cosineInterpolate(float v0, float v1, float delta)
{
	float delta2 = (1-cos(delta*3.14159265f))/2;
	return v0*(1-delta2) + v1 * delta2;
}

static const int verticesCount = 20;
float2 terrain[20];
sampler s0;

//temp
float fixedDistance;
float treshold;

float4 PixelShaderFunction(float2 pixelCoords : VPOS, float2 textureCoords :TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, textureCoords);
	int i = pixelCoords.x / fixedDistance;
	float delta = (pixelCoords.x - terrain[i].x)/(terrain[i+1].x - terrain[i].x);
	if(cosineInterpolate(terrain[i].y, terrain[i+1].y, delta) >= pixelCoords.y)
	{
		color.rgba = 0;
	}
	return color;
}



technique Technique1
{
	pass Pass1
	{
		VertexShader= compile vs_3_0 VertexShaderFunction();
		PixelShader	= compile ps_3_0 PixelShaderFunction();
	}
}
