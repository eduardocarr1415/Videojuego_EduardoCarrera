# Sonidos del juego

Actualización: derrota y victoria ahora usan los memes solicitados y el salto quedó silencioso. Consulta `Sonidos_memes.md` para los archivos actuales. Los cuatro WAV descritos abajo son las alternativas originales.

Los cuatro archivos están en `Assets/Sonidos/Efectos`:

| Archivo | Acción |
|---|---|
| SaltoBoing.wav | Salto que se ejecuta estando en el suelo |
| RecogerHoja.wav | Recoger una hoja, con timbre retro de moneda |
| Derrota.wav | Mostrar la pantalla de derrota |
| Victoria.wav | Entregar las diez hojas y ganar |

Son efectos de SFXMint, con licencia CC0. Las páginas originales y enlaces de descarga están en `Fuentes_sonidos.json`. Se eligieron efectos de caricatura y videojuego retro; no son grabaciones de frases de memes conocidos.

## Cómo funciona

El objeto AdministradorNivel tiene un Audio Source y el componente SonidosJuego. Los cuatro WAV se arrastran a los campos Salto, Hoja, Derrota y Victoria de ese componente. El campo Sonidos de ControlNivel apunta al mismo SonidosJuego.

ControlJugador llama al sonido dentro de la condición que ejecuta el salto. ControlNivel avisa al recoger una hoja, ganar o perder. PlayOneShot reproduce los clips sin crear otro objeto cada vez. Los resultados detienen los efectos anteriores y solo suenan una vez por partida. Audio Source sigue reproduciendo aunque Time.timeScale sea cero.

Para cambiar un efecto, arrastra otro AudioClip al campo correspondiente. Para ajustar los volúmenes, selecciona AdministradorNivel y cambia Volumen Salto, Volumen Hoja o Volumen Resultado en SonidosJuego. Audio Source debe tener Play On Awake desactivado y Spatial Blend en 2D.

La ambientación continua de selva se puede añadir en un siguiente paso con otra fuente en bucle.
