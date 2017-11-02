// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:True,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:2,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:False,qofs:0,qpre:0,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:7713,x:35767,y:33868,varname:node_7713,prsc:2|normal-3521-OUT,emission-7015-OUT,custl-8278-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:786,x:32089,y:32260,ptovrint:False,ptlb:Control,ptin:_Control,varname:node_786,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3361,x:32075,y:31976,varname:node_3361,prsc:2,ntxv:0,isnm:False|TEX-786-TEX;n:type:ShaderForge.SFN_Tex2d,id:5986,x:31498,y:32302,varname:node_5986,prsc:2,ntxv:0,isnm:False|TEX-6038-TEX;n:type:ShaderForge.SFN_Tex2d,id:5603,x:31484,y:32484,varname:node_5603,prsc:2,ntxv:0,isnm:False|TEX-8406-TEX;n:type:ShaderForge.SFN_Tex2d,id:8836,x:31484,y:32675,varname:node_8836,prsc:2,ntxv:0,isnm:False|TEX-7356-TEX;n:type:ShaderForge.SFN_Tex2d,id:741,x:31484,y:32870,varname:node_741,prsc:2,ntxv:0,isnm:False|TEX-2714-TEX;n:type:ShaderForge.SFN_Tex2d,id:4497,x:32976,y:33382,varname:node_4497,prsc:2,ntxv:0,isnm:False|TEX-379-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:6038,x:31264,y:32356,ptovrint:False,ptlb:Splat0,ptin:_Splat0,varname:node_6038,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:8406,x:31264,y:32543,ptovrint:False,ptlb:Splat1,ptin:_Splat1,varname:node_8406,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:7356,x:31264,y:32778,ptovrint:False,ptlb:Splat2,ptin:_Splat2,varname:node_7356,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:379,x:32729,y:33382,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_379,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:2714,x:31252,y:32977,ptovrint:False,ptlb:Splat3,ptin:_Splat3,varname:node_2714,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ChannelBlend,id:2134,x:32980,y:32487,varname:node_2134,prsc:2,chbt:1|M-6210-OUT,R-5986-RGB,G-5603-RGB,B-8836-RGB,A-741-RGB,BTM-9897-OUT;n:type:ShaderForge.SFN_Color,id:3790,x:32939,y:33637,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_3790,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:3040,x:33361,y:33302,varname:node_3040,prsc:2,v1:0;n:type:ShaderForge.SFN_Lerp,id:2607,x:33575,y:33606,varname:node_2607,prsc:2|A-4497-RGB,B-3790-RGB,T-3040-OUT;n:type:ShaderForge.SFN_DDX,id:9405,x:34604,y:34435,varname:node_9405,prsc:2|IN-9141-XYZ;n:type:ShaderForge.SFN_DDY,id:2711,x:34604,y:34568,varname:node_2711,prsc:2|IN-9141-XYZ;n:type:ShaderForge.SFN_FragmentPosition,id:9141,x:34225,y:34510,varname:node_9141,prsc:2;n:type:ShaderForge.SFN_Normalize,id:704,x:34822,y:34435,varname:node_704,prsc:2|IN-9405-OUT;n:type:ShaderForge.SFN_Cross,id:7870,x:35074,y:34494,varname:node_7870,prsc:2|A-9849-OUT,B-704-OUT;n:type:ShaderForge.SFN_Normalize,id:2646,x:34601,y:34154,varname:node_2646,prsc:2|IN-7870-OUT;n:type:ShaderForge.SFN_Normalize,id:9849,x:34822,y:34568,varname:node_9849,prsc:2|IN-2711-OUT;n:type:ShaderForge.SFN_Lerp,id:1495,x:33993,y:33420,varname:node_1495,prsc:2|A-2607-OUT,B-2134-OUT,T-5635-OUT;n:type:ShaderForge.SFN_Slider,id:1762,x:35353,y:33337,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_1762,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6633,x:34917,y:34238,varname:node_6633,prsc:2|A-2646-OUT,B-7450-OUT;n:type:ShaderForge.SFN_Vector1,id:7450,x:34751,y:34293,varname:node_7450,prsc:2,v1:-1;n:type:ShaderForge.SFN_Slider,id:5992,x:34987,y:33669,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:node_5992,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector1,id:5635,x:33536,y:33408,varname:node_5635,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp01,id:6210,x:32994,y:32258,varname:node_6210,prsc:2|IN-5921-OUT;n:type:ShaderForge.SFN_Append,id:4812,x:32270,y:31976,varname:node_4812,prsc:2|A-3361-RGB,B-3361-A;n:type:ShaderForge.SFN_Round,id:5921,x:32994,y:32112,varname:node_5921,prsc:2|IN-1979-OUT;n:type:ShaderForge.SFN_Vector3,id:9897,x:32977,y:33043,varname:node_9897,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Multiply,id:1979,x:32742,y:31946,varname:node_1979,prsc:2|A-4812-OUT,B-7369-OUT;n:type:ShaderForge.SFN_Vector1,id:7369,x:32688,y:32121,varname:node_7369,prsc:2,v1:2;n:type:ShaderForge.SFN_Color,id:757,x:34987,y:33496,ptovrint:False,ptlb:Specular Color,ptin:_SpecularColor,varname:node_757,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5439,x:35363,y:33476,varname:node_5439,prsc:2|A-757-RGB,B-5992-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:8985,x:36066,y:33892,varname:node_8985,prsc:2;n:type:ShaderForge.SFN_LightVector,id:2213,x:36066,y:33750,varname:node_2213,prsc:2;n:type:ShaderForge.SFN_Relay,id:6167,x:35116,y:33944,varname:node_6167,prsc:2|IN-1495-OUT;n:type:ShaderForge.SFN_Dot,id:7013,x:36356,y:33815,varname:node_7013,prsc:2,dt:1|A-2213-OUT,B-8985-OUT;n:type:ShaderForge.SFN_Power,id:8960,x:36356,y:33602,varname:node_8960,prsc:2|VAL-7013-OUT,EXP-4878-OUT;n:type:ShaderForge.SFN_RemapRange,id:7556,x:35778,y:33591,varname:node_7556,prsc:2,frmn:0,frmx:1,tomn:1,tomx:11|IN-1762-OUT;n:type:ShaderForge.SFN_Exp,id:4878,x:36066,y:33595,varname:node_4878,prsc:2,et:1|IN-7556-OUT;n:type:ShaderForge.SFN_Multiply,id:8967,x:36745,y:33715,varname:node_8967,prsc:2|A-8960-OUT,B-5439-OUT;n:type:ShaderForge.SFN_Add,id:5847,x:35613,y:34375,varname:node_5847,prsc:2|A-6167-OUT,B-8967-OUT;n:type:ShaderForge.SFN_Multiply,id:8278,x:35879,y:34384,varname:node_8278,prsc:2|A-5847-OUT,B-5573-RGB,C-3902-OUT,D-936-OUT;n:type:ShaderForge.SFN_LightColor,id:5573,x:35613,y:34524,varname:node_5573,prsc:2;n:type:ShaderForge.SFN_LightAttenuation,id:3902,x:35613,y:34653,varname:node_3902,prsc:2;n:type:ShaderForge.SFN_Dot,id:936,x:36389,y:34008,varname:node_936,prsc:2,dt:1|A-2213-OUT,B-9429-OUT;n:type:ShaderForge.SFN_NormalVector,id:9429,x:36075,y:34158,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:7015,x:35456,y:33835,varname:node_7015,prsc:2|A-9365-RGB,B-6167-OUT,C-7516-OUT;n:type:ShaderForge.SFN_AmbientLight,id:9365,x:35057,y:33812,varname:node_9365,prsc:2;n:type:ShaderForge.SFN_Vector1,id:7516,x:35431,y:33722,varname:node_7516,prsc:2,v1:1.2;n:type:ShaderForge.SFN_If,id:3521,x:35037,y:34045,varname:node_3521,prsc:2|A-1024-OUT,B-5135-OUT,GT-6633-OUT,EQ-2646-OUT,LT-2646-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:1024,x:34572,y:33990,ptovrint:False,ptlb:Invert Normals,ptin:_InvertNormals,varname:node_1024,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_Vector1,id:5135,x:34593,y:34060,varname:node_5135,prsc:2,v1:0;proporder:786-6038-8406-7356-379-2714-3790-1762-757-5992-1024;pass:END;sub:END;*/

