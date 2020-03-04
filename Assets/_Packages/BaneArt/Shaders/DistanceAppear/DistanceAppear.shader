// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BaneArt/DistanceAppear"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 1
		_ColourA("Colour A", Color) = (1,0,0,1)
		_TextureA("Texture A", 2D) = "white" {}
		_ColourB("Colour B", Color) = (1,0,0,1)
		_TextureB("Texture B", 2D) = "white" {}
		[Toggle]_TransparentTextureSwapToggle("Transparent/Texture Swap Toggle", Float) = 1
		[Toggle]_AppearDisappearToggle("Appear/Disappear Toggle", Float) = 1
		_Radius("Radius", Range( 0 , 10)) = 5.588235
		_Strength("Strength", Range( 0 , 1)) = 1
		[Toggle]_ColourDiffuseToggle("Colour/Diffuse Toggle", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" }
		Cull Off
		CGPROGRAM
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float _ColourDiffuseToggle;
		uniform float4 _ColourA;
		uniform sampler2D _TextureA;
		uniform float4 _TextureA_ST;
		uniform float4 _ColourB;
		uniform sampler2D _TextureB;
		uniform float4 _TextureB_ST;
		uniform int ArrayLength;
		uniform float4 positionsArray[42];
		uniform float _Radius;
		uniform float _Strength;
		uniform float _TransparentTextureSwapToggle;
		uniform float _AppearDisappearToggle;
		uniform float _Cutoff = 1;


		float DistanceCheck18_g14( float3 WorldPos , int ArrayLength , float3 objectPosition )
		{
			float closest=10000;
			float now=0;
			for(int i=0; i<ArrayLength;i++)
			{
			  now = distance(WorldPos,positionsArray[i]);
			  if(now < closest)
			  {
			    closest = now;
			  }
			}
			return closest;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureA = i.uv_texcoord * _TextureA_ST.xy + _TextureA_ST.zw;
			float2 uv_TextureB = i.uv_texcoord * _TextureB_ST.xy + _TextureB_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float3 WorldPos18_g14 = ase_worldPos;
			int ArrayLength18_g14 = ArrayLength;
			float3 objectPosition18_g14 = positionsArray[clamp(0,0,(42 - 1))].xyz;
			float localDistanceCheck18_g14 = DistanceCheck18_g14( WorldPos18_g14 , ArrayLength18_g14 , objectPosition18_g14 );
			float clampResult10_g14 = clamp( pow( ( localDistanceCheck18_g14 / _Radius ) , (0.0 + (_Strength - 0.0) * (100.0 - 0.0) / (1.0 - 0.0)) ) , 0.0 , 1.0 );
			float TextureBlend136 = clampResult10_g14;
			float4 lerpResult143 = lerp( lerp(_ColourA,tex2D( _TextureA, uv_TextureA ),_ColourDiffuseToggle) , lerp(_ColourB,tex2D( _TextureB, uv_TextureB ),_ColourDiffuseToggle) , TextureBlend136);
			o.Albedo = lerpResult143.rgb;
			o.Alpha = 1;
			float lerpResult129 = lerp( lerp(1.0,0.0,_AppearDisappearToggle) , ( 1.0 - lerp(1.0,0.0,_AppearDisappearToggle) ) , TextureBlend136);
			clip( lerp(1.0,lerpResult129,_TransparentTextureSwapToggle) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
1972;43;1049;728;841.7753;320.4794;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;57;-1660.982,-127.6296;Inherit;False;Property;_Strength;Strength;9;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-1466.869,-213.3909;Inherit;False;Property;_Radius;Radius;8;0;Create;True;0;0;False;0;5.588235;1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;65;-1378.138,-120.961;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;100;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;124;-1134.73,-106.0373;Inherit;False;DistanceBlendObjectArray;-1;;14;5b9aec6b10979884f8f92cd3b2f819e8;0;2;20;FLOAT;0;False;21;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;132;-418.5783,130.0729;Inherit;False;Property;_AppearDisappearToggle;Appear/Disappear Toggle;7;0;Create;True;0;0;False;0;1;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;136;-834.1364,-114.9842;Inherit;False;TextureBlend;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;138;-132.5253,131.0016;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-80.55658,-704.4775;Inherit;False;Property;_ColourA;Colour A;2;0;Create;True;0;0;False;0;1,0,0,1;0.005348554,0.6509434,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;141;-94.74666,-324.1948;Inherit;False;Property;_ColourB;Colour B;4;0;Create;True;0;0;False;0;1,0,0,1;0.6509804,0.01420808,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;142;-177.1678,-142.7466;Inherit;True;Property;_TextureB;Texture B;5;0;Create;True;0;0;False;0;None;84508b93f15f2b64386ec07486afc7a3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;127;-162.9778,-523.0292;Inherit;True;Property;_TextureA;Texture A;3;0;Create;True;0;0;False;0;None;c68296334e691ed45b62266cbc716628;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;137;-166.7916,243.6382;Inherit;False;136;TextureBlend;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;139;24.47477,134.0016;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;134;-113.8998,162.9053;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;140;182.8592,-309.1602;Inherit;False;Property;_ColourDiffuseToggle;Colour/Diffuse Toggle;1;0;Create;True;0;0;False;0;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;144;243.727,-200.8099;Inherit;False;136;TextureBlend;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;129;125.4322,130.8818;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;125;185.5682,-547.3641;Inherit;False;Property;_ColourDiffuseToggle;Colour/Diffuse Toggle;10;0;Create;True;0;0;False;0;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;143;514.1636,-325.4974;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;1,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;146;407.0838,133.827;Inherit;True;Property;_TransparentTextureSwapToggle;Transparent/Texture Swap Toggle;6;0;Create;True;0;0;False;0;1;2;0;FLOAT;1;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;725.4511,-90.24815;Float;False;True;6;ASEMaterialInspector;0;0;Standard;BaneArt/DistanceAppear;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;1;True;True;0;False;TransparentCutout;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;13.6;10;25;False;0.582;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;5;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;65;0;57;0
WireConnection;124;20;58;0
WireConnection;124;21;65;0
WireConnection;136;0;124;0
WireConnection;138;0;132;0
WireConnection;139;0;138;0
WireConnection;134;0;132;0
WireConnection;140;0;141;0
WireConnection;140;1;142;0
WireConnection;129;0;139;0
WireConnection;129;1;134;0
WireConnection;129;2;137;0
WireConnection;125;0;9;0
WireConnection;125;1;127;0
WireConnection;143;0;125;0
WireConnection;143;1;140;0
WireConnection;143;2;144;0
WireConnection;146;1;129;0
WireConnection;0;0;143;0
WireConnection;0;10;146;0
ASEEND*/
//CHKSM=17B6EF45DB2B94027947B400DD1B26B005AEBAEE