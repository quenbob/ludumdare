﻿Shader "Randomchaos/Tutorial/SSOutline" 
{
	Properties 
	{
		_MainTex ("Diffuse", 2D) = "white" {}

		_OutlineColor ("Outline Color",Color) = (0,0,0,1)
        _OutlineSize ("Outline Size", float) = .05
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque"}
		LOD 200
		

		// Geom pass
        CGPROGRAM
        #pragma surface surf SimpleDiffuse 

        sampler2D _MainTex;
				
        half4 LightingSimpleDiffuse (inout SurfaceOutput s, half3 lightDir, half atten)
        {
			half4 c;

			half NdotL = saturate(dot(s.Normal, lightDir));

			c.rgb = s.Albedo * _LightColor0.rgb * NdotL;
			c.a = s.Alpha;
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
			o.Alpha = c.a;
        }
        ENDCG

		// Outline pass
        Cull Front

        CGPROGRAM
        #pragma surface surf LineLit vertex:vert

        float4 _OutlineColor;
        float _OutlineSize;

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
            o.Albedo = _OutlineColor;
            o.Alpha = _OutlineColor.a;
        }
        ENDCG
        
        CGPROGRAM
        #pragma surface surf LineLit vertex:vert

        float4 _OutlineColor;
        float _OutlineSize;

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
            o.Alpha = _OutlineColor.a;
        }
        ENDCG
        
        CGPROGRAM
        #pragma surface surf LineLit vertex:vert

        float4 _OutlineColor;
        float _OutlineSize;

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
            o.Alpha = _OutlineColor.a;
        }
        ENDCG
        
        CGPROGRAM
        #pragma surface surf LineLit vertex:vert

        float4 _OutlineColor;
        float _OutlineSize;

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
            o.Alpha = _OutlineColor.a;
        }
        ENDCG
        
        CGPROGRAM
        #pragma surface surf LineLit vertex:vert

        float4 _OutlineColor;
        float _OutlineSize;

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
            o.Alpha = _OutlineColor.a;
        }
        ENDCG
	}
	FallBack "Diffuse"
}
