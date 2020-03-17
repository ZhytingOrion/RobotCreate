Shader "Unlit/CloudShader"
{
	Properties
	{
		_Color("Color", COLOR) = (1,1,1,1)
		//_CharacterPos("Character Position", Vector) = (0,0,0,0)
		_Ratio("Ratio", Float) = 1.0 
		_Radius("Radius", Float) = 5.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
			};

			float4 _Color;
			float4 _CharacterPos;
			float _Ratio;
			float _Radius;
			
			v2f vert (appdata v)
			{
				v2f o;
				//o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.uv = v.uv;
				float3 worldPos = mul(UNITY_MATRIX_M, v.vertex);
				float3 dir = worldPos - _CharacterPos.xyz;
				float distance = length(dir);
				worldPos += normalize(dir) * (1 - clamp(0,_Radius, distance) / _Radius) * _Ratio * max(0,dot(o.normal, normalize(dir)));
				o.vertex = mul(UNITY_MATRIX_VP, float4(worldPos, 1));
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float NdotL = max(0,dot(normalize(i.normal), normalize(_WorldSpaceLightPos0.xyz)));
				NdotL = NdotL * 0.5 + 0.5;
				
				fixed4 col = _Color * NdotL;
				return col;
			}
			ENDCG
		}
	}

}
