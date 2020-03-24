Shader "Effect/OutlineEffectShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _OutlineTex ("Outline Texture", 2D) = "white" {}
    }

    CGINCLUDE

    sampler2D _MainTex;
    float4 _MainTex_TexelSize;
    sampler2D _OutlineTex;
    float4 _OutlineTex_TexelSize;
    float4 _Color;
    float _OutlineWidth;

    struct appdata {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct v2f_cal {
        float4 pos : SV_POSITION;
        float4 uv0 : TEXCOORD0;
        float4 uv1 : TEXCOORD1;
    };

    struct v2f_add {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0;
    };

    v2f_cal vert_cal(appdata v)
    {
        v2f_cal o;
        o.pos = UnityObjectToClipPos(v.vertex);
        o.uv0 = v.uv.xyxy + _OutlineTex_TexelSize.xyxy * float4(1, 1, -1, 1) * _OutlineWidth;
        o.uv1 = v.uv.xyxy + _OutlineTex_TexelSize.xyxy * float4(-1, 1, -1, -1) * _OutlineWidth;
        return o;
    }

    v2f_add vert_add(appdata v)
    {
        v2f_add o;
        o.pos = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
    }

    fixed4 frag_cal(v2f_cal i) : SV_TARGET
    {
        fixed4 col = fixed4(0,0,0,1);
        fixed col_0 = tex2D(_OutlineTex, i.uv0.xy).b;
        fixed col_1 = tex2D(_OutlineTex, i.uv0.zw).b;
        fixed col_2 = tex2D(_OutlineTex, i.uv1.xy).b;
        fixed col_3 = tex2D(_OutlineTex, i.uv1.zw).b;
        col += ceil(clamp(abs(col_0 * 3 - col_1 - col_2 - col_3), 0, 0.9)) * _Color;
        return col;
    }

    fixed4 frag_add(v2f_add i) : SV_TARGET
    {
        fixed4 col = tex2D(_MainTex, i.uv);
        fixed3 outline = tex2D(_OutlineTex, i.uv).rgb;
        col.rgb += outline;
        return col;
    }

    ENDCG

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_cal
            #pragma fragment frag_cal
            #include "UnityCG.cginc"
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_add
            #pragma fragment frag_add
            #include "UnityCG.cginc"
            ENDCG
        }
    }
}
