# Cómo animé la selva

La selva sigue siendo la misma imagen PNG. Añadí una capa transparente encima del paisaje para darle vida: neblina tenue, haces de luz y dieciocho pequeñas motas doradas. El dibujo no se desplaza.

## Dónde está cada cosa

En Hierarchy, dentro de Main Camera, ahora tienes:

- FondoSelva: la imagen original, con orden de dibujo -100.
- AtmosferaSelva: los efectos, con orden de dibujo -90.

El personaje, las plataformas y los objetos del nivel se dibujan delante de estas dos capas. Los efectos son decorativos y no tienen colisiones.

## Cómo funciona

1. `Assets/Materiales/AtmosferaSelva.shader` dibuja formas transparentes mediante cálculos. Un shader es un programa pequeño que le dice a la tarjeta gráfica qué color dibujar en cada punto. Aquí crea la neblina, los haces y las motas; no usa un video ni un GIF.
2. `Assets/Materiales/AtmosferaSelva.mat` usa ese shader en el plano `PlanoAtmosferaSelva.asset`.
3. `Assets/Scripts/AnimarSelva.cs` pasa el tiempo de la animación al material y adapta el plano al tamaño de la cámara.
4. La fase avanza del 0 al 1 durante diez segundos. Los movimientos usan seno y coseno, de forma que terminan donde empezaron y vuelven a comenzar sin un salto visible.
5. Usé un MaterialPropertyBlock para actualizar los valores sin crear materiales nuevos en cada fotograma.
6. La animación usa el tiempo del juego: se congela cuando aparece la victoria o la derrota y comienza de nuevo al reiniciar la partida.

## Cómo cambiarlo tú

Sal de Play para guardar tus cambios. Selecciona Main Camera → AtmosferaSelva y busca Animar Selva en el Inspector.

| Campo | Qué controla | Valor inicial |
| --- | --- | --- |
| Duracion Ciclo | Segundos antes de repetir los movimientos. Un número mayor los hace más lentos. | 10 |
| Cantidad Neblina | Visibilidad de la neblina. | 0.42 |
| Intensidad Rayos | Fuerza de los haces de luz. | 0.075 |
| Brillo Particulas | Brillo de las motas flotantes. | 0.22 |
| Suelo Referencia | Caja del suelo que determina la altura del efecto. | SueloIzquierdo |
| Altura Sobre Suelo | Altura del centro de la niebla sobre la superficie, en unidades del mundo. | 0.35 |
| Espesor Neblina | Grosor suave de la franja, en unidades del mundo. | 1.4 |

El ambiente actual es una selva oscura vista desde el piso. La luz y la niebla usan tonos fríos. El centro de la niebla se calcula desde la superficie del suelo para que siga alineado si cambia la cámara. Los dos suelos se extendieron hacia abajo hasta Y = -7, conservando su superficie en Y = -2.5 y sus anchos originales. Sus centros quedaron en Y = -4.75, con escala Y = 4.5; no se aplicó la textura de tierra.

Pon cualquiera de las tres intensidades en 0 para apagar ese efecto. Desmarca el objeto AtmosferaSelva para apagar toda la animación y conservar el fondo fijo. Dale a Play para ver el movimiento; fuera de Play se muestra una vista estática.

## Qué se cambió

Se añadió el objeto AtmosferaSelva, el script AnimarSelva y sus tres recursos de dibujo (shader, material y plano). Se guardó juegofuncional. No se modificaron la imagen de selva, el jugador, las plataformas, las colisiones, los controles ni las pantallas finales.
