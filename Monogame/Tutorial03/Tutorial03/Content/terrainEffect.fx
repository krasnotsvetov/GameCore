float4x4 World;
float4x4 View;
float4x4 Projection;
float3 LightDirection;

texture BaseTexture;
sampler2D BaseSampler = sampler_state
{
    Texture = <BaseTexture>;
	MipFilter = POINT;
    MinFilter = POINT;
    MagFilter = POINT;
};

struct VertexShaderInput
{
	float4 Position : SV_POSITION;
	float2 Normal : NORMAL0;
	float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
	float2 Normal : NORMAL0;
	float LightIntensity : TEXCOORD1;
};
 

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	output.TexCoord = input.TexCoord;
	float3 tempNormal = mul(input.Normal, World);

	float3 Normal = normalize(tempNormal);
	output.LightIntensity = dot(Normal, -LightDirection);
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{

	return float4(tex2D(BaseSampler, input.TexCoord) * input.LightIntensity);
}

technique Standart
{
    pass
    {
        PixelShader = compile ps_5_0 PixelShaderFunction();
        VertexShader = compile vs_5_0 VertexShaderFunction();
    }
}