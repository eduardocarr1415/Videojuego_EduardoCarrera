Shader "Marihuanero/AtmosferaSelva"
{
    // Son las casillas que AnimarSelva rellena para ajustar el aspecto de la capa.
    Properties
    {
        _Fase ("Fase del ciclo", Range(0,1)) = 0
        _Aspecto ("Proporcion de pantalla", Float) = 1.77778
        _Neblina ("Cantidad de neblina", Range(0,1)) = .20
        _Rayos ("Intensidad de rayos", Range(0,1)) = .20
        _Particulas ("Brillo de particulas", Range(0,1)) = .65
        _AlturaNeblina ("Altura de la niebla", Float) = .285
        _EspesorNeblina ("Espesor de la niebla", Float) = .14
        _ColorNeblina ("Color de la niebla", Color) = (.45,.65,.67,1)
        _ColorRayos ("Color de los rayos", Color) = (.61,.77,.81,1)
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "Queue"="Transparent" "RenderType"="Transparent" }
        Pass
        {
            Tags { "LightMode"="SRPDefaultUnlit" }
            // Mezclamos la capa con el fondo según su transparencia, sin taparlo todo.
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            HLSLPROGRAM
            #pragma vertex Vertice
            #pragma fragment Fragmento
            #pragma target 3.0
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float _Fase, _Aspecto, _Neblina, _Rayos, _Particulas;
                float _AlturaNeblina, _EspesorNeblina;
                float4 _ColorNeblina, _ColorRayos;
            CBUFFER_END
            struct Entrada { float4 posicion : POSITION; float2 uv : TEXCOORD0; };
            struct Salida { float4 posicion : SV_POSITION; float2 uv : TEXCOORD0; };
            // Colocamos el plano en pantalla y pasamos sus coordenadas al dibujo.
            Salida Vertice(Entrada v)
            {
                Salida o;
                o.posicion = TransformObjectToHClip(v.posicion.xyz);
                o.uv = v.uv;
                return o;
            }

            // Ruido suave calculado con números; no necesita otra imagen.
            float Azar(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);
                return frac(p.x * p.y);
            }
            float Ruido(float2 p)
            {
                float2 i = floor(p), f = frac(p);
                f = f * f * (3.0 - 2.0 * f);
                return lerp(lerp(Azar(i), Azar(i + float2(1,0)), f.x),
                            lerp(Azar(i + float2(0,1)), Azar(i + 1), f.x), f.y);
            }
            // Sumamos manchas grandes, medianas y pequeñas para formar la neblina.
            float Nube(float2 p)
            {
                return .57 * Ruido(p) + .29 * Ruido(p * 2.03 + 9.3)
                    + .14 * Ruido(p * 4.07 + 17.8);
            }
            half4 Fragmento(Salida i) : SV_Target
            {
                float2 uv = i.uv;
                float angulo = _Fase * 6.2831853;
                // Seno y coseno regresan al mismo punto: el bucle no da saltos.
                float2 desplazamiento = float2(sin(angulo), cos(angulo));
                float2 p = uv * float2(_Aspecto * 2.6, 8.0);
                float n = Nube(p + desplazamiento * float2(.8,.35));
                float espesor = max(.01, _EspesorNeblina);
                float ondulacion = .018 * sin(uv.x * 8 + angulo);
                float franja = exp(-pow((uv.y - _AlturaNeblina - ondulacion) / espesor, 2));
                float velo = exp(-pow((uv.y - _AlturaNeblina - espesor * .7) / (espesor * 1.25), 2));
                float niebla = (smoothstep(.18,.78,n) * franja + .14 * velo) * _Neblina;

                // Luz fría y tenue desde el claro de la copa de los árboles.
                float eje = uv.x - .42 * (uv.y - 1);
                float balanceo = .013 * sin(angulo);
                float haces = exp(-pow((eje - .80 - balanceo) / .035, 2))
                    + .55 * exp(-pow((eje - .91 + balanceo) / .052, 2))
                    + .3 * exp(-pow((eje - .69) / .025, 2));
                float luz = haces * smoothstep(.08,.85,uv.y)
                    * (.72 + .28 * sin(angulo + .8)) * _Rayos;

                // Dieciocho motas pequeñas: trayectorias lentas y brillo suave.
                float motas = 0;
                [unroll] for (int k = 0; k < 18; k++)
                {
                    float semilla = (float)k;
                    float2 origen = float2(Azar(float2(semilla,4)), Azar(float2(semilla,9)));
                    float fase = origen.x * 6.2831853;
                    float2 centro = .08 + origen * .84
                        + float2(.032 * sin(angulo + fase), .045 * cos(angulo + fase));
                    float2 distancia = (uv - centro) * float2(_Aspecto,1);
                    float radio = lerp(.0011,.0020,origen.y);
                    float d = dot(distancia,distancia) / (radio * radio);
                    float brillo = .25 + .75 * pow(.5 + .5 * sin(angulo + fase * 2), 2);
                    motas += (exp(-d * 1.8) + .12 * exp(-d * .11)) * brillo;
                }
                motas *= _Particulas;
                // Juntamos las tres capas: el alfa decide cuánto se ve de la selva detrás.
                float alfa = saturate(niebla + luz + motas);
                float3 color = (_ColorNeblina.rgb * niebla
                    + _ColorRayos.rgb * luz + float3(.67,.87,.70) * motas)
                    / max(.0001, niebla + luz + motas);
                return half4(color, alfa);
            }
            ENDHLSL
        }
    }
}
