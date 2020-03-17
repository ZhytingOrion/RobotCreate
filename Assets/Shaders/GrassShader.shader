Shader "Unlit/GrassShader"
{
	Properties
	{
		_Color("Color", COLOR) = (0,1,0,1)
		_Ratio("Ratio", Range(0,1)) = 1
		_Radius("Radius", Float) = 1
		_ObjPos("Object Position", Vector)= (0,0,0,0)
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

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
			};

			float4 _Color;
			float _Ratio;
			float _Radius;
			float4 _CharacterPos;
			float4 _ObjPos;
			
			v2f vert (appdata v)
			{
				v2f o;
				//o.vertex = UnityObjectToClipPos(v.vertex);
				//float2 dir = _ObjPos.xz - _CharacterPos.xz;
				//o.vertex.xz += smoothstep(_Radius, 0, length(dir))* normalize(dir) * v.color.r * _Ratio;
				float4 worldPos = mul(UNITY_MATRIX_M, v.vertex);
				float2 dir = _ObjPos.xz - _CharacterPos.xz;// worldPos.xz - _CharacterPos.xz;
				worldPos.xz += smoothstep(_Radius, 0, length(dir)) * normalize(dir) * v.color.r * _Ratio;
				o.vertex = mul(UNITY_MATRIX_VP, worldPos);
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float NdotL = max(0,dot(normalize(i.normal), normalize(_WorldSpaceLightPos0.xyz)));
				NdotL = NdotL * 0.5 + 0.5;
				fixed4 col = _Color * NdotL;
				return col;
			}
			ENDCG
		}
	}
}
