# Cómo conecté tus cuatro sonidos

Usé los cuatro archivos que dejaste en `Assets/Sonidos/Memes`, sin descargar otros ni modificar sus grabaciones.

| Campo de SonidosJuego | Archivo |
|---|---|
| Salto | SaltoBoing.wav |
| Hoja | RecogerHoja.wav |
| Derrota | DerrotaOof.mp3 |
| Victoria | VictoriaPerrito.wav |

## Lo que cambié ahora

En AdministradorNivel, asigné esos archivos a los campos del componente SonidosJuego. Volví a activar la llamada `nivel.sonidos?.Saltar();` dentro de ControlJugador, justo después de aplicar la fuerza del salto. Así solo suena cuando el personaje salta estando apoyado, no cada vez que se pulsa Espacio en el aire.

## El código que ya habíamos preparado

SonidosJuego tiene cuatro campos AudioClip. Un AudioClip es el archivo de sonido. AudioSource es el componente que lo reproduce. Awake guarda ese componente y configura sonido 2D, sin reproducción automática y sin bucle.

Por ejemplo, RecogerHoja usa:

```csharp
fuente.PlayOneShot(hoja, volumenHoja);
```

Esto significa «reproduce el clip guardado en Hoja con el volumen indicado». ControlNivel llama a RecogerHoja al sumar una hoja, Perder al mostrar la derrota y Ganar al completar las diez hojas y llegar a la meta.

Al ganar o perder, partidaTerminada impide repetir el audio final. fuente.Stop detiene los efectos anteriores y PlayOneShot reproduce el resultado. Time.timeScale = 0 detiene las físicas pero no este AudioSource, por eso la frase de victoria puede terminar. Reiniciar recarga la escena y crea otra vez los componentes para una nueva partida.

El signo ?. significa «llama al método solo si la referencia existe».

## Cómo cambiar uno tú mismo

1. Sal del modo Play.
2. Selecciona AdministradorNivel en Hierarchy.
3. Busca SonidosJuego en Inspector.
4. Arrastra un audio desde Project al campo correspondiente.
5. Ajusta Volumen Salto, Volumen Hoja o Volumen Resultado entre 0 y 1.
6. Guarda con Ctrl+S y prueba en Play.

Estos cambios solo afectan al audio. Los campos nuevos no cambian las posiciones de las plataformas ni la fuerza del salto.
