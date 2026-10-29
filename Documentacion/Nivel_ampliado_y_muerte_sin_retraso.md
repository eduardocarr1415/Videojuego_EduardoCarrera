# Qué cambió en el nivel

La entrega pasó de X=55 a X=89: hay 34 unidades más de recorrido hasta el Patrón. Pasamos de ocho a doce plataformas y de seis a doce grupos de pinchos. Siguen siendo diez hojas; ahora están repartidas a lo largo del recorrido nuevo.

## Saltos y huecos

El salto pasó de 12 a 10.5. La rapidez sigue en 6 y la gravedad del cuerpo en 2. Las plataformas están separadas siete unidades entre sus centros y tienen 3.6 de ancho. Por eso conviene acercarse al borde antes de saltar a la siguiente.

| Plataforma | X | Altura de la superficie |
|---|---:|---:|
| 1 | 5 | -1 |
| 2 | 12 | 0.1 |
| 3 | 19 | -0.65 |
| 4 | 26 | -0.1 |
| 5 | 33 | -1.05 |
| 6 | 40 | 0.1 |
| 7 | 47 | -0.6 |
| 8 | 54 | 0 |
| 9 | 61 | -1 |
| 10 | 68 | -0.05 |
| 11 | 75 | -0.8 |
| 12 | 82 | -0.1 |

El primer hueco va de X=24 a X=29. El segundo va de X=58 a X=63. Las plataformas 4 y 9 ayudan a cruzarlos. El piso derecho termina en X=104 para que siga habiendo tierra debajo de la imagen al llegar al final.

Los doce grupos de pinchos están en X=9, 16, 22, 31, 36, 43, 50, 56, 65, 71, 78 y 85. Las hojas se colocan sobre las plataformas 1 a 8, 10 y 12. Puedes cambiar ambas listas en AdministradorNivel > ControlNivel.

## Árboles de los extremos

Arbol1 está en X=-3.5 y Arbol2 en X=94. Los dos apoyan sus raíces en Y=-2.5. Cada uno tiene un dibujo y un BoxCollider2D sólido, sin Is Trigger, para impedir salir por los lados. El collider es alto para que no se pueda saltar por encima.

Ajuste de las raíces: el dibujo ahora está en un hijo llamado ImagenArbol, bajado 0.22 unidades y ordenado detrás del suelo. Así la hierba cubre el borde de las raíces y no queda una franja de fondo debajo. El objeto principal y su collider conservan sus posiciones; el SpriteRenderer original queda desactivado.

El PNG está en Assets/Imagenes/Decoracion/ArbolLimiteSelva.png. Se creó con la herramienta de imágenes integrada y fondo transparente; el segundo árbol usa el mismo dibujo volteado. El texto usado para generarlo está en Prompt_arbol_limite.txt.

## Sonido de muerte

La espera estaba dentro del archivo: la voz comenzaba después de unos 0.523 segundos de silencio. Hice una copia llamada DerrotaOofInmediato.wav, cortando ese comienzo, y conservé el MP3 original. La voz de la copia empieza aproximadamente a los 0.008 segundos.

El WAV usa PCM y datos precargados. SonidosJuego también carga los clips en Awake. ControlNivel.Morir llama primero al sonido, en la misma llamada que procesa la muerte, y después muestra el panel y detiene las físicas. No hay un temporizador entre el golpe y el audio. La salida del dispositivo mantiene su latencia normal.

## Código y pruebas

ControlJugador dejó el salto en 10.5 y no sigue recibiendo órdenes tras terminar. ControlNivel expone las listas de hojas y pinchos y recuerda victoria y derrota para no repetirlas. SonidosJuego precarga las grabaciones. Los demás scripts recibieron comentarios para explicar sus bloques sin cambiar su comportamiento.

Se probaron doce saltos sucesivos en una escena de físicas separada, con las dimensiones reales del personaje, velocidad 6, impulso 10.5 y gravedad real. Todos llegaron a la plataforma siguiente saliendo cerca del borde. Los dos árboles detuvieron al cuerpo de prueba. Esto comprueba que los saltos son posibles; la dificultad jugando depende del momento en que pulses y de cómo te acerques al borde.

En Play se comprobó que aparecen diez hojas y doce peligros, que Morir inicia audio y panel en la misma llamada, que el reinicio funciona y que nueve hojas no ganan pero diez sí. El informe está en Temp/ResultadoNivelAmpliadoFinal.txt.

La explicación de cada script está en Guia_de_todos_los_scripts.md y en los comentarios de los propios archivos C#.
