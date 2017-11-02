// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LightingBox/Terrain/Terrain 5-Layers"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_TessValue( "Max Tessellation", Range( 1, 32 ) ) = 10
		_TessMin( "Tess Min Distance", Float ) = 30
		_TessMax( "Tess Max Distance", Float ) = 30
		_Float1("Float 1", Range( 0 , 10)) = 0.13
		_Control_2("Control_2", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample3("Texture Sample 3", 2D) = "bump" {}
		_Control_1("Control_1", 2D) = "white" {}
		_Layer_0("Layer_0", 2D) = "white" {}
		_Normal_0("Normal_0", 2D) = "bump" {}
		_Smoothness_0("Smoothness_0", Range( 0 , 10)) = 0.3
		_Normal_Power_0("Normal_Power_0", Range( 0 , 1)) = 1
		_Displacement_0("Displacement_0", Range( 0 , 3)) = 1
		_Layer_1("Layer_1", 2D) = "white" {}
		_Normal_1("Normal_1", 2D) = "bump" {}
		_Smoothness_1("Smoothness_1", Range( 0 , 10)) = 0.3
		_Normal_Power_1("Normal_Power_1", Range( 0 , 1)) = 1
		_Displacement_1("Displacement_1", Range( 0 , 3)) = 1
		_Layer_2("Layer_2", 2D) = "white" {}
		_Normal_2("Normal_2", 2D) = "bump" {}
		_Smoothness_2("Smoothness_2", Range( 0 , 10)) = 0.3
		_Normal_Power_2("Normal_Power_2", Range( 0 , 1)) = 1
		_Displacement_2("Displacement_2", Range( 0 , 3)) = 1
		_Layer_3("Layer_3", 2D) = "white" {}
		_Normal_3("Normal_3", 2D) = "bump" {}
		_Smoothness_3("Smoothness_3", Range( 0 , 10)) = 0.3
		_Normal_Power_3("Normal_Power_3", Range( 0 , 1)) = 1
		_Displacement_3("Displacement_3", Range( 0 , 3)) = 1
		_Layer_4("Layer_4", 2D) = "white" {}
		_Smoothness_4("Smoothness_4", Range( 0 , 10)) = 0.3
		_Displacement_4("Displacement_4", Range( 0 , 3)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "Tessellation.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.6
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
		};

		struct appdata
		{
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
			float4 texcoord2 : TEXCOORD2;
			float4 texcoord3 : TEXCOORD3;
			fixed4 color : COLOR;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		uniform sampler2D _Control_1;
		uniform float4 _Control_1_ST;
		uniform float _Normal_Power_0;
		uniform sampler2D _Normal_0;
		uniform float4 _Normal_0_ST;
		uniform float _Normal_Power_1;
		uniform sampler2D _Normal_1;
		uniform float4 _Normal_1_ST;
		uniform float _Normal_Power_2;
		uniform sampler2D _Normal_2;
		uniform float4 _Normal_2_ST;
		uniform float _Normal_Power_3;
		uniform sampler2D _Normal_3;
		uniform float4 _Normal_3_ST;
		uniform sampler2D _TextureSample3;
		uniform float4 _TextureSample3_ST;
		uniform float _Float1;
		uniform sampler2D _Layer_0;
		uniform float4 _Layer_0_ST;
		uniform sampler2D _Layer_1;
		uniform float4 _Layer_1_ST;
		uniform sampler2D _Layer_2;
		uniform float4 _Layer_2_ST;
		uniform sampler2D _Layer_3;
		uniform float4 _Layer_3_ST;
		uniform sampler2D _Control_2;
		uniform float4 _Control_2_ST;
		uniform sampler2D _Layer_4;
		uniform float4 _Layer_4_ST;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;
		uniform float _Smoothness_0;
		uniform float _Smoothness_1;
		uniform float _Smoothness_2;
		uniform float _Smoothness_3;
		uniform float _Smoothness_4;
		uniform float _Displacement_0;
		uniform float _Displacement_1;
		uniform float _Displacement_2;
		uniform float _Displacement_3;
		uniform float _Displacement_4;
		uniform float _TessValue;
		uniform float _TessMin;
		uniform float _TessMax;

		float4 tessFunction( appdata v0, appdata v1, appdata v2 )
		{
			return UnityDistanceBasedTess( v0.vertex, v1.vertex, v2.vertex, _TessMin, _TessMax, _TessValue );
		}

		void vertexDataFunc( inout appdata v )
		{
			float4 uv_Control_1 = float4(v.texcoord * _Control_1_ST.xy + _Control_1_ST.zw, 0 ,0);
			float4 tex2DNode1 = tex2Dlod( _Control_1, uv_Control_1 );
			float4 uv_Control_2 = float4(v.texcoord * _Control_2_ST.xy + _Control_2_ST.zw, 0 ,0);
			float4 tex2DNode2 = tex2Dlod( _Control_2, uv_Control_2 );
			float4 uv_Layer_0 = float4(v.texcoord * _Layer_0_ST.xy + _Layer_0_ST.zw, 0 ,0);
			float4 tex2DNode3 = tex2Dlod( _Layer_0, uv_Layer_0 );
			float4 uv_Layer_1 = float4(v.texcoord * _Layer_1_ST.xy + _Layer_1_ST.zw, 0 ,0);
			float4 tex2DNode4 = tex2Dlod( _Layer_1, uv_Layer_1 );
			float4 uv_Layer_2 = float4(v.texcoord * _Layer_2_ST.xy + _Layer_2_ST.zw, 0 ,0);
			float4 tex2DNode5 = tex2Dlod( _Layer_2, uv_Layer_2 );
			float4 uv_Layer_3 = float4(v.texcoord * _Layer_3_ST.xy + _Layer_3_ST.zw, 0 ,0);
			float4 tex2DNode6 = tex2Dlod( _Layer_3, uv_Layer_3 );
			float4 uv_Layer_4 = float4(v.texcoord * _Layer_4_ST.xy + _Layer_4_ST.zw, 0 ,0);
			float4 tex2DNode97 = tex2Dlod( _Layer_4, uv_Layer_4 );
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( ( ( ( ( ( ( tex2DNode1.r * _Displacement_0 ) + ( tex2DNode1.g * _Displacement_1 ) ) + ( tex2DNode1.b * _Displacement_2 ) ) + ( tex2DNode1.a * _Displacement_3 ) ) + ( tex2DNode2.r * _Displacement_4 ) ) * ( ( ( ( ( tex2DNode1.r * tex2DNode3.a ) + ( tex2DNode1.g * tex2DNode4.a ) ) + ( tex2DNode1.b * tex2DNode5.a ) ) + ( tex2DNode1.a * tex2DNode6.a ) ) + ( tex2DNode2.r * tex2DNode97.a ) ) ) * ase_vertexNormal );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Control_1 = i.uv_texcoord * _Control_1_ST.xy + _Control_1_ST.zw;
			float4 tex2DNode1 = tex2D( _Control_1, uv_Control_1 );
			float2 uv_Normal_0 = i.uv_texcoord * _Normal_0_ST.xy + _Normal_0_ST.zw;
			float2 uv_Normal_1 = i.uv_texcoord * _Normal_1_ST.xy + _Normal_1_ST.zw;
			float2 uv_Normal_2 = i.uv_texcoord * _Normal_2_ST.xy + _Normal_2_ST.zw;
			float2 uv_Normal_3 = i.uv_texcoord * _Normal_3_ST.xy + _Normal_3_ST.zw;
			float2 uv_TextureSample3 = i.uv_texcoord * _TextureSample3_ST.xy + _TextureSample3_ST.zw;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 lerpResult114 = lerp( ( ( ( ( tex2DNode1.r * UnpackScaleNormal( tex2D( _Normal_0, uv_Normal_0 ) ,_Normal_Power_0 ) ) + ( tex2DNode1.g * UnpackScaleNormal( tex2D( _Normal_1, uv_Normal_1 ) ,_Normal_Power_1 ) ) ) + ( tex2DNode1.b * UnpackScaleNormal( tex2D( _Normal_2, uv_Normal_2 ) ,_Normal_Power_2 ) ) ) + ( tex2DNode1.a * UnpackScaleNormal( tex2D( _Normal_3, uv_Normal_3 ) ,_Normal_Power_3 ) ) ) , UnpackNormal( tex2D( _TextureSample3, uv_TextureSample3 ) ) , saturate( ( ase_worldNormal.y * _Float1 ) ));
			o.Normal = lerpResult114;
			float2 uv_Layer_0 = i.uv_texcoord * _Layer_0_ST.xy + _Layer_0_ST.zw;
			float4 tex2DNode3 = tex2D( _Layer_0, uv_Layer_0 );
			float2 uv_Layer_1 = i.uv_texcoord * _Layer_1_ST.xy + _Layer_1_ST.zw;
			float4 tex2DNode4 = tex2D( _Layer_1, uv_Layer_1 );
			float2 uv_Layer_2 = i.uv_texcoord * _Layer_2_ST.xy + _Layer_2_ST.zw;
			float4 tex2DNode5 = tex2D( _Layer_2, uv_Layer_2 );
			float2 uv_Layer_3 = i.uv_texcoord * _Layer_3_ST.xy + _Layer_3_ST.zw;
			float4 tex2DNode6 = tex2D( _Layer_3, uv_Layer_3 );
			float2 uv_Control_2 = i.uv_texcoord * _Control_2_ST.xy + _Control_2_ST.zw;
			float4 tex2DNode2 = tex2D( _Control_2, uv_Control_2 );
			float2 uv_Layer_4 = i.uv_texcoord * _Layer_4_ST.xy + _Layer_4_ST.zw;
			float4 tex2DNode97 = tex2D( _Layer_4, uv_Layer_4 );
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float4 lerpResult115 = lerp( ( ( ( ( ( tex2DNode1.r * tex2DNode3 ) + ( tex2DNode1.g * tex2DNode4 ) ) + ( tex2DNode1.b * tex2DNode5 ) ) + ( tex2DNode1.a * tex2DNode6 ) ) + ( tex2DNode2.r * tex2DNode97 ) ) , tex2D( _TextureSample1, uv_TextureSample1 ) , saturate( ( WorldNormalVector( i , lerpResult114 ).y * _Float1 ) ));
			o.Albedo = lerpResult115.xyz;
			o.Smoothness = ( ( ( ( ( tex2DNode1.r * ( tex2DNode3.b * _Smoothness_0 ) ) + ( tex2DNode1.g * ( tex2DNode4.b * _Smoothness_1 ) ) ) + ( tex2DNode1.b * ( tex2DNode5.b * _Smoothness_2 ) ) ) + ( tex2DNode1.a * ( tex2DNode6.b * _Smoothness_3 ) ) ) + ( tex2DNode2.r * ( tex2DNode97.b * _Smoothness_4 ) ) );
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma exclude_renderers d3d9 gles gles3 xbox360 psp2 n3ds 
		#pragma surface surf Standard keepalpha fullforwardshadows novertexlights nodirlightmap noforwardadd vertex:vertexDataFunc tessellate:tessFunction 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD6;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				float4 texcoords01 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord.xy = IN.texcoords01.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
544;92;480;614;892.1119;1966.723;1.555329;False;False
Node;AmplifyShaderEditor.CommentaryNode;56;-2428.5,-2203.928;Float;False;1706.762;1397.105;Comment;15;17;16;15;14;31;25;30;24;26;22;23;21;20;19;18;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-2283.958,-1807.4;Float;False;Property;_Normal_Power_1;Normal_Power_1;20;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;18;-2286.729,-2059.014;Float;False;Property;_Normal_Power_0;Normal_Power_0;15;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;20;-2293.749,-1545.451;Float;False;Property;_Normal_Power_2;Normal_Power_2;25;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;14;-1905.511,-2103.928;Float;True;Property;_Normal_0;Normal_0;13;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;15;-1905.512,-1853.877;Float;True;Property;_Normal_1;Normal_1;18;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-3737.093,-407.5356;Float;True;Property;_Control_1;Control_1;11;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;126;-561.8358,-2101.683;Float;False;1905.907;812.8562;Comment;11;124;123;122;121;120;118;117;116;115;114;119;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;111;-2254.355,2349.72;Float;False;1326.35;687.4788;Displacement Power;14;80;78;77;76;82;74;75;73;70;69;72;71;67;68;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;35;-2020.435,-764.8676;Float;False;1217.122;1272.594;Splats;14;12;13;101;99;97;6;5;4;3;10;11;9;7;8;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1448.315,-1820.487;Float;False;2;2;0;FLOAT;0,0,0;False;1;FLOAT3;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1457.183,-2056.376;Float;False;2;2;0;FLOAT;0,0,0;False;1;FLOAT3;0;False;1;FLOAT3
Node;AmplifyShaderEditor.RangedFloatNode;21;-2306.851,-1258.979;Float;False;Property;_Normal_Power_3;Normal_Power_3;30;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;16;-1905.512,-1589.622;Float;True;Property;_Normal_2;Normal_2;23;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;65;-1883.934,1511.389;Float;False;777.8558;758.0242;Displacement;9;87;64;85;63;61;60;62;59;58;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-2202.726,2420.167;Float;False;Property;_Displacement_0;Displacement_0;16;0;1;0;3;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;68;-2206.543,2544.07;Float;False;Property;_Displacement_1;Displacement_1;21;0;1;0;3;0;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;55;-2042.811,567.6201;Float;False;1335.056;898.5645;Smoothness;19;95;46;91;90;43;45;50;42;44;89;40;49;54;41;53;48;47;52;51;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;4;-1967.139,-468.3163;Float;True;Property;_Layer_1;Layer_1;17;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;3;-1963.813,-714.8676;Float;True;Property;_Layer_0;Layer_0;12;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldNormalVector;121;-529.1725,-2045.279;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;120;-605.4456,-1757.465;Float;False;Property;_Float1;Float 1;6;0;0.13;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-1230.16,-1964.15;Float;False;2;2;0;FLOAT3;0.0;False;1;FLOAT3;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1448.314,-1511.883;Float;False;2;2;0;FLOAT;0,0,0;False;1;FLOAT3;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.SamplerNode;17;-1902.67,-1311.811;Float;True;Property;_Normal_3;Normal_3;28;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;-1831.994,2524.742;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;51;-1984.229,639.9022;Float;False;Property;_Smoothness_0;Smoothness_0;14;0;0.3;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;5;-1964.569,-223.5932;Float;True;Property;_Layer_2;Layer_2;22;0;None;True;0;True;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;52;-1983.368,795.3799;Float;False;Property;_Smoothness_1;Smoothness_1;19;0;0.3;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;69;-2211.819,2660.087;Float;False;Property;_Displacement_2;Displacement_2;26;0;1;0;3;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-1844.295,1571.335;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1442.993,-1256.485;Float;False;2;2;0;FLOAT;0,0,0;False;1;FLOAT3;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-1091.115,-1627.238;Float;False;2;2;0;FLOAT3;0.0;False;1;FLOAT3;0.0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;122;-232.0931,-1815.665;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;-1838.973,2406.188;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-1842.694,1695.374;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;62;-1557.22,1605.209;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;6;-1959.748,44.37933;Float;True;Property;_Layer_3;Layer_3;27;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1561.946,-633.2185;Float;False;2;2;0;FLOAT;0.0,0,0,0;False;1;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-1833.009,2644.83;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;53;-1976.08,985.4167;Float;False;Property;_Smoothness_2;Smoothness_2;24;0;0.3;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-1838.912,1847.388;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-1578.376,622.6201;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-997.3843,-1323.883;Float;False;2;2;0;FLOAT3;0.0;False;1;FLOAT3;0.0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-1572.75,772.9169;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;123;-4.221691,-1814.12;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;124;-485.9503,-1503.402;Float;True;Property;_TextureSample3;Texture Sample 3;10;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;70;-2215.958,2784.062;Float;False;Property;_Displacement_3;Displacement_3;31;0;1;0;3;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;75;-1614.851,2454.006;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-1566.317,-416.1329;Float;False;2;2;0;FLOAT;0.0,0,0,0;False;1;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;82;-2212.237,2931.752;Float;False;Property;_Displacement_4;Displacement_4;36;0;0;0;3;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;54;-1974.497,1150.086;Float;False;Property;_Smoothness_3;Smoothness_3;29;0;0.3;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-1308.104,807.8271;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-3770.672,343.7685;Float;True;Property;_Control_2;Control_2;8;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-1836.408,1996.272;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;97;-1950.905,297.7488;Float;True;Property;_Layer_4;Layer_4;32;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;76;-1473.986,2574.117;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-1299.698,-538.5175;Float;False;2;2;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-1308.814,635.2728;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;114;130.8076,-1508.855;Float;False;3;0;FLOAT3;0.0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-1574.868,966.2974;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-1566.318,-164.0808;Float;False;2;2;0;FLOAT;0.0,0,0,0;False;1;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-1835.578,2768.297;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;63;-1480.697,1795.905;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-1575.001,1128.353;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;77;-1336.642,2734.037;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-1094.52,698.1492;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-1563.403,108.3689;Float;False;2;2;0;FLOAT;0.0,0,0,0;False;1;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-1307.571,990.9491;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;64;-1415.288,1969.577;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;89;-1972.157,1314.41;Float;False;Property;_Smoothness_4;Smoothness_4;34;0;0.3;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.WorldNormalVector;116;199.5871,-1814.719;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-1233.404,-190.0229;Float;False;2;2;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-1845.713,2145.582;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-1839.702,2914.703;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-1140.286,85.13793;Float;False;2;2;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleAddOpNode;87;-1328.307,2134.403;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-1569.953,1306.247;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;-1559.775,352.3984;Float;False;2;2;0;FLOAT;0.0,0,0,0;False;1;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-1303.93,1177.802;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;117;692.3412,-1567.701;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-1052.221,965.7276;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;80;-1197.07,2883.053;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;101;-1065.368,333.1642;Float;False;2;2;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-1001.166,1155.351;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-930.2366,2107.556;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-1310.995,1342.126;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;119;408.2125,-2058.619;Float;True;Property;_TextureSample1;Texture Sample 1;9;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SaturateNode;118;912.9636,-1797.495;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.NormalVertexDataNode;113;-277.4213,1890.597;Float;False;0;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;115;1089.704,-2030.035;Float;False;3;0;COLOR;0.0,0,0,0;False;1;FLOAT4;0.0,0,0,0;False;2;FLOAT;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-348.9623,1630.188;Float;False;2;2;0;FLOAT;0.0,0,0;False;1;FLOAT3;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;95;-955.905,1316.236;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2325.48,275.6877;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;LightingBox/Terrain/Terrain 5-Layers;False;False;False;False;False;True;False;False;True;False;False;True;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;False;True;True;False;False;True;True;False;True;True;False;False;True;True;True;True;True;False;0;255;255;0;0;0;0;True;0;10;30;30;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;Diffuse;-1;-1;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;5;18;0
WireConnection;15;5;19;0
WireConnection;23;0;1;2
WireConnection;23;1;15;0
WireConnection;22;0;1;1
WireConnection;22;1;14;0
WireConnection;16;5;20;0
WireConnection;26;0;22;0
WireConnection;26;1;23;0
WireConnection;24;0;1;3
WireConnection;24;1;16;0
WireConnection;17;5;21;0
WireConnection;72;0;1;2
WireConnection;72;1;68;0
WireConnection;58;0;1;1
WireConnection;58;1;3;4
WireConnection;25;0;1;4
WireConnection;25;1;17;0
WireConnection;30;0;26;0
WireConnection;30;1;24;0
WireConnection;122;0;121;2
WireConnection;122;1;120;0
WireConnection;71;0;1;1
WireConnection;71;1;67;0
WireConnection;59;0;1;2
WireConnection;59;1;4;4
WireConnection;62;0;58;0
WireConnection;62;1;59;0
WireConnection;7;0;1;1
WireConnection;7;1;3;0
WireConnection;73;0;1;3
WireConnection;73;1;69;0
WireConnection;60;0;1;3
WireConnection;60;1;5;4
WireConnection;47;0;3;3
WireConnection;47;1;51;0
WireConnection;31;0;30;0
WireConnection;31;1;25;0
WireConnection;48;0;4;3
WireConnection;48;1;52;0
WireConnection;123;0;122;0
WireConnection;75;0;71;0
WireConnection;75;1;72;0
WireConnection;8;0;1;2
WireConnection;8;1;4;0
WireConnection;41;0;1;2
WireConnection;41;1;48;0
WireConnection;61;0;1;4
WireConnection;61;1;6;4
WireConnection;76;0;75;0
WireConnection;76;1;73;0
WireConnection;11;0;7;0
WireConnection;11;1;8;0
WireConnection;40;0;1;1
WireConnection;40;1;47;0
WireConnection;114;0;31;0
WireConnection;114;1;124;0
WireConnection;114;2;123;0
WireConnection;49;0;5;3
WireConnection;49;1;53;0
WireConnection;9;0;1;3
WireConnection;9;1;5;0
WireConnection;74;0;1;4
WireConnection;74;1;70;0
WireConnection;63;0;62;0
WireConnection;63;1;60;0
WireConnection;50;0;6;3
WireConnection;50;1;54;0
WireConnection;77;0;76;0
WireConnection;77;1;74;0
WireConnection;44;0;40;0
WireConnection;44;1;41;0
WireConnection;10;0;1;4
WireConnection;10;1;6;0
WireConnection;42;0;1;3
WireConnection;42;1;49;0
WireConnection;64;0;63;0
WireConnection;64;1;61;0
WireConnection;116;0;114;0
WireConnection;12;0;11;0
WireConnection;12;1;9;0
WireConnection;85;0;2;1
WireConnection;85;1;97;4
WireConnection;78;0;2;1
WireConnection;78;1;82;0
WireConnection;13;0;12;0
WireConnection;13;1;10;0
WireConnection;87;0;64;0
WireConnection;87;1;85;0
WireConnection;90;0;97;3
WireConnection;90;1;89;0
WireConnection;99;0;2;1
WireConnection;99;1;97;0
WireConnection;43;0;1;4
WireConnection;43;1;50;0
WireConnection;117;0;116;2
WireConnection;117;1;120;0
WireConnection;45;0;44;0
WireConnection;45;1;42;0
WireConnection;80;0;77;0
WireConnection;80;1;78;0
WireConnection;101;0;13;0
WireConnection;101;1;99;0
WireConnection;46;0;45;0
WireConnection;46;1;43;0
WireConnection;66;0;80;0
WireConnection;66;1;87;0
WireConnection;91;0;2;1
WireConnection;91;1;90;0
WireConnection;118;0;117;0
WireConnection;115;0;101;0
WireConnection;115;1;119;0
WireConnection;115;2;118;0
WireConnection;112;0;66;0
WireConnection;112;1;113;0
WireConnection;95;0;46;0
WireConnection;95;1;91;0
WireConnection;0;0;115;0
WireConnection;0;1;114;0
WireConnection;0;4;95;0
WireConnection;0;11;112;0
ASEEND*/
//CHKSM=D430BED6DA5C6BF00D468F22C851497479CD13B8