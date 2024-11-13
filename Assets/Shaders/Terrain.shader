Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
        _CookieTex ("Cookie Tex", 2D) = "white" {}
        _DarknessFade ("Darkness Fade", Float) = 100.0
        _AnglePower ("Angle Power", Float) = 2.0
        _CookieStrength ("Cookie Strength", Float) = 1.0
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
            #pragma multi_compile_fog
            
            #include "UnityCG.cginc"

            #define MAX_LIGHT_COUNT 4

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD2;
                float3 normal : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _CookieTex;
            float _CookieStrength;

            // _11_12_13 = position
            // _21_22_23 = direction
            // _31 = angle
            // _32 = range
            // _33 = spotlight? (else pointlight)
            // _34 = enabled?
            float4x4 _LightProperties[MAX_LIGHT_COUNT]; 

            float _DarknessFade;
            float _AnglePower;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = v.normal;

                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
            float modulate(float x, float strength) {
                return (1.0 - strength) + strength * x;
            }

            fixed4 frag (v2f IN) : SV_Target
            {
                fixed3 col = tex2D(_MainTex, TRANSFORM_TEX(IN.worldPos.xz, _MainTex)).rgb;

                float sunAngleFactor = saturate(dot(IN.normal, _WorldSpaceLightPos0));
                float globeFactor = saturate(1.0 - IN.worldPos.z / _DarknessFade);

                float additionalLightFactor = 0.0;
                for(int i = 0; i < MAX_LIGHT_COUNT; i++) {
                    float4x4 light = _LightProperties[i];
                    
                    float3 diff = IN.worldPos - light._11_12_13;
                    float distAtten = pow(saturate(1.0 - length(diff) / light._32), 2.0);

                    float3 dir = light._21_22_23;
                    float angleNormalised = acos(dot(normalize(diff), normalize(dir))) / light._31;
                    float angleAtten = pow(saturate(1.0 - angleNormalised), _AnglePower) * modulate(tex2D(_CookieTex, float2(angleNormalised, 0.0)).r, _CookieStrength);
                    float atten = lerp(1.0, angleAtten, light._33);

                    float lighting = distAtten * atten * light._34;
                    additionalLightFactor += lighting;
                }

                col *= saturate(sunAngleFactor * globeFactor + additionalLightFactor);

                UNITY_APPLY_FOG(i.fogCoord, col);

                return fixed4(col, 1.0);
            }
            ENDCG
        }
    }
}