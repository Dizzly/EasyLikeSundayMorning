Shader "Custom/FadeinShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_HitPosition("HitPos", Vector) =(0,0,0,1)
		_T("T",Float)=100000
		_Collision("Collision", Float)=1
	}
	SubShader {
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _Collision;
		float4 _HitPosition;
		float _T;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			if(_Collision>0)
			{
				if(distance(_HitPosition.xyz,IN.worldPos)<_T)
				{
					o.Albedo = c.rgb;
					o.Alpha=1;
				}
			}
			else
			{
				o.Albedo = float3(0,0,0);
				o.Alpha = 0;
			}
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
