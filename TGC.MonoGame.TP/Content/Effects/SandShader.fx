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

float Time;

float2 screenSize;


struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 TextureCoordinate : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float2 TextureCoordinate : TEXCOORD0;
};

texture TexturaRuido;
sampler2D ruidoSampler = sampler_state
{
    Texture = (TexturaRuido);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};

texture texturaAuxiliar;
sampler2D auxiliarTextureSampler = sampler_state
{
    Texture = (texturaAuxiliar);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};


#define PI 3.1415926535898

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = input.Position;
    output.TextureCoordinate = input.TextureCoordinate;
    return output;
	
	return output;
}


float4 BlowingSandPS(in VertexShaderOutput input) : COLOR
{    
    float2 t = input.TextureCoordinate;
    float4 arena = tex2D(auxiliarTextureSampler, float2(t.x+Time*0.1, t.y));
    float4 backColor = tex2D(ruidoSampler, float2(t.x+Time*0.1, t.y));
    float4 frontColor = tex2D(ruidoSampler, float2(t.x+Time*0.2+20, t.y));
    
    return 0.7*backColor*arena*frontColor*arena;
    
}

technique BlowingSand
{
    pass Pass0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL BlowingSandPS();
    }
};
