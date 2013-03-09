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
int verticesCount;
float2 terrain[8];
sampler s0;

float4 PixelShaderFunction(float2 pixelCoords : VPOS, float2 textureCoords :TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, textureCoords);
	int i = pixelCoords.x / 200;
	float totalDeltaX = terrain[i+1].x - terrain[i].x;
	float curentDeltaX = terrain[i+1].x - pixelCoords.x;
	float deltaPercentage = 1 - curentDeltaX/totalDeltaX;
	if(lerp(terrain[i].y, terrain[i+1].y, deltaPercentage)> pixelCoords.y)
		color.rgba = 0;

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