Shader "DCG/Nature/Lowpoly Terrain" {
    Properties {
        _Control ("Control", 2D) = "white" {}
        _Splat0 ("Splat0", 2D) = "white" {}
        _Splat1 ("Splat1", 2D) = "white" {}
        _Splat2 ("Splat2", 2D) = "white" {}
        _MainTex ("MainTex", 2D) = "white" {}
        _Splat3 ("Splat3", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Gloss ("Gloss", Range(0, 1)) = 0
        _SpecularColor ("Specular Color", Color) = (1,1,1,1)
        _Specular ("Specular", Range(0, 1)) = 0
        [MaterialToggle] _InvertNormals ("Invert Normals", Float ) = 0
    }
    SubShader {
        Tags {
            "Queue"="Background"
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _Control; uniform float4 _Control_ST;
            uniform sampler2D _Splat0; uniform float4 _Splat0_ST;
            uniform sampler2D _Splat1; uniform float4 _Splat1_ST;
            uniform sampler2D _Splat2; uniform float4 _Splat2_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Splat3; uniform float4 _Splat3_ST;
            uniform float4 _Color;
            uniform float _Gloss;
            uniform float _Specular;
            uniform float4 _SpecularColor;
            uniform fixed _InvertNormals;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float node_3521_if_leA = step(_InvertNormals,0.0);
                float node_3521_if_leB = step(0.0,_InvertNormals);
                float3 node_2646 = normalize(cross(normalize(ddy(i.posWorld.rgb)),normalize(ddx(i.posWorld.rgb))));
                float3 normalDirection = lerp((node_3521_if_leA*node_2646)+(node_3521_if_leB*(node_2646*(-1.0))),node_2646,node_3521_if_leA*node_3521_if_leB);
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 node_4497 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_3361 = tex2D(_Control,TRANSFORM_TEX(i.uv0, _Control));
                float4 node_6210 = saturate(round((float4(node_3361.rgb,node_3361.a)*2.0)));
                float4 node_5986 = tex2D(_Splat0,TRANSFORM_TEX(i.uv0, _Splat0));
                float4 node_5603 = tex2D(_Splat1,TRANSFORM_TEX(i.uv0, _Splat1));
                float4 node_8836 = tex2D(_Splat2,TRANSFORM_TEX(i.uv0, _Splat2));
                float4 node_741 = tex2D(_Splat3,TRANSFORM_TEX(i.uv0, _Splat3));
                float3 node_6167 = lerp(lerp(node_4497.rgb,_Color.rgb,0.0),(lerp( lerp( lerp( lerp( float3(0,0,0), node_5986.rgb, node_6210.r ), node_5603.rgb, node_6210.g ), node_8836.rgb, node_6210.b ), node_741.rgb, node_6210.a )),1.0);
                float3 emissive = (UNITY_LIGHTMODEL_AMBIENT.rgb*node_6167*1.2);
                float3 finalColor = emissive + ((node_6167+(pow(max(0,dot(lightDirection,viewReflectDirection)),exp2((_Gloss*10.0+1.0)))*(_SpecularColor.rgb*_Specular)))*_LightColor0.rgb*attenuation*max(0,dot(lightDirection,normalDirection)));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _Control; uniform float4 _Control_ST;
            uniform sampler2D _Splat0; uniform float4 _Splat0_ST;
            uniform sampler2D _Splat1; uniform float4 _Splat1_ST;
            uniform sampler2D _Splat2; uniform float4 _Splat2_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Splat3; uniform float4 _Splat3_ST;
            uniform float4 _Color;
            uniform float _Gloss;
            uniform float _Specular;
            uniform float4 _SpecularColor;
            uniform fixed _InvertNormals;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float node_3521_if_leA = step(_InvertNormals,0.0);
                float node_3521_if_leB = step(0.0,_InvertNormals);
                float3 node_2646 = normalize(cross(normalize(ddy(i.posWorld.rgb)),normalize(ddx(i.posWorld.rgb))));
                float3 normalDirection = lerp((node_3521_if_leA*node_2646)+(node_3521_if_leB*(node_2646*(-1.0))),node_2646,node_3521_if_leA*node_3521_if_leB);
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 node_4497 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_3361 = tex2D(_Control,TRANSFORM_TEX(i.uv0, _Control));
                float4 node_6210 = saturate(round((float4(node_3361.rgb,node_3361.a)*2.0)));
                float4 node_5986 = tex2D(_Splat0,TRANSFORM_TEX(i.uv0, _Splat0));
                float4 node_5603 = tex2D(_Splat1,TRANSFORM_TEX(i.uv0, _Splat1));
                float4 node_8836 = tex2D(_Splat2,TRANSFORM_TEX(i.uv0, _Splat2));
                float4 node_741 = tex2D(_Splat3,TRANSFORM_TEX(i.uv0, _Splat3));
                float3 node_6167 = lerp(lerp(node_4497.rgb,_Color.rgb,0.0),(lerp( lerp( lerp( lerp( float3(0,0,0), node_5986.rgb, node_6210.r ), node_5603.rgb, node_6210.g ), node_8836.rgb, node_6210.b ), node_741.rgb, node_6210.a )),1.0);
                float3 finalColor = ((node_6167+(pow(max(0,dot(lightDirection,viewReflectDirection)),exp2((_Gloss*10.0+1.0)))*(_SpecularColor.rgb*_Specular)))*_LightColor0.rgb*attenuation*max(0,dot(lightDirection,normalDirection)));
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #include "UnityCG.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _Control; uniform float4 _Control_ST;
            uniform sampler2D _Splat0; uniform float4 _Splat0_ST;
            uniform sampler2D _Splat1; uniform float4 _Splat1_ST;
            uniform sampler2D _Splat2; uniform float4 _Splat2_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Splat3; uniform float4 _Splat3_ST;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_4497 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_3361 = tex2D(_Control,TRANSFORM_TEX(i.uv0, _Control));
                float4 node_6210 = saturate(round((float4(node_3361.rgb,node_3361.a)*2.0)));
                float4 node_5986 = tex2D(_Splat0,TRANSFORM_TEX(i.uv0, _Splat0));
                float4 node_5603 = tex2D(_Splat1,TRANSFORM_TEX(i.uv0, _Splat1));
                float4 node_8836 = tex2D(_Splat2,TRANSFORM_TEX(i.uv0, _Splat2));
                float4 node_741 = tex2D(_Splat3,TRANSFORM_TEX(i.uv0, _Splat3));
                float3 node_6167 = lerp(lerp(node_4497.rgb,_Color.rgb,0.0),(lerp( lerp( lerp( lerp( float3(0,0,0), node_5986.rgb, node_6210.r ), node_5603.rgb, node_6210.g ), node_8836.rgb, node_6210.b ), node_741.rgb, node_6210.a )),1.0);
                o.Emission = (UNITY_LIGHTMODEL_AMBIENT.rgb*node_6167*1.2);
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    //CustomEditor "ShaderForgeMaterialInspector"
}
