Shader "Custom/Ghost"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_EmissionColor ("Rim Color", Color) = (1, 1, 1, 1)
		_EmissionStrength ("Rim Strength", float) = 0.5

		_turb ("Turbulence", float) = 1

		_xScale ("X Amount", Range(-1,1)) = 0.5
		_yScale ("Y Amount", Range(-1,1)) = 0.5
		_zScale ("Z Amount", Range(-1,1)) = 0.5

		_Scale("Effect Scale", float) = 1.0 
		_Speed("Effect Speed", float) = 1.0 
    }
    SubShader
    {
       	Tags { "RenderType" = "fade" "Queue" = "Transparent"}
		Blend SrcAlpha One
		BlendOp Add
		ZWrite On
		LOD 200	

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert
 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		sampler2D _MainTex;
        fixed4 _EmissionColor;
		float _EmissionStrength;
		float _turb;
		float _xScale;
		float _yScale;
		float _zScale;
		float _Scale;
		float _Speed;
		float _Amount;

        struct Input
        {
            float2 uv_MainTex;
			float3 viewDir;
        };

		void vert (inout appdata_full v)
		{
			float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz * _turb;
			float x = sin(worldPos.x + (_Time.y*_Speed))   * _Scale * 0.01;
			float y = sin(worldPos.y + (_Time.y*_Speed))  * _Scale * 0.01;
			float z = sin(worldPos.z + (_Time.y*_Speed))  * _Scale * 0.01;

			v.vertex.x += x * _xScale;
			v.vertex.y += y * _yScale;
			v.vertex.z += z * _zScale;
			
		}

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			//clip(tex2D (_MainTex, IN.uv_MainTex) - 0.5);

            o.Albedo = tex2D (_MainTex, IN.uv_MainTex);
			//o.Alpha = 0.5;

			half eyeEmission = (tex2D (_MainTex, IN.uv_MainTex));
			o.Emission = _EmissionStrength * _EmissionColor * eyeEmission;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
