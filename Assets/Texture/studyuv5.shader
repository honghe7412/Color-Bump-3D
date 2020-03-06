Shader "Custom/studyuv5" //纹理混合
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BG ("BG", 2D) = "white" {}
        _F("_F",Range(1,10))=4
        _A("_A",Range(0.01,0.1))=0.01
    }

    SubShader
    {
        pass
        {
            //ColorMask rg //控制颜色输出

            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #include "unitycg.cginc"
            //#pragma target
            sampler2D _MainTex;
            sampler2D _BG;
            float4 _MainTex_ST; //只要声明了_MainTex_ST（后缀ST，让系统识别），unity系统就会自动赋值；
            float _F;
            Float _A;
            
            struct v2f
            {
                float4 pos:POSITION;
                float2 uv:TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;

                o.pos=UnityObjectToClipPos(v.vertex);
               // o.uv=v.texcoord.xy*_MainTex_ST.xy+_MainTex_ST.zw; //获取模型的UV 有平铺和偏移的功能；
                //o.uv=TRANSFORM_TEX(v.texcoord,_MainTex); 
                o.uv=v.texcoord.xy;

                return o;
            }
            
            fixed4 frag(v2f IN):COLOR  //多次采样
            {
                fixed4 Maincolor=tex2D(_BG,IN.uv);

                float Offset_uv=_A*sin(IN.uv*_F+_Time.x*5);

                float2 uv=IN.uv+Offset_uv;

                fixed4 color1=tex2D(_MainTex,uv);
                
                Maincolor.rgb*=color1;
                //Maincolor.rbg*=2;

                uv=IN.uv-Offset_uv;

                fixed4 color2=tex2D(_MainTex,uv);

                Maincolor.rgb*=color2;
                Maincolor.rbg*=2;

                return Maincolor;
                /*float2 uv=IN.uv;
                float Offset_uv=_A*sin(IN.uv*_F+_Time.x*2);

                uv+=Offset_uv;
                
                fixed4 color1=tex2D(_MainTex,uv);

                uv=IN.uv;
                uv-=Offset_uv*2;
                
                fixed4 color2=tex2D(_MainTex,uv);

                return (color1+color2)/2;*/
            }

            ENDCG
        }
    }
    //FallBack "Diffuse"
}
