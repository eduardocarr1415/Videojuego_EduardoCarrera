# Personaje relajado: dibujos y animaciones

Creé una hoja de sprites original con el generador de imágenes integrado de Codex. Es un PNG con transparencia real y doce dibujos del mismo personaje adulto de caricatura. Su expresión es humorística, con ojos entrecerrados y sonrisa relajada.

## Dónde están los recursos

En Unity abre Assets → Imagenes → Personajes.

- PersonajeRelajado.png: el dibujo completo con todas las poses.
- Al desplegar la flecha del PNG aparecen doce sprites individuales, separados mediante el importador de Unity. No son doce copias de la imagen.
- Animaciones/Quieto.anim: cuatro poses en bucle para estar quieto.
- Animaciones/Caminar.anim: cuatro poses en bucle para caminar.
- Animaciones/Saltar.anim: cuatro poses sin bucle, desde la preparación hasta el aterrizaje.

En la imagen, la primera fila es Quieto, la segunda Caminar y la tercera Saltar. Cada fila se lee de izquierda a derecha.

## Una aclaración útil

El PNG contiene los dibujos, pero por sí mismo no se mueve. Los archivos .anim indican en qué orden y a qué velocidad se muestran esos dibujos. El Animator conectado en Jugador → ImagenJugador usa Jugador.controller. El script AnimarJugador elige Quieto, Caminar o Saltar según el movimiento real del personaje.

El personaje ya está conectado en la escena. El rectángulo rojo original tiene su Sprite Renderer desactivado. La imagen y el Animator están en el hijo ImagenJugador; el Rigidbody2D, el BoxCollider2D y ControlJugador permanecen en Jugador con su configuración anterior.

## Cómo los preparé

1. Generé el PNG de 1448 × 1086 píxeles con fondo transparente.
2. Lo importé como Sprite (2D and UI), con Sprite Mode = Multiple.
3. Separé las doce poses en el Sprite Editor mediante sus rectángulos. Los cortes entre filas caen en franjas transparentes para no cortar manos, pelo ni humo.
4. Puse el punto de apoyo vertical cerca de los pies de cada dibujo, para facilitar la alineación al animar.
5. Preparé los tres clips con referencias a los sprites: Quieto a 4 dibujos por segundo; Caminar y Saltar a 8 dibujos por segundo.

Son animaciones iniciales de cuatro poses. La imagen conserva sus proporciones y alinea los pies con la caja de colisiones. Caminar y Quieto se repiten; durante el salto, el script selecciona la pose de despegue o de vuelo según la velocidad vertical. La pose de aterrizaje aparece brevemente al tocar el suelo. Al caminar hacia la izquierda, SpriteRenderer.flipX gira solo la imagen.

La conexión se comprobó con nueve casos: quieto, avance del bucle quieto, caminar, mirar a la izquierda, despegue, vuelo, caída, aterrizaje y regreso a quieto. Se conservó el código de movimiento existente.

## Prompt enviado al generador

```text
Use case: stylized-concept. Asset type: production 2D platformer character SPRITE SHEET on a truly transparent alpha background, landscape 1536x1152 PNG. EXACTLY 12 full-body sprites in an exact regular grid of FOUR equal columns and THREE equal rows. Each cell is 384x384 pixels; nothing crosses cell borders; generous transparent gutters; no drawn grid, no labels, no text, no numbers, no floor, no shadows outside the character. The SAME original fictional ADULT man age 30 in every cell: humorous laid-back scruffy tropical jungle adventurer, exaggerated big head and compact body, messy dark curly hair, little stubble, sleepy half-closed slightly red eyes, goofy relaxed grin. Off-white loose short-sleeve shirt with a small green leaf graphic (no text), olive-brown trousers and dark worn boots. A tiny hand-rolled cigarette at the corner of his mouth and one very small light gray wisp of smoke; humorous facial expression, no other props. Clearly adult proportions and features. All 12 frames face RIGHT in the same three-quarter side-view suitable for moving left/right in a platform game. Identical face, clothes, colors, drawing scale, and head size throughout; entire body including hair and feet inside its own cell. Clean dark cartoon outlines, warm soft cel shading, readable silhouette, polished humorous game sprite art to match a painted jungle. Character about 290 pixels tall within each 384px cell. Feet baseline at 350px down each cell, centered horizontally with room for arms. TOP ROW (four frames left to right): idle breathing loop, relaxed standing pose with hands loosely at sides, four subtly different breath phases with feet stationary. MIDDLE ROW: four-frame WALK cycle, frame1 right leg forward left leg back, frame2 legs passing under body with right leg bearing weight, frame3 left leg forward right leg back, frame4 legs passing with left leg bearing weight; alternate arms naturally, maintain baseline. BOTTOM ROW: four distinct JUMP poses left to right, anticipation crouch, takeoff extension with arms raised, airborne knees bent with arms out, landing crouch. Keep jump poses contained and centered in their cells, no simulated trajectory across the sheet. Important: exact 4x3 grid aligned to cell centers x=192,576,960,1344 and row centers y=192,576,960. Transparent background alpha, NOT a checkerboard painted into the image. No decorative elements, no frame borders, no extra fifth column or fourth row, no duplicated sheet.
```
