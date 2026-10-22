Shader "FoGD/Water"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0, 0.2)) = 0.1

        _Height ("Main Wave Height", Float) = 0.5
        _Amplitude ("Main Wave Amplitude", Range(0, 0.2)) = 0.1
        _Frequency ("Main Wave Frequency", Range(0, 1)) = 1
        _Speed ("Main Wave Speed", Float) = 10.0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                half4 _OutlineColor;
                float _OutlineThickness;
                float _Height;
                float _Amplitude;
                float _Frequency;
                float _Speed;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float wave = _Height + sin(IN.positionWS.x * _Frequency * TWO_PI + _Time * _Speed) * _Amplitude;
                clip(wave + _OutlineThickness - IN.positionWS.y);
                if (IN.positionWS.y > wave)
                    return _OutlineColor;

                return _BaseColor;
            }
            ENDHLSL
        }
    }
}
