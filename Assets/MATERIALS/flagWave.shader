// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/NewShader" 
{
//okay, so, this thing ripples, sort of, kind of. it's a weird sort of effect and it took a lot of trying
//and a bunch of stealing code off of the internet. not sure how this thing is going to work in a 3D scene, 
//works alright, if a bit timidly and slowli, in a 2D scene. So there.



	Properties 

	{ 
	_Color ("Main Color", Color) = (1,1,1,1) 
	_MainTex ("Texture", 2D) = "white" { }
	} 


	SubShader 

	{ 
		Pass 
		{ 
			CULL Off 
			CGPROGRAM 

			#pragma vertex vert 
			#pragma fragment frag 
			#include "UnityCG.cginc" 

			float4 _Color; 
			sampler2D _MainTex;

			struct appdata { 
				float4 vertex : SV_POSITION; 
				float4 texcoord : TEXCOORD0; 

			}; 

			struct v2f { 
				float4 pos : SV_POSITION; 
				float2 uv: TEXCOORD0; 
			}; 



			v2f vert (appdata v) { 
				v2f o; 

				float sinAddition=v.vertex.x+v.vertex.y+v.vertex.z; 
				
				float flagX=v.texcoord.x; 
				float flagY=v.texcoord.x*v.texcoord.y; 
				
				v.vertex.x+=sin((_Time/2)*2.2+sinAddition)*flagX*0.2; 
				v.vertex.y=sin(_Time*3.12+sinAddition)*flagX*0.4-flagY*0.9; 
				v.vertex.z-=cos((_Time/2)*4.2+sinAddition*1.5)*flagX*0.2; 
				
				o.pos = UnityObjectToClipPos( v.vertex ); 
				o.uv = v.texcoord; 

				return o; 
			} 


			float4 frag (v2f i) : COLOR 

			{ 
				half4 color = tex2D(_MainTex, i.uv); 
				return color; 
			} 



			ENDCG 

			SetTexture [_MainTex] {combine texture} 

		} 

	} 

	Fallback "VertexLit" 

}

