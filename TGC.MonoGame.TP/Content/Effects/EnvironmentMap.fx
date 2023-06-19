#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 World;
float4x4 InverseTransposeWorld;
float4x4 View;
float4x4 Projection;

float3 eyePosition;

texture baseTexture;
sampler2D textureSampler = sampler_state
{
    Texture = (baseTexture);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};

texture environmentMap;
samplerCUBE environmentMapSampler = sampler_state
{
    Texture = (environmentMap);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};


struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Normal : NORMAL;
    float2 TextureCoordinates : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float2 TextureCoordinates : TEXCOORD0;
	float4 WorldPosition : TEXCOORD1;
	float4 Normal : TEXCOORD2;
};


VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

    float4 worldPosition = mul(input.Position, World);
    // World space to View space
    float4 viewPosition = mul(worldPosition, View);	
	// View space to Projection space
    output.Position = mul(viewPosition, Projection);

    //output.Position = mul(input.Position, WorldViewProjection);
    output.WorldPosition = mul(input.Position, World);
    output.Normal = mul(input.Normal, InverseTransposeWorld);
    output.TextureCoordinates = input.TextureCoordinates;
	
	return output;
}

float4 EnvironmentMapPS(VertexShaderOutput input) : COLOR
{
	//Normalizar vectores
	float3 normal = normalize(input.Normal.xyz);
    
	// Get the texel from the texture
	float3 baseColor = tex2D(textureSampler, input.TextureCoordinates).rgb;
	
    // Not part of the mapping, just adjusting color
    baseColor = lerp(baseColor, float3(1, 1, 1), step(length(baseColor), 0.01));
    
	//Obtener texel de CubeMap
	float3 view = normalize(eyePosition.xyz - input.WorldPosition.xyz);
	float3 reflection = reflect(view, normal);
	float3 reflectionColor = texCUBE(environmentMapSampler, reflection).rgb;

    float fresnel = saturate((1.0 - dot(normal, view)));

    return float4(lerp(baseColor, reflectionColor, fresnel), 1);
}



technique EnvironmentMap
{
    pass Pass0
    {
		VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL EnvironmentMapPS();
    }
};



