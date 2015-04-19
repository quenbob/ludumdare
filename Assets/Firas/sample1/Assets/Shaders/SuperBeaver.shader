Shader "Custom/SuperBeaver" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_OutlineColor ("Outline Color",Color) = (0,0,0,1)
        _OutlineSize ("Outline Size", float) = .05
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
				// Outline pass
        Cull Front
        CGPROGRAM
        
        #pragma surface surf LineLit vertex:vert

        float4 _OutlineColor;
        float _OutlineSize;

        void vert (inout appdata_full v) 
        {
            v.vertex.xyz += v.normal * abs(_OutlineSize*(_SinTime.a*3+1.0));
            
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
            o.Albedo = float3(_SinTime.a,_CosTime.a*_CosTime.a,_CosTime.a*_SinTime.a);
            o.Alpha = 1.0;
        }
        ENDCG
        
       
		
		Cull Back
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		//float4 _CosTime;
		//float4 _SinTime;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = float3(_CosTime.a*_CosTime.a,_SinTime.a,_CosTime.a*_SinTime.a);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
