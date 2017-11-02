// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:True,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:2,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:False,qofs:0,qpre:0,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:7713,x:35601,y:34028,varname:node_7713,prsc:2|diff-1495-OUT,spec-5992-OUT,gloss-1762-OUT,normal-8217-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:786,x:32089,y:32260,ptovrint:False,ptlb:Control,ptin:_Control,varname:node_786,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3361,x:32075,y:31976,varname:node_3361,prsc:2,ntxv:0,isnm:False|TEX-786-TEX;n:type:ShaderForge.SFN_Tex2d,id:5986,x:31498,y:32302,varname:node_5986,prsc:2,ntxv:0,isnm:False|TEX-6038-TEX;n:type:ShaderForge.SFN_Tex2d,id:5603,x:31484,y:32484,varname:node_5603,prsc:2,ntxv:0,isnm:False|TEX-8406-TEX;n:type:ShaderForge.SFN_Tex2d,id:8836,x:31484,y:32675,varname:node_8836,prsc:2,ntxv:0,isnm:False|TEX-7356-TEX;n:type:ShaderForge.SFN_Tex2d,id:741,x:31484,y:32870,varname:node_741,prsc:2,ntxv:0,isnm:False|TEX-2714-TEX;n:type:ShaderForge.SFN_Tex2d,id:4497,x:32976,y:33382,varname:node_4497,prsc:2,ntxv:0,isnm:False|TEX-379-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:6038,x:31264,y:32356,ptovrint:False,ptlb:Splat0,ptin:_Splat0,varname:node_6038,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:8406,x:31264,y:32543,ptovrint:False,ptlb:Splat1,ptin:_Splat1,varname:node_8406,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:7356,x:31264,y:32778,ptovrint:False,ptlb:Splat2,ptin:_Splat2,varname:node_7356,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:379,x:32729,y:33382,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_379,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:2714,x:31252,y:32977,ptovrint:False,ptlb:Splat3,ptin:_Splat3,varname:node_2714,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ChannelBlend,id:2134,x:33516,y:32894,varname:node_2134,prsc:2,chbt:1|M-6210-OUT,R-5986-RGB,G-5603-RGB,B-8836-RGB,A-741-RGB,BTM-9897-OUT;n:type:ShaderForge.SFN_Color,id:3790,x:32939,y:33637,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_3790,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:3040,x:33361,y:33302,varname:node_3040,prsc:2,v1:0;n:type:ShaderForge.SFN_Lerp,id:2607,x:33575,y:33606,varname:node_2607,prsc:2|A-4497-RGB,B-3790-RGB,T-3040-OUT;n:type:ShaderForge.SFN_DDX,id:9405,x:34604,y:34435,varname:node_9405,prsc:2|IN-9141-XYZ;n:type:ShaderForge.SFN_DDY,id:2711,x:34604,y:34568,varname:node_2711,prsc:2|IN-9141-XYZ;n:type:ShaderForge.SFN_FragmentPosition,id:9141,x:34225,y:34510,varname:node_9141,prsc:2;n:type:ShaderForge.SFN_Normalize,id:704,x:34822,y:34435,varname:node_704,prsc:2|IN-9405-OUT;n:type:ShaderForge.SFN_Cross,id:7870,x:35074,y:34494,varname:node_7870,prsc:2|A-9849-OUT,B-704-OUT;n:type:ShaderForge.SFN_Normalize,id:9849,x:34822,y:34568,varname:node_9849,prsc:2|IN-2711-OUT;n:type:ShaderForge.SFN_Lerp,id:1495,x:33886,y:33376,varname:node_1495,prsc:2|A-2607-OUT,B-2134-OUT,T-5635-OUT;n:type:ShaderForge.SFN_Slider,id:1762,x:35233,y:33582,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_1762,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:5992,x:35233,y:33679,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_5992,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector1,id:5635,x:33536,y:33408,varname:node_5635,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp01,id:6210,x:32994,y:32258,varname:node_6210,prsc:2|IN-5921-OUT;n:type:ShaderForge.SFN_Append,id:4812,x:32270,y:31976,varname:node_4812,prsc:2|A-3361-RGB,B-3361-A;n:type:ShaderForge.SFN_Round,id:5921,x:32994,y:32112,varname:node_5921,prsc:2|IN-1979-OUT;n:type:ShaderForge.SFN_Vector3,id:9897,x:33516,y:33052,varname:node_9897,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Multiply,id:1979,x:32552,y:31978,varname:node_1979,prsc:2|A-4812-OUT,B-7369-OUT;n:type:ShaderForge.SFN_Vector1,id:7369,x:32393,y:32149,varname:node_7369,prsc:2,v1:2;n:type:ShaderForge.SFN_Normalize,id:2301,x:35316,y:34460,varname:node_2301,prsc:2|IN-7870-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:9056,x:35045,y:34190,ptovrint:False,ptlb:Invert Normals,ptin:_InvertNormals,varname:node_9056,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_If,id:8217,x:35299,y:34120,varname:node_8217,prsc:2|A-9056-OUT,B-9015-OUT,GT-1809-OUT,EQ-2301-OUT,LT-2301-OUT;n:type:ShaderForge.SFN_Vector1,id:9015,x:35025,y:34084,varname:node_9015,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:1809,x:35581,y:34629,varname:node_1809,prsc:2|A-2301-OUT,B-1450-OUT;n:type:ShaderForge.SFN_Vector1,id:1450,x:35377,y:34707,varname:node_1450,prsc:2,v1:-1;proporder:786-6038-8406-7356-379-2714-3790-1762-5992-9056;pass:END;sub:END;*/

