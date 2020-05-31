/*  CSCI-B581 / Spring 2020 / Mitja Hmeljak

	The Vertex Shader component in this Unity Shader should:

	use t1, t2 values to compute the position of each
	   vertex on the Spline Curve:
	t1 is for the "base curve",
	t2 is for the "offset curve".

	to calculate (on the GPU) the vertices on a single Spline Curve segment,
	to be displayed as a Mesh, using a Mesh Renderer.

	Original demo code by CSCI-B481 alumnus Rajin Shankar, IU Computer Science.
 */




Shader "WaveShader" {

	Properties{ 
	[NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
	[NoScaleOffset] _NormalMap("Normals", 2D) = "bump" {}
	}

		SubShader{

			cull off

			Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }

			Pass  {

				CGPROGRAM

				// we'll provide both Vertex and Fragment shaders:
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata {
					float4 vertex : POSITION;
					float3 normal: NORMAL;
					fixed4 color : COLOR;
					float3 l: TEXCOORD1;
					float3 c:TEXCOORD2;
					float2 uv : TEXCOORD3;
				};

	// "varying" struct of variables,
	//     passed from vertex shader
	//     to fragment shader:
	struct v2f {
		float4 vertex : SV_POSITION;
		float3 n: TEXCOORD0;
		fixed4 color : COLOR;
		float3 l: TEXCOORD1;
		float3 c:TEXCOORD2;
		float2 uv : TEXCOORD3;
	};


	float3 _Light;
	float4 _Amb, _Diff, _Spec;
	float  _Shine, _Kd;
	float3 _Cam;

	// ---------------------------------------------------------
	// the Vertex Shaders outputs positions on the Spline Curve:
	v2f vert(appdata v) {

		// the output to this shader is:
		v2f o;
		float4 worldPosition = v.vertex;
		worldPosition.w = 1;
		o.n = normalize(v.normal);
		o.l = normalize(_Light - worldPosition.xyz);
		o.c = normalize(_Cam - worldPosition.xyz);
		o.vertex = mul(UNITY_MATRIX_VP, worldPosition);
		o.uv = v.uv;
		return o;
	} // end of vert shader


	// -------------------------------------------------
	// the Fragment Shader simply outputs a fixed color:
	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color;

	fixed4 frag(v2f i) : SV_Target{
		float lam = max(dot(i.l, i.n), 0);
		float spec = 0.0;
		float3 b;
		if (lam > 0.0) {
			b = normalize(i.l + i.c);
			spec = pow(max(0, dot(i.n, b)), _Shine);
		}
		fixed4 tex = tex2D(_MainTex, i.uv).rgba;
		_Color = _Amb + tex*_Diff*lam + _Spec * spec;
		return _Color;
	} // end of frag shader

	ENDCG
}
	}
}