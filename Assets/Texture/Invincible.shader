Shader "Invincible/Oneshader"
{
    SubShader
    {
        tags{"Queue"="transparent"} //渲染透明
       pass
        {
            blend srcalpha oneminussrcalpha
            zwrite off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "unitycg.cginc"

            struct v2f
            {
                float4 pos:POSITION;
                float4 normal:NORMAL;
                float4 vertex:TEXCOORD1;
            };
                
                v2f vert(appdata_base v)
                {
                    v2f o;
                    o.pos=UnityObjectToClipPos(v.vertex);

                    o.normal=float4(v.normal,1);
                    o.vertex=v.vertex;

                    return o;
                }
                
                fixed4 frag(v2f IN):COLOR
                {
                    float3 N=UnityObjectToWorldNormal(IN.normal);
                    float3 V=WorldSpaceViewDir(IN.vertex);
                    
                    V=normalize(V);

                    float dots=1-saturate(dot(N,V));

                    return fixed4(1,1,1,1)*dots;
                }

            ENDCG
        }
    }
  
}
