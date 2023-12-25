Shader "Custom/GridShader"
{
    Properties
    {
        _LineColor("Line Color", Color) = (0,1,0,1)
        _FillColor("Fill Color", Color) = (0,1,0,0.5)
        _LineThickness("Line Thickness", float) = 0.01
        _GridSize("Grid Size", Float) = 9
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" "LightMode"="UniversalForward" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _LineColor;
            float4 _FillColor;
            float _LineThickness;
            float _GridSize;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Normalized grid coordinates
                float2 gridUV = IN.uv * _GridSize;

                // Fractional part to determine the position within the grid cell
                float2 gridFraction = frac(gridUV);

                // Calculate the distance to the nearest edge within the grid cell
                float distToEdge = min(gridFraction.x, min(gridFraction.y, min(1.0 - gridFraction.x, 1.0 - gridFraction.y)));

                // Set fixed value for line thickness
                float edgeWidth = _LineThickness;

                // Calculate alpha based on the distance to the edge for the lines
                float lineAlpha = 1.0 - smoothstep(edgeWidth - 0.01, edgeWidth, distToEdge);

                // Determine the color of the grid line
                float4 lineColor = _LineColor;
                float4 fillColor = _FillColor;

                // Ripple effect - Use a sine wave over time and distance
                float ripple = sin(_Time.y ); // The constants here control the speed and frequency of the ripples

                // Apply the ripple effect to the cell transparency
                fillColor.a = clamp(ripple * 0.15, 0.1, 0.3);

                // Mix the fill color and the line color based on the alpha value
                float4 color = lerp(fillColor, lineColor, lineAlpha);

                // Set the final alpha value
                color.a = saturate(fillColor.a + lineAlpha * lineColor.a);

                // Final color output
                return color;
            }
            ENDHLSL
        }
    }
}
