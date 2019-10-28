Shader "Unlit/PlanetShader"
{
	Properties
	{
		_MaskTex("Mask 0", 2D) = "white" {}
		_MaskTex1("Mask 1", 2D) = "white" {}
		_MaskTex2("Mask 2", 2D) = "white" {}
		_MaskTex3("Mask 3", 2D) = "white" {}
		_MaskTex4("Mask 4", 2D) = "white" {}
		_MaskTex5("Mask 5", 2D) = "white" {}

		_GrassTex("Tex 1", 2D) = "white" {}
		_MarsTex("Tex 2", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Speed("MovingSpeed", float) = 1

		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0


	}
		SubShader
	{
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature MASK0 MASK1 MASK2 MASK3 MASK4 MASK5

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float3 normal   : TEXCOORD1;    // The vertex normal in model space.
				float4 vertex : SV_POSITION;
			};

			sampler2D _MaskTex;
			sampler2D _MaskTex1;
			sampler2D _MaskTex2;
			sampler2D _MaskTex3;
			sampler2D _MaskTex4;
			sampler2D _MaskTex5;

			sampler2D _GrassTex;
			sampler2D _MarsTex;
			float4 _Color;

			float _Speed;
			float3 viewDir;

			float4 _MaskTex_ST;

			v2f vert(appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MaskTex);
				o.normal = mul((float3x3)UNITY_MATRIX_M, v.normal);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				i.normal = normalize(i.normal);

			half diffuseLight = 1;// saturate(dot(i.normal, float3(0, 1, 0)));

				// sample the textures
				fixed4 mask = tex2D(_MaskTex, i.uv) * diffuseLight;

				float4 col = _Color;

				#ifdef MASK0
					mask = tex2D(_MaskTex, i.uv) * diffuseLight;
				#endif
				#ifdef MASK1
					mask = tex2D(_MaskTex1, i.uv) * diffuseLight;
				#endif
				#ifdef MASK2
					mask = tex2D(_MaskTex2, i.uv) * diffuseLight;
				#endif
				#ifdef MASK3
					mask = tex2D(_MaskTex3, i.uv) * diffuseLight;
				#endif
				#ifdef MASK4
					mask = tex2D(_MaskTex4, i.uv) * diffuseLight;
				#endif
				#ifdef MASK5
					mask = tex2D(_MaskTex5, i.uv) * diffuseLight;
				#endif


				fixed4 grass = tex2D(_GrassTex, i.uv);
				fixed4 mars = tex2D(_MarsTex, i.uv);


				// mask the different texture by the corrosponding rgb channels in the mask
				grass = grass * mask.g;
				mars = mars * mask.r;

				// combine all masked textures as output
				return grass + mars * col;
			}
			ENDCG
		}

	
	}
	Fallback "VertexLit"
}
