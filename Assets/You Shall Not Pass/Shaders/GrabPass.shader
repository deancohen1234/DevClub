Shader "Custom/GrabPassInvert"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_XMod("XMod", Float) = 2.0
		_YMod("YMod", Float) = 2.0
		_Intensity("Intensity", Float) = 2.0
	}
		SubShader
		{
			// Draw ourselves after all opaque geometry
			Tags{ "Queue" = "Transparent" }

			// Grab the screen behind the object into _BackgroundTexture
			GrabPass
		{
			"_BackgroundTexture"
		}

			// Render the object with the texture generated above, and invert the colors
			Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
		float _XMod;
		float _YMod;
		float _Intensity;

		struct v2f
	{
		float4 grabPos : TEXCOORD0;
		float4 pos : SV_POSITION;
	};

	v2f vert(appdata_base v)
	{
		v2f o;
		// use UnityObjectToClipPos from UnityCG.cginc to calculate 
		// the clip-space of the vertex
		o.pos = UnityObjectToClipPos(v.vertex);
		// use ComputeGrabScreenPos function from UnityCG.cginc
		// to get the correct texture coordinate
		o.grabPos = ComputeGrabScreenPos(o.pos);

		//dean adjusts
		o.grabPos *= 2;

		float x = o.grabPos.x;
		float y = o.grabPos.y;

		x += _XMod;
		y += _YMod;

		o.grabPos.x = x;
		o.grabPos.y = y;

		o.grabPos.x += _Time.y;

		return o;
	}

		sampler2D _BackgroundTexture;

		half4 frag(v2f i) : SV_Target
		{
			float4 sam = tex2D(_MainTex, i.grabPos);

			float combination = sam.b * _Intensity;

			half4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos + combination);
			return bgcolor;
		}
		ENDCG
	}

	}
}
