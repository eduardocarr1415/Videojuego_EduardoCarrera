# Aspecto más agresivo de los pinchos

Se creó una variante del PNG con la herramienta de imágenes integrada, usando el dibujo anterior como referencia. La nueva versión tiene cinco puntas de hierro, púas laterales y madera astillada, manteniendo el estilo dibujado de la selva.

Archivo nuevo: Assets/Imagenes/Objetos/PinchosSelvaAgresivos.png. El original PinchosSelva.png se conserva.

La imagen se asignó al SpriteRenderer del hijo ImagenPinchos dentro de Assets/Prefabs/Pinchos.prefab. Como ControlNivel crea sus doce peligros desde ese prefab, todos reciben el nuevo aspecto. El dibujo conserva sus proporciones y la base se alineó con la parte baja del collider. No se cambiaron la hitbox, la etiqueta Pinchos, Is Trigger ni las posiciones del recorrido.

Para cambiarlo tú: abre el prefab Pinchos, selecciona ImagenPinchos y cambia Sprite en SpriteRenderer. El objeto padre contiene el collider que mata al jugador; la imagen está separada para poder cambiar su aspecto sin mover la zona de contacto.

## Prompt utilizado

Modo: herramienta integrada; edición con referencia local y fondo transparente.

Edit the supplied 2D jungle platformer spike-trap sprite. Make this same trap visibly MORE AGGRESSIVE and unmistakably dangerous at small game scale: exactly five upright stakes on one low horizontal dark wooden base, with much sharper narrow spear tips, splintered angular dark wood and worn dark iron spearhead caps with bright steel cutting edges. Small jagged backward barbs near the shoulders of the spearheads, subtle rusty red metal accents, rough tightly bound ropes. Menacing silhouette, central tip highest, outer tips slightly shorter, all FIVE tips fully visible. Keep the hand-painted cartoon videogame style, warm dark brown wood, clear thick outlines and simple readable shading to fit a teal tropical jungle platformer. Front orthographic side-scroller view. Keep the overall opaque shape approximately 2.2 times as wide as tall, base flat and horizontal at bottom, centered, a little transparent padding on every edge. True transparent background. NO blood, no gore, no people, no text, no scenery, no ground patch, no detached shadows. Preserve the original trap's basic five-stake composition; change only its aggressive visual design.
