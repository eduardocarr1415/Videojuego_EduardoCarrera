# La meta del Patrón

Creé el PNG transparente `Assets/Imagenes/Patron/PatronMeta.png` con el generador de imágenes integrado de Codex, usando PatronConDinero.png como referencia para conservar la cara y la ropa. El nuevo dibujo muestra al Patrón de cuerpo entero junto a un letrero que dice ENTREGA AQUÍ.

En la escena añadí `Meta → ImagenPatronMeta`, con su Sprite Renderer. Conserva las proporciones del PNG, mide 1.7 unidades de alto y apoya los pies en Y = -2.5, sobre el césped. El material sin iluminación mantiene visibles los colores en la selva oscura.

Desactivé el Sprite Renderer del rectángulo amarillo original. El objeto Meta conserva su posición X = 55, su caja de contacto, su Tag Meta e Is Trigger.

El código y la condición de victoria permanecen iguales. Comprobé en una escena temporal que con nueve hojas aparece el aviso de hojas faltantes y la partida continúa, y que con diez hojas se muestra la victoria y se pausa la partida. También revisé la imagen al final del nivel en Play. El desplazamiento usado para esa revisión fue temporal y no se guardó en la escena.

Para seleccionar el dibujo, despliega Meta en Hierarchy y selecciona ImagenPatronMeta. El Box Collider 2D que detecta la entrega sigue estando en Meta.

Actualización: el dibujo actual es `Assets/Imagenes/Patron/PatronMetaEnojado.png`, una caricatura de Pablo Escobar enojado con una pistola apuntando hacia abajo. Conserva el letrero ENTREGA AQUÍ. El PNG anterior sigue guardado como versión previa.

Se añadió SueloDerecho2 junto a SueloDerecho. Usa la misma textura de tierra, material y capa de colisión. Abarca X = 57 a X = 84, con superficie Y = -2.5 y sin hueco ni escalón en la unión. Así el piso continúa detrás de la zona de entrega. Se verificaron apoyos antes y después de la unión y en el extremo nuevo. La meta y la condición de diez hojas se conservaron.

## Prompt utilizado

```text
Use case: identity-preserve / stylized-concept. Create ONE new 2D videogame finish-goal sprite on a genuinely transparent alpha background, wide 4:3 landscape composition. Reference image: preserve the SAME caricature face and identity of the smiling mustached Patrón shown (black wavy hair, thick black mustache, beige short-sleeved button-up shirt, stocky adult body), but show his ENTIRE body standing with both feet visible: dark brown trousers, belt and dark shoes. He is waiting at the END of a humorous side-scrolling jungle platform game, looking three-quarter LEFT toward the approaching player, relaxed friendly expression. On the LEFT of the character, a simple sturdy wooden sign on a short post, both post and shoes on the same horizontal baseline. The sign has a large clean dark-brown face, with big very readable pale cream lettering in exactly TWO lines: first line "ENTREGA", second line "AQUÍ". Preserve the accent on Í. He gestures casually toward the sign with one hand and holds a small fan of banknotes in the other. One cohesive asset: full-body Patrón on the right, clearly readable wooden sign on the left. Clean hand-drawn cartoon game art, bold tidy dark contours, simple two-to-three-tone cel shading, restrained detail and coherent anatomy, match a dark tropical jungle game and its earthy grass-and-soil platforms. Generous head for small-size facial readability. Pale shirt and lettering contrast clearly against the dark forest. Keep the complete outer silhouette, sign edges, post base and shoes fully within the canvas with a small even transparent margin; no floor or landscape drawn underneath. No background scenery, no additional characters, no weapons, no extra floating items, no glow, no cast shadow on a ground plane, no border, no watermark, no other text. This is one static sprite, not a spritesheet or a poster.
```
