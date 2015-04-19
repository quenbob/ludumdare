﻿Shader "Custom/Cell_Transparent2"
{
	Properties 
	{
		_MainTex ("Diffuse", 2D) = "white" {}
		
		_BackgroundTex ("Diffuse", 2D) = "black" {}

		_OutlineColor ("Outline Color",Color) = (0,0,0,1)
        _OutlineSize ("Outline Size", float) = .05
        _AlphaMultiplier("Alpha Multiplier",float)= 1.0
	}

	SubShader 
	{
		//Tags { "RenderType"="Opaque" "Queue" = "Transparent"}
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque"}
		LOD 200
		
		
		
		




		
		


		// Outline pass
        Cull Front
        ZWrite On

        CGPROGRAM
        #pragma surface surf LineLit vertex:vert Lambda alpha

        float4 _OutlineColor;
        float _OutlineSize;
        float _AlphaMultiplier;
        sampler2D _BackgroundTex;


        void vert (inout appdata_full v) 
        {
            v.vertex.xyz += (v.normal) * _OutlineSize;
        }

        half4 LightingLineLit (inout SurfaceOutput s, half3 lightDir, half3 viewDir) 
        {    
			return float4(s.Albedo,s.Alpha);
        }

        struct Input 
        {
            float2 uv_MainTex;
            float2 uv_Bump;
        };

        void surf (Input IN, inout SurfaceOutput o) 
        {
        
            half4 c = tex2D (_BackgroundTex, IN.uv_MainTex);
	o.Albedo = c.rgb;
           // o.Albedo = _OutlineColor;
            o.Alpha = _OutlineColor.a * _AlphaMultiplier;
            
            
             
        }
        ENDCG
        
        CGPROGRAM
        #pragma surface surf LineLit vertex:vert Lambda alpha

        float4 _OutlineColor;
        float _OutlineSize;
        float _AlphaMultiplier;

        void vert (inout appdata_full v) 
        {
            v.vertex.xyz += (v.normal+v.tangent) * (_OutlineSize*1.1);
        }

        half4 LightingLineLit (inout SurfaceOutput s, half3 lightDir, half3 viewDir) 
        {    
			return float4(s.Albedo,s.Alpha);
        }

        struct Input 
        {
            float2 uv_MainTex;
            float2 uv_Bump;
        };

        void surf (Input IN, inout SurfaceOutput o) 
        {
            o.Albedo = _OutlineColor;
            o.Alpha = _OutlineColor.a * _AlphaMultiplier;
        }
        ENDCG
        
        CGPROGRAM
        #pragma surface surf LineLit vertex:vert Lambda alpha

        float4 _OutlineColor;
        float _OutlineSize;
        float _AlphaMultiplier;

        void vert (inout appdata_full v) 
        {
            v.vertex.xyz += (v.normal-v.tangent) * (_OutlineSize*1.1);
        }

        half4 LightingLineLit (inout SurfaceOutput s, half3 lightDir, half3 viewDir) 
        {    
			return float4(s.Albedo,s.Alpha);
        }

        struct Input 
        {
            float2 uv_MainTex;
            float2 uv_Bump;
        };

        void surf (Input IN, inout SurfaceOutput o) 
        {
            o.Albedo = _OutlineColor;
            o.Alpha = _OutlineColor.a * _AlphaMultiplier;
        }
        ENDCG
        
        CGPROGRAM
        #pragma surface surf LineLit vertex:vert Lambda alpha

        float4 _OutlineColor;
        float _OutlineSize;
        float _AlphaMultiplier;

        void vert (inout appdata_full v) 
        {
            v.vertex.xyz += (v.normal-normalize(cross(v.tangent,v.normal))) * (_OutlineSize*1.1);
        }

        half4 LightingLineLit (inout SurfaceOutput s, half3 lightDir, half3 viewDir) 
        {    
			return float4(s.Albedo,s.Alpha);
        }

        struct Input 
        {
            float2 uv_MainTex;
            float2 uv_Bump;
        };

        void surf (Input IN, inout SurfaceOutput o) 
        {
            o.Albedo = _OutlineColor;
            o.Alpha = _OutlineColor.a * _AlphaMultiplier;
        }
        ENDCG
        
        CGPROGRAM
        #pragma surface surf LineLit vertex:vert Lambda alpha

        float4 _OutlineColor;
        float _OutlineSize;
        float _AlphaMultiplier;

        void vert (inout appdata_full v) 
        {
            v.vertex.xyz += (v.normal+normalize(cross(v.tangent,v.normal))) * (_OutlineSize*1.1);
        }

        half4 LightingLineLit (inout SurfaceOutput s, half3 lightDir, half3 viewDir) 
        {    
			return float4(s.Albedo,s.Alpha);
        }

        struct Input 
        {
            float2 uv_MainTex;
            float2 uv_Bump;
        };

        void surf (Input IN, inout SurfaceOutput o) 
        {
            o.Albedo = _OutlineColor;
            o.Alpha = _OutlineColor.a * _AlphaMultiplier;
        }
        ENDCG
        


        //Cull Front
Cull Back
ZWrite On




CGPROGRAM
#pragma surface surf SimpleDiffuse alpha

sampler2D _MainTex;
float _AlphaMultiplier;


		
half4 LightingSimpleDiffuse (inout SurfaceOutput s, half3 lightDir, half atten)
{
	half4 c;

	half NdotL = saturate(dot(s.Normal, lightDir));

	c.rgb = s.Albedo * _LightColor0.rgb * NdotL;
	c.a = s.Alpha * _AlphaMultiplier;
	return c;
}		

struct Input 
{
    float2 uv_MainTex;
    float2 uv_Bump;
};

void surf (Input IN, inout SurfaceOutput o) 
{
    half4 c = tex2D (_MainTex, IN.uv_MainTex);
	o.Albedo = c.rgb;
	o.Alpha = c.a * _AlphaMultiplier;
	
}
ENDCG
//Cull Back
ZWrite On
Blend SrcAlpha OneMinusDstAlpha 

Pass
{
 Lighting Off
 SetTexture [_MainTex] { combine texture } 
}


        
	}
	FallBack "Diffuse"
}
