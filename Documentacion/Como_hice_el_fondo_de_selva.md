# Cómo hice el fondo de selva

## 1. Crear la imagen

Usé el generador de imágenes integrado de Codex para crear una ilustración original de selva. Pedí un paisaje horizontal con árboles, vegetación y neblina, con el centro más despejado para ver al jugador y las plataformas. La imagen no contiene personajes ni plataformas dibujadas.

La imagen está en `Assets/Imagenes/Fondos/FondoSelva.png`.

Este fue el prompt completo enviado al generador:

```text
Use case: stylized-concept. Asset type: finished full-screen background PNG for a 2D side-scrolling Unity jungle platform game. Create an original beautiful lush tropical South American jungle, panoramic 16:9 landscape, ideally 2048x1152. Hand-painted cartoon game art with clean silhouettes, harmonious layered emerald and teal greens, misty distant jungle and soft warm sunbeams. Side-on orthographic environment, distant trees with vines, huge tropical leaves and fern foliage mostly framing the outer edges and lower corners. Keep the central and lower-middle playable area relatively open, lower contrast and light mist so separate player and platform sprites remain easy to read in front. Layers of distant tree trunks and canopy create depth. The image must fill the entire rectangular canvas edge to edge, opaque. Background scenery only: no characters, no animals, no text, no letters, no logos, no watermark, no UI, no collectible objects, no floating platforms, no foreground solid floor or walkable ground. Polished videogame illustration, consistent natural lighting, no photorealism.
```

## 2. Importarla a Unity

En el Inspector de la imagen configuré el equivalente a:

- Texture Type: Sprite (2D and UI).
- Sprite Mode: Single.
- Pixels Per Unit: 100.
- Filter Mode: Bilinear.
- Wrap Mode: Clamp, para no repetir la imagen en sus bordes.
- Generate Mip Maps: desactivado.
- Compresión: desactivada para conservar la ilustración.

## 3. Colocarla detrás del nivel

Añadí el objeto `FondoSelva` como hijo de `Main Camera` en la jerarquía. Su Sprite Renderer usa la imagen y tiene `Order in Layer = -100`, de modo que se dibuja detrás del personaje, el suelo, las hojas y las plataformas. Su material es `Sprite-Unlit-Default`, que conserva los colores de la imagen sin depender de la luz 2D.

El fondo no tiene Collider ni Rigidbody. Es un decorado y no interviene en las colisiones.

Como es hijo de la cámara, la acompaña por todo el nivel. Es una sola ilustración distante; no se generan nuevas plataformas ni se añaden capas de paralaje.

## 4. Cubrir la pantalla sin deformar la imagen

Añadí `Assets/Scripts/AjustarFondo.cs`, con comentarios y nombres en español. El script calcula el ancho y el alto que ve la cámara y aplica la misma escala horizontal y vertical al fondo. Si cambia la proporción de la ventana, recorta parte de los bordes para seguir llenando la pantalla sin estirar el dibujo.

La referencia `Camara` de ese script apunta a `Main Camera`.

## Qué cambió

- Se añadió el PNG y su carpeta Fondos.
- Se añadió el script AjustarFondo.
- Se añadió Main Camera → FondoSelva en juegofuncional y se guardó la escena.
- Se añadieron los archivos .meta que Unity necesita para las referencias.

El jugador, su movimiento, los suelos, las plataformas, las hojas, los pinchos y las pantallas finales conservan su configuración. La cámara mantiene su script de seguimiento.

## Cómo verlo o cambiarlo tú

1. En Hierarchy, despliega Main Camera y selecciona FondoSelva.
2. En Sprite Renderer → Sprite verás la imagen utilizada.
3. Para probar otra ilustración, importa su PNG como Sprite y arrástralo a ese campo. AjustarFondo adapta el tamaño automáticamente.
4. Para ocultar el fondo temporalmente, desmarca la casilla del objeto FondoSelva. Volverás a ver el color anterior de la cámara.

Antes de añadirlo guardé una copia de la escena en `Temp/EscenaAntesDeFondoSelva.unity`. Temp es una carpeta de trabajo temporal de Unity; no es una copia de seguridad permanente del proyecto.
