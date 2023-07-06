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

float3 ambientColor; // Light's Ambient Color
float3 diffuseColor; // Light's Diffuse Color
float3 specularColor; // Light's Specular Color
float KAmbient; 
float KDiffuse; 
float KSpecular;
float shininess; 
float3 lightPosition;
float3 farosPosition;
float3 eyePosition; // Camera position

float Time;

float3 colorBloom;

float3 carDirection;

struct VertexShaderInput
{
	float4 Position : POSITION0;
    float4 Normal : NORMAL;
    float2 TextureCoordinate : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
    float4 Mesh : TEXCOORD3;
    float2 TextureCoordinate : TEXCOORD0;
    float4 WorldPosition : TEXCOORD1;
    float4 Normal : TEXCOORD2;    
};

texture ModelTexture;
sampler2D textureSampler = sampler_state
{
    Texture = (ModelTexture);
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

texture baseTexture;
sampler2D baseTextureSampler = sampler_state
{
    Texture = (baseTexture);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};

texture bloomTexture;
sampler2D bloomTextureSampler = sampler_state
{
    Texture = (bloomTexture);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};

texture TexturaRuido;
sampler2D ruidoSampler = sampler_state
{
    Texture = (TexturaRuido);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
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
	VertexShaderOutput output = (VertexShaderOutput)0;

    output.Mesh = input.Position;

    float4 worldPosition = mul(input.Position, World);
    // World space to View space
    float4 viewPosition = mul(worldPosition, View);	
	// View space to Projection space
    output.Position = mul(viewPosition, Projection);

    //output.Position = mul(input.Position, WorldViewProjection);
    output.WorldPosition = mul(input.Position, World);
    //output.Normal = mul(input.Normal, InverseTransposeWorld);
    output.Normal = mul(float4(normalize(input.Normal.xyz), 1.0), InverseTransposeWorld);
    output.TextureCoordinate = input.TextureCoordinate;
	
	return output;
}

float4 LuzPS(VertexShaderOutput input) : COLOR
{
    //LUZ SOL
    // Base vectors
    float3 lightDirection = normalize(lightPosition - input.WorldPosition.xyz);
    float3 viewDirection = normalize(eyePosition - input.WorldPosition.xyz);
    float3 halfVector = normalize(lightDirection + viewDirection);

	// Get the texture texel
    float4 texelColor = tex2D(textureSampler, input.TextureCoordinate);
    
	// Calculate the diffuse light
    float NdotL = saturate(dot(input.Normal.xyz, lightDirection));
    float3 diffuseLight = KDiffuse * diffuseColor * NdotL;

	// Calculate the specular light
    float NdotH = dot(input.Normal.xyz, halfVector);
    float3 specularLight = sign(NdotL) * KSpecular * specularColor * pow(saturate(NdotH), shininess);
    
    // Final calculation
    float4 finalColor = float4(saturate(ambientColor * KAmbient + diffuseLight) * texelColor.rgb + specularLight, texelColor.a);

    //LUZ FAROS
    lightDirection = normalize(farosPosition - input.WorldPosition.xyz);
    viewDirection = normalize(eyePosition - input.WorldPosition.xyz);
    halfVector = normalize(lightDirection + viewDirection);
    
    
	// Calculate the diffuse light
    NdotL = saturate(dot(input.Normal.xyz, lightDirection));
    diffuseLight = KDiffuse * diffuseColor * NdotL;

	// Calculate the specular light
    NdotH = dot(input.Normal.xyz, halfVector);
    specularLight = sign(NdotL) * KSpecular * specularColor * pow(saturate(NdotH), shininess);
    
    // Final calculation
    float4 finalColor2 = float4(saturate(ambientColor * KAmbient + diffuseLight) + specularLight, texelColor.a);

            float alcance = smoothstep(0, length(farosPosition - input.WorldPosition.xyz), 50.0);
        
            float LdotD = (dot(lightDirection, (-carDirection)));
            float rango = smoothstep(0.707, 1.0 , LdotD);
            
            float4 color = lerp(finalColor*texelColor,finalColor*texelColor + finalColor2*texelColor,rango*alcance);
        
    return color;

}

float4 EnvironmentMapPS(VertexShaderOutput input) : COLOR
{
	//Normalizar vectores
	float3 normal = normalize(input.Normal.xyz);
    
    float4 baseColor = LuzPS(input);
	// Get the texel from the texture
	//float3 baseColor = tex2D(textureSampler, input.TextureCoordinate).rgb;
	
    // Not part of the mapping, just adjusting color
    baseColor.xyz = lerp(baseColor.xyz, float3(1, 1, 1), step(length(baseColor), 0.01));
    
	//Obtener texel de CubeMap
	float3 view = normalize(eyePosition.xyz - input.WorldPosition.xyz);
	float3 reflection = reflect(view, normal);
	float3 reflectionColor = texCUBE(environmentMapSampler, reflection).rgb;

    float fresnel = saturate((1.0 - dot(normal, view)));

    return float4(lerp(baseColor.xyz, reflectionColor, fresnel), 1);
}

float4 DissolvePS(VertexShaderOutput input) : COLOR
{
    float4 baseColor = EnvironmentMapPS(input);
    
    float4 textureRuido = tex2D(ruidoSampler, input.TextureCoordinate);
    float4 textureAux = tex2D(auxiliarTextureSampler, input.TextureCoordinate+Time*0.1);
    //float4 red = float4(1.0, 0.0, 0.0, 1.0);

    float factor1 = step(textureRuido.r, sin(Time));
    float factor2 = step(textureRuido.r, sin(Time+0.09));

    if(factor2){
        baseColor = textureAux;
    }

    if(factor1){
        discard;
    }
    return baseColor;
}

float4 BloomAutoPS(VertexShaderOutput input) : COLOR
{
    float4 color = tex2D(baseTextureSampler, input.TextureCoordinate);

    if(input.Mesh.z>2.5 && input.Mesh.z<3.0 && input.Mesh.x<0.7 && input.Mesh.x>-0.7){
        color = float4(colorBloom, 1.0);
    }else{
        color = float4(0.0,0.0,0.0,1.0);
    }

    return color;
}

float4 BloomPowerUpPS(VertexShaderOutput input) : COLOR
{
    float4 color = tex2D(baseTextureSampler, input.TextureCoordinate);
    float4 bloom = lerp(float4(colorBloom, 1.0), float4(0.0,0.0,0.0,1.0), (sin(Time)));

    return bloom;
}

float4 BloomNegroPS(VertexShaderOutput input) : COLOR
{
    return float4(0.0,0.0,0.0,1.0);
}

float4 PintarRuedasPS(VertexShaderOutput input) : COLOR
{
    float4 texelColor = tex2D(textureSampler, input.TextureCoordinate);
    if((texelColor.r+texelColor.g+texelColor.b)/3>0.1){
        texelColor = lerp(float4(1.0, 0.0, 0.0, 1.0), texelColor, 0.4);
    }

    return texelColor;
}

VertexShaderOutput IntegrarVS(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;
    output.Position = input.Position;
    output.TextureCoordinate = input.TextureCoordinate;
    return output;
}

float4 IntegrarPS(in VertexShaderOutput input) : COLOR
{    
    float4 bloomColor = tex2D(bloomTextureSampler, input.TextureCoordinate);
    float4 sceneColor = tex2D(baseTextureSampler, input.TextureCoordinate);
    
    return sceneColor * 0.5 + bloomColor;
    
}

float4 IntegrarPresentacionPS(in VertexShaderOutput input) : COLOR
{    
    float4 auxiliarColor = tex2D(auxiliarTextureSampler, input.TextureCoordinate);
    float4 sceneColor = tex2D(baseTextureSampler, input.TextureCoordinate);
    
    return auxiliarColor*0.9+sceneColor;
    
}

technique Luz
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL LuzPS();
	}
};

technique PintarRuedas
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL PintarRuedasPS();
	}
};

technique Reflejo
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL EnvironmentMapPS();
	}
};

technique Dissolve
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL DissolvePS();
	}
};

technique BloomPowerUp
{
    pass Pass0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL BloomPowerUpPS();
    }
};

technique BloomNegro
{
    pass Pass0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL BloomNegroPS();
    }
};

technique Bloom
{
    pass Pass0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL BloomAutoPS();
    }
};

technique Integrar
{
    pass Pass0
    {
        VertexShader = compile VS_SHADERMODEL IntegrarVS();
        PixelShader = compile PS_SHADERMODEL IntegrarPS();
    }
};

technique IntegrarPresentacion
{
    pass Pass0
    {
        VertexShader = compile VS_SHADERMODEL IntegrarVS();
        PixelShader = compile PS_SHADERMODEL IntegrarPresentacionPS();
    }
};
