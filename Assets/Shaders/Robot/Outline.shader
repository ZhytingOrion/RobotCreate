Shader "Unlit/Outline"
{
    Properties
    {
		_Color("Color", COLOR) = (0,0,0,1)
		_OutlineWidth("Outline Width", float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
			Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

			float4 _Color;
			float _OutlineWidth;

            v2f vert (appdata v)
            {
                v2f o;
				float3 Pw = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 normal = UnityObjectToWorldNormal(v.normal);
				Pw += normal * _OutlineWidth;
				o.vertex = mul(UNITY_MATRIX_VP, float4(Pw, 1));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col = _Color;
                return col;
            }
            ENDCG
        }
    }
}
