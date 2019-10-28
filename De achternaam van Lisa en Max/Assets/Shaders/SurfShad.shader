Shader "Custom/SurfShad"
{
    Properties
    {
		_MaskTex("Mask 0", 2D) = "white" {}
		_MaskTex1("Mask 1", 2D) = "white" {}
		_MaskTex2("Mask 2", 2D) = "white" {}
		_MaskTex3("Mask 3", 2D) = "white" {}
		_MaskTex4("Mask 4", 2D) = "white" {}
		_MaskTex5("Mask 5", 2D) = "white" {}
		_MaskTex6("Mask 6", 2D) = "white" {}
		_MaskTex7("Mask 7", 2D) = "white" {}
		_MaskTex8("Mask 8", 2D) = "white" {}
		_MaskTex9("Mask 9", 2D) = "white" {}
		_MaskTex10("Mask 10", 2D) = "white" {}


		_GrassTex("Tex 1", 2D) = "white" {}
		_MarsTex("Tex 2", 2D) = "white" {}
		_AshTex("Tex 3", 2D) = "white" {}

        _Color ("Color", Color) = (1,1,1,1)
		_AshColor ("Ash Color", Color) = (0, 0, 0, 0)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {

        Tags { "RenderType"="Opaque" }
        LOD 200


        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
		#pragma shader_feature MASK0 MASK1 MASK2 MASK3 MASK4 MASK5 MASK6 MASK7 MASK8 MASK9 MASK10 MASK11

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

		sampler2D _MaskTex;
		sampler2D _MaskTex1;
		sampler2D _MaskTex2;
		sampler2D _MaskTex3;
		sampler2D _MaskTex4;
		sampler2D _MaskTex5;
		sampler2D _MaskTex6;
		sampler2D _MaskTex7;
		sampler2D _MaskTex8;
		sampler2D _MaskTex9;
		sampler2D _MaskTex10;

		sampler2D _GrassTex;
		sampler2D _MarsTex;
		sampler2D _AshTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed4 _AshColor;
		sampler2D _GrabTexture;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_GrassTex, IN.uv_MainTex) * _Color;
			half diffuseLight = 1;
			float4 col = _Color;
			float4 ashCol = _AshColor;

			fixed4 mask = tex2D(_MaskTex5, IN.uv_MainTex) * diffuseLight;

			#ifdef MASK0
						mask = tex2D(_MaskTex, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK1
						mask = tex2D(_MaskTex1, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK2
						mask = tex2D(_MaskTex2, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK3
						mask = tex2D(_MaskTex3, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK4
						mask = tex2D(_MaskTex4, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK5
						mask = tex2D(_MaskTex5, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK6
						mask = tex2D(_MaskTex6, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK7
						mask = tex2D(_MaskTex7, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK8
						mask = tex2D(_MaskTex8, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK9
						mask = tex2D(_MaskTex9, IN.uv_MainTex) * diffuseLight;
			#endif
			#ifdef MASK10
						mask = tex2D(_MaskTex10, IN.uv_MainTex) * diffuseLight;
			#endif


			fixed4 grass = tex2D(_GrassTex, IN.uv_MainTex);
			fixed4 mars = tex2D(_MarsTex, IN.uv_MainTex);
			fixed4 ash = tex2D(_AshTex, IN.uv_MainTex);

			grass = grass * mask.g;
			mars = mars * mask.r;
			ash = ash * mask.b;

			o.Albedo = mars * col + grass + ash * ashCol;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG

    }
    FallBack "Diffuse"
}