Shader "DCG/Nature/Lowpoly Terrain PBL" {
    Properties {
        _Control ("Control", 2D) = "white" {}
        _Splat0 ("Splat0", 2D) = "white" {}
        _Splat1 ("Splat1", 2D) = "white" {}
        _Splat2 ("Splat2", 2D) = "white" {}
        _MainTex ("MainTex", 2D) = "white" {}
        _Splat3 ("Splat3", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Gloss ("Gloss", Range(0, 1)) = 0
        _Metallic ("Metallic", Range(0, 1)) = 0
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
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
            uniform float _Metallic;
            uniform fixed _InvertNormals;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float node_8217_if_leA = step(_InvertNormals,0.0);
                float node_8217_if_leB = step(0.0,_InvertNormals);
                float3 node_2301 = normalize(cross(normalize(ddy(i.posWorld.rgb)),normalize(ddx(i.posWorld.rgb))));
                float3 normalDirection = lerp((node_8217_if_leA*node_2301)+(node_8217_if_leB*(node_2301*(-1.0))),node_2301,node_8217_if_leA*node_8217_if_leB);
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float4 node_4497 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_3361 = tex2D(_Control,TRANSFORM_TEX(i.uv0, _Control));
                float4 node_6210 = saturate(round((float4(node_3361.rgb,node_3361.a)*2.0)));
                float4 node_5986 = tex2D(_Splat0,TRANSFORM_TEX(i.uv0, _Splat0));
                float4 node_5603 = tex2D(_Splat1,TRANSFORM_TEX(i.uv0, _Splat1));
                float4 node_8836 = tex2D(_Splat2,TRANSFORM_TEX(i.uv0, _Splat2));
                float4 node_741 = tex2D(_Splat3,TRANSFORM_TEX(i.uv0, _Splat3));
                float3 diffuseColor = lerp(lerp(node_4497.rgb,_Color.rgb,0.0),(lerp( lerp( lerp( lerp( float3(0,0,0), node_5986.rgb, node_6210.r ), node_5603.rgb, node_6210.g ), node_8836.rgb, node_6210.b ), node_741.rgb, node_6210.a )),1.0); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz)*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
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
            uniform float _Metallic;
            uniform fixed _InvertNormals;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float node_8217_if_leA = step(_InvertNormals,0.0);
                float node_8217_if_leB = step(0.0,_InvertNormals);
                float3 node_2301 = normalize(cross(normalize(ddy(i.posWorld.rgb)),normalize(ddx(i.posWorld.rgb))));
                float3 normalDirection = lerp((node_8217_if_leA*node_2301)+(node_8217_if_leB*(node_2301*(-1.0))),node_2301,node_8217_if_leA*node_8217_if_leB);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float4 node_4497 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_3361 = tex2D(_Control,TRANSFORM_TEX(i.uv0, _Control));
                float4 node_6210 = saturate(round((float4(node_3361.rgb,node_3361.a)*2.0)));
                float4 node_5986 = tex2D(_Splat0,TRANSFORM_TEX(i.uv0, _Splat0));
                float4 node_5603 = tex2D(_Splat1,TRANSFORM_TEX(i.uv0, _Splat1));
                float4 node_8836 = tex2D(_Splat2,TRANSFORM_TEX(i.uv0, _Splat2));
                float4 node_741 = tex2D(_Splat3,TRANSFORM_TEX(i.uv0, _Splat3));
                float3 diffuseColor = lerp(lerp(node_4497.rgb,_Color.rgb,0.0),(lerp( lerp( lerp( lerp( float3(0,0,0), node_5986.rgb, node_6210.r ), node_5603.rgb, node_6210.g ), node_8836.rgb, node_6210.b ), node_741.rgb, node_6210.a )),1.0); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
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
            uniform float _Metallic;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 node_4497 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_3361 = tex2D(_Control,TRANSFORM_TEX(i.uv0, _Control));
                float4 node_6210 = saturate(round((float4(node_3361.rgb,node_3361.a)*2.0)));
                float4 node_5986 = tex2D(_Splat0,TRANSFORM_TEX(i.uv0, _Splat0));
                float4 node_5603 = tex2D(_Splat1,TRANSFORM_TEX(i.uv0, _Splat1));
                float4 node_8836 = tex2D(_Splat2,TRANSFORM_TEX(i.uv0, _Splat2));
                float4 node_741 = tex2D(_Splat3,TRANSFORM_TEX(i.uv0, _Splat3));
                float3 diffColor = lerp(lerp(node_4497.rgb,_Color.rgb,0.0),(lerp( lerp( lerp( lerp( float3(0,0,0), node_5986.rgb, node_6210.r ), node_5603.rgb, node_6210.g ), node_8836.rgb, node_6210.b ), node_741.rgb, node_6210.a )),1.0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _Metallic, specColor, specularMonochrome );
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    //CustomEditor "ShaderForgeMaterialInspector"
}
