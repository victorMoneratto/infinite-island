sampler s0 : register(s0);

float4 BlurFunction3x3(float2 texcoords :TEXCOORD0) : COLOR0
{
	float factor = 1.0f / 100;
  // TOP ROW
  float4 s11 = tex2D(s0, texcoords + float2(-factor, -factor));    // LEFT
  float4 s12 = tex2D(s0, texcoords + float2(0, -factor));              // MIDDLE
  float4 s13 = tex2D(s0, texcoords + float2(factor, -factor)); // RIGHT
 
  // MIDDLE ROW
  float4 s21 = tex2D(s0, texcoords + float2(-factor, 0));             // LEFT
  float4 col = tex2D(s0, texcoords);                                          // DEAD CENTER
  float4 s23 = tex2D(s0, texcoords + float2(-factor, 0));                 // RIGHT
 
  // LAST ROW
  float4 s31 = tex2D(s0, texcoords + float2(-factor, factor)); // LEFT
  float4 s32 = tex2D(s0, texcoords + float2(0, factor));                   // MIDDLE
  float4 s33 = tex2D(s0, texcoords + float2(factor, factor));  // RIGHT
 
  // Average the color with surrounding samples
  col = (col + s11 + s12 + s13 + s21 + s23 + s31 + s32 + s33) / 9;
  return col;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 BlurFunction3x3();
    }
}
