Shader "Unlit/baiyechaung 3"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Amount("Amount",float) = 6
        _Blend("Blend",Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "PreviewType"="Plane" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Amount,_Blend;

            v2f vert (appdata v)
            {
                v2f o = (v2f)0;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float hdir =abs(frac(i.uv.x * _Amount + 0.5) * 2 - 1) - _Blend;
                float vdir =abs(frac(i.uv.y * _Amount * 0.5625 + 0.5) * 2 - 1) - _Blend;
                float al = step(0,hdir) * step(0,vdir);
                col.a = al;
                return col;
            }
            ENDCG
        }
    }
}
