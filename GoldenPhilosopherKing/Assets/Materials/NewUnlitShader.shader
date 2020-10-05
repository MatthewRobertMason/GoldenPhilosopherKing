Shader "Tutorial/Basic" {
    Properties {
        // _MainTex("Base (RGB)", 2D) = "white" { }
        _Noise("Noise", 2D) = "white" { }
        _Strength("Distort Strength", float) = 1.0
        _Speed("Distort Speed", float) = 1.0
    }
    SubShader {
        // Draw ourselves after all opaque geometry
        Tags { 
            "Queue" = "Transparent" 
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"    
        }

        // Grab the screen behind the object into _BackgroundTexture
        GrabPass
        {
            "_BackgroundTexture"
        }

        ZTest Off
        ZWrite Off 

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            uniform sampler2D _Noise;
            uniform float _Strength;
            uniform float _Speed;

            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 texCoord : TEXCOORD0;
            };

            v2f vert(vertexInput input) {
                v2f output;
                output.pos = UnityObjectToClipPos(input.vertex);
                output.grabPos = ComputeGrabScreenPos(output.pos);

                float noise = tex2Dlod(_Noise, float4(input.texCoord, 0)).rgb;
                output.grabPos.x += cos(noise*_Time.x*_Speed) * _Strength;
                output.grabPos.y += sin(noise*_Time.x*_Speed) * _Strength;

                return output;
            }

            sampler2D _BackgroundTexture;

            half4 frag(v2f i) : SV_Target
            {
                half4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos);
                return bgcolor;
            }
            ENDCG
        }
    }
}