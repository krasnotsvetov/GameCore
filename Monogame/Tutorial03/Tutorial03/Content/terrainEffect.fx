float4x4 World;
float4x4 View;
float4x4 Projection;
float3 LightDirection;

texture BaseTextureA;
sampler2D BaseSamplerA = sampler_state
{
    Texture = <BaseTextureA>;
	MipFilter = POINT;
    MinFilter = POINT;
    MagFilter = POINT;
};

texture BaseTextureB;
sampler2D BaseSamplerB = sampler_state
{
	Texture = <BaseTextureB>;
	MipFilter = POINT;
	MinFilter = POINT;
	MagFilter = POINT;
};

struct VertexShaderInput
{
	float4 Position : SV_POSITION;
	float2 Normal : NORMAL0;
	float2 Color : COLOR0;
	float2 TexCoord : TEXCOORD0;
	float4 TexWeights : TEXCOORD1;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
	float LightIntensity : NORMAL0;
	float4 TexWeights : TEXCOORD1;
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
	output.TexWeights = input.TexWeights;
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 baseA = tex2D(BaseSamplerA, input.TexCoord) * input.TexWeights.r;
	float4 baseB = tex2D(BaseSamplerB, input.TexCoord) * input.TexWeights.g;
	return float4((baseA + baseB) * input.LightIntensity);
}

technique Standart
{
    pass
    {
        PixelShader = compile ps_5_0 PixelShaderFunction();
        VertexShader = compile vs_5_0 VertexShaderFunction();
    }
}