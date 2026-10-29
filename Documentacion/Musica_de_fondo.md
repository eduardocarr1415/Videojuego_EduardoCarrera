# Canción de fondo del nivel

Pista: Juhani Junkala [Retro Game Music Pack] Level 3, del paquete 5 Chiptunes (Action). Es instrumental de acción retro. Se obtuvo de https://opengameart.org/content/5-chiptunes-action y el autor la publicó bajo CC0. Su nota original está guardada en Licencia_musica_arcade.txt. No es una grabación de Metal Slug.

Archivo del proyecto: Assets/Sonidos/Musica/MusicaArcade.wav.

## Cómo la puse

En juegofuncional añadí MusicaDelNivel con un Audio Source independiente del que reproduce saltos, hojas y memes. Su clip es MusicaArcade. Play On Awake lo inicia al entrar al nivel y Loop repite la canción. Spatial Blend está en 2D y Volume en 0.18 para dejar espacio a los efectos.

El archivo se importa con compresión Vorbis y Compressed In Memory para no mantener todo el WAV descomprimido durante la partida.

## Qué cambió en el código

SonidosJuego tiene una referencia pública AudioSource musicaFondo. En el Inspector apunta al reproductor de MusicaDelNivel. ReproducirResultado llama a musicaFondo.Stop antes del OOF o la frase final. Recoger una hoja y saltar no paran la canción.

No hace falta otro script para reproducirla ni dejar un objeto que sobreviva entre escenas. Al reiniciar, Unity vuelve a cargar MusicaDelNivel y la pista comienza otra vez. El menú principal no tiene esta música.

## Cómo cambiar la canción o el volumen

Abre juegofuncional fuera de Play y selecciona MusicaDelNivel. Arrastra otro AudioClip a la casilla del reproductor para cambiarlo. Baja o sube Volume entre 0 y 1. Mantén Loop activado si quieres que se repita. Guarda con Ctrl+S.

## Comprobación

Se probó en Play que la música empieza y avanza, que recoger una hoja no la corta, que la derrota la detiene sin tapar el OOF, que al reiniciar vuelve a sonar y que la victoria la detiene para escuchar la frase final. El informe está en Temp/ResultadoMusicaArcadeFinal.txt. El nivel quedó guardado y se retiró la herramienta temporal de configuración.
