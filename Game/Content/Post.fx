sampler s0 : register(s0);

float4 PixelShaderFunction(float2 tex : TEXCOORD0) : COLOR
{
        // lens distortion coefficient (between
        float k = -.12;
       
        // cubic distortion value
        float kcube = +.2f;
       
       
        float r2 = (tex.x-0.5) * (tex.x-0.5) + (tex.y-0.5) * (tex.y-0.5);       
        float f = 0;
		
		f = 1 + r2 * (k + kcube * sqrt(r2));
       
        // get the right pixel for the current position
        float x = f*(tex.x-0.5)+0.5;
        float y = f*(tex.y-0.5)+0.5;
        float3 inputDistord = tex2D(s0,float2(x,y));
			
		float alpha = 0;
		alpha = sin(tex.y*1000) < 0? 0 : .15;

		float4 screen = tex2D(s0, tex);
        return float4(screen.r, inputDistord.g, inputDistord.b, 1) - float4(0, 0, 0, alpha);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
