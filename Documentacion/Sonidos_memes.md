# Cambios de sonidos solicitados

Actualización posterior: el usuario eligió cuatro archivos en Memes. Se conectaron todos, incluido SaltoBoing.wav, y se reactivó el sonido al saltar. Consulta `Como_conecte_tus_cuatro_sonidos.md` para el estado actual.

- Derrota: `Assets/Sonidos/Memes/DerrotaOof.mp3`, descargado desde https://www.myinstants.com/en/instant/roblox-oof/ (archivo https://www.myinstants.com/media/sounds/roblox-death-sound_1.mp3).
- Victoria: `Assets/Sonidos/Memes/VictoriaPerrito.wav`, audio del video de seis segundos indicado por el usuario: https://www.youtube.com/watch?v=T7Tv_VtNq3E. Se convirtió a WAV mono de 44100 Hz para Unity.
- Salto: se quitó la llamada de audio de ControlJugador y se dejó vacío el campo Salto de SonidosJuego. El WAV anterior se conserva como alternativa.
- Hojas: conserva RecogerHoja.wav hasta disponer del recorte musical solicitado. No se descargó ninguna canción de Snoop Dogg.

Estos dos memes no forman parte del paquete CC0 de SFXMint. Sus páginas de origen no acreditan aquí una licencia de reutilización en videojuegos. El archivo Fuentes_sonidos.json describe los cuatro efectos originales, que se conservan como alternativas.

En Unity, AdministradorNivel > SonidosJuego permite cambiar los clips y sus volúmenes. El juego continúa usando un Audio Source 2D y PlayOneShot; los resultados se oyen aunque Time.timeScale sea cero.
