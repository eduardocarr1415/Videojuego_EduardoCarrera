# Cómo funciona el código de Marihuanero

Esta guía sigue los archivos que están en Assets/Scripts. Dentro de cada archivo también dejé comentarios encima de los bloques que conviene entender. Para leer el proyecto, empieza por ControlJugador, después ControlNivel y luego SonidosJuego.

## Antes de abrir un archivo

- Una variable guarda un dato: rapidez guarda un número; cuerpo guarda un componente.
- Un método agrupa instrucciones. Morir() contiene lo que pasa cuando pierdes.
- public permite que otros scripts usen el dato y que Unity muestre campos compatibles en el Inspector.
- private deja el dato para uso del propio script.
- if comprueba una condición. return termina esa llamada al método.
- GetComponent busca un componente del mismo objeto.
- Instantiate crea una copia de un prefab; Destroy quita un objeto.
- ?. hace una llamada solo si la referencia existe. No sustituye conectar bien las casillas.
- Awake prepara referencias al cargar el objeto. Start prepara la partida antes de la primera actualización.
- Update se ejecuta por fotograma. FixedUpdate se ejecuta por pasos de física. LateUpdate va después de Update.
- Una posición usa X para izquierda/derecha, Y para altura y Z para profundidad.

## ControlJugador.cs: moverse, saltar y tocar objetos

**Dónde va:** en Jugador, junto al Rigidbody2D y BoxCollider2D.

**Las variables:** rapidez vale 6 y fuerzaSalto vale 10.5. capaSuelo filtra qué objetos cuentan como apoyo. cuerpo es el Rigidbody2D; caja es el collider del jugador. direccion vale -1, 0 o 1. saltoPendiente recuerda que pulsaste Espacio. nivel apunta al AdministradorNivel.

**Awake:** guarda el cuerpo y la caja para usarlos después, sin buscarlos en cada fotograma.

**Update, primer bloque:** si la partida terminó, deja de leer controles. Si Y es menor que -7, llama a Morir: el personaje cayó al hueco. No reinicia automáticamente; aparece el botón.

**Update, teclado:** empieza con direccion en cero. A o flecha izquierda resta uno; D o flecha derecha suma uno. Ambas pulsadas dan cero. Si no hay teclado conectado, termina sin intentar leerlo.

**Update, Espacio:** wasPressedThisFrame detecta la pulsación nueva. Guardamos saltoPendiente porque la siguiente actualización de física puede ocurrir después.

**FixedUpdate, pies:** calcula una caja pequeña justo debajo del collider. OverlapBox pregunta si toca algo de capaSuelo. Esa respuesta se guarda en enSuelo.

**FixedUpdate, velocidad:** la velocidad horizontal es direccion multiplicada por rapidez. Se conserva la velocidad vertical para no borrar la caída que calcula Unity.

**FixedUpdate, salto:** solo si hay una pulsación pendiente y apoyo, pone la velocidad vertical en fuerzaSalto y llama al sonido. Mantener Espacio o pulsarlo en el aire no permite volar. Al final borra saltoPendiente para no gastar la misma pulsación dos veces.

**OnTriggerEnter2D:** Unity llama al entrar en una zona Is Trigger. Si la etiqueta es Hoja, apaga su collider, suma una hoja y destruye esa copia. Si es Meta, pide comprobar la victoria. Si es Pinchos, pide morir. Las etiquetas se escriben igual que en Unity, respetando mayúsculas.

## ControlNivel.cs: preparar y terminar la partida

**Dónde va:** en AdministradorNivel. Aquí conectamos los textos del Canvas, los dos prefabs, los paneles y SonidosJuego.

**Contador y final:** hojasRecogidas empieza en cero; totalHojas es diez. derrotaMostrada y victoriaMostrada recuerdan si ya terminó la partida. PartidaTerminada devuelve verdadero si cualquiera de las dos lo hizo.

**posicionesHojas:** son las diez coordenadas de aparición. Ahora se ven en el Inspector para poder cambiarlas sin reescribir el archivo. Cada hoja está por encima de una plataforma del recorrido ampliado.

**posicionesPinchos:** son doce coordenadas X. Todos usan la altura preparada para apoyar la base del prefab en el piso. No hay pinchos plantados en los huecos.

**Start:** devuelve Time.timeScale a 1, vacía el aviso, reinicia el contador y oculta los paneles. Después crea hojas y pinchos y escribe el contador inicial.

**CrearHojas:** recorre la lista y copia prefabHoja en cada coordenada. Quaternion.identity deja la rotación sin giro. No hace falta dejar diez hojas colocadas a mano en Hierarchy.

**RecogerHoja:** si la partida sigue activa, suma uno, reproduce el efecto y actualiza el texto.

**ActualizarContador:** une palabras y números para escribir, por ejemplo, Hojas: 3 / 10. Usa el texto existente del Canvas.

**IntentarGanar:** si faltan hojas, muestra el aviso y deja jugar. Con diez, marca victoria, reproduce la frase, muestra el panel y pone Time.timeScale en cero. Así las físicas paran. No vuelve a ganar cada vez que llega otro aviso del collider.

**ReiniciarNivel:** el botón llama a este método. Primero devuelve el tiempo a 1 y luego recarga la escena activa. Start vuelve a crear los objetos y el contador.

**CrearPinchos:** copia el prefab en cada X de la lista, con Y=-2.2. El dibujo y el collider del prefab ya están ajustados para que la base quede en Y=-2.5.

**Morir:** descarta llamadas repetidas, marca derrota y arranca el sonido enseguida. Después muestra el panel y detiene el tiempo. No tiene espera, corrutina ni animación que retrase el OOF.

## SonidosJuego.cs: elegir qué grabación suena

**Dónde va:** en AdministradorNivel, junto al Audio Source. AudioClip es el archivo; AudioSource es el reproductor.

**Los cuatro campos:** salto, hoja, derrota y victoria reciben sus archivos desde Project. Los volúmenes van de 0 a 1. fuente guarda el reproductor y partidaTerminada evita repetir un resultado.

**Awake:** busca Audio Source, desactiva reproducción automática y bucle, y elige sonido 2D. LoadAudioData carga las grabaciones en memoria antes de necesitarlas.

**Saltar y RecogerHoja:** comprueban que la partida siga activa y que haya clip asignado. PlayOneShot reproduce esa grabación con su volumen. No se crea un altavoz nuevo por cada hoja.

**Perder y Ganar:** pasan el clip de derrota o victoria al mismo método, ReproducirResultado.

**ReproducirResultado:** marca el final, detiene los efectos anteriores y reproduce el resultado una vez. El AudioSource continúa aunque Time.timeScale sea cero: se detienen las físicas, no el audio.

**musicaFondo:** apunta a otro AudioSource del objeto MusicaDelNivel. Este reproduce la canción en bucle por su cuenta. ReproducirResultado lo detiene al ganar o perder para que se escuche el meme final; los efectos de hojas y salto no lo interrumpen. El montaje se explica en Musica_de_fondo.md.

**Por qué el OOF llegaba tarde:** el MP3 original tenía unos 0.523 segundos de silencio antes de la voz. Preparé DerrotaOofInmediato.wav quitando ese inicio, conservé el original y conecté el nuevo. Está importado como PCM con los datos precargados. Esto quita el retraso del archivo; la salida del altavoz todavía tiene la latencia normal del dispositivo.

## CamaraSigue.cs: acompañar al jugador

**Dónde va:** en Main Camera. jugador apunta al Transform de Jugador. suavizado es el tiempo aproximado de acercamiento. velocidad es una memoria que necesita SmoothDamp; no es la velocidad del personaje.

**OnEnable:** borra la referencia de seguimiento anterior para preparar un encuadre nuevo al activarse.

**LateUpdate:** si no hay jugador asignado, sale. En el primer fotograma coloca la cámara dos unidades por delante. Después calcula cuánto avanzó el jugador desde el fotograma anterior y acompaña esa misma distancia. El Rigidbody2D usa Interpolate para mostrar ese avance suavemente. SmoothDamp solo corrige un posible desajuste respecto al destino; así la cámara no persigue el avance con retraso ni da sensación de empujar al personaje hacia atrás. Y y Z se conservan. El ajuste se explica en Movimiento_sin_tiron_al_saltar.md.

## AnimarJugador.cs: mostrar la pose que toca

**Dónde va:** en Jugador. animador apunta al Animator del dibujo e imagen a su SpriteRenderer. El archivo lee el movimiento; ControlJugador sigue siendo quien lo aplica.

**Nombres guardados:** Quieto, Caminar y Saltar son los nombres de estados del controller. StringToHash los convierte en números para no buscar la misma cadena todo el tiempo.

**Awake:** guarda Rigidbody2D, BoxCollider2D y ControlJugador.

**Update:** si el tiempo está parado, no cambia la pose. Comprueba el apoyo debajo de los pies y pasa velocidad, apoyo y tiempo transcurrido a MostrarMovimiento.

**MostrarMovimiento, dirección:** flipX cambia el lado al que mira el dibujo. Quieto conserva la última dirección. primerFotograma evita interpretar el arranque como un aterrizaje.

**MostrarMovimiento, aire:** elige la pose de despegue, la acerca a la pose alta según pierde velocidad hacia arriba y conserva la de caída. Usa fuerzaSalto del propio jugador, así sigue funcionando al bajar el salto.

**MostrarMovimiento, suelo:** al aterrizar mantiene esa pose durante 0.1 segundos. Después elige Caminar si se mueve o Quieto si no. Solo reinicia el estado al cambiar; no empieza la animación desde cero en cada fotograma.

**MostrarPoseSalto:** pone la velocidad del Animator en cero y muestra un punto concreto del clip. Eso deja que la pose siga la caída real en lugar de terminar la animación mientras todavía estás arriba.

## AjustarFondo.cs: cubrir la pantalla sin deformar la selva

**Dónde va:** en FondoSelva, hijo de la cámara. ExecuteAlways permite ver el ajuste también mientras editas.

**OnEnable:** busca el SpriteRenderer y, si no conectaste la cámara, la busca en el padre.

**LateUpdate:** vuelve a llamar al ajuste porque puedes cambiar la proporción de Game.

**AjustarTamano:** comprueba que haya cámara ortográfica e imagen. El alto visible es Orthographic Size por dos; el ancho usa la proporción de pantalla. Elige la escala mayor necesaria para cubrir ambos. Mantiene la misma proporción del dibujo, por eso puede recortar bordes sin estirarlo. Descuenta la escala del padre y solo escribe el tamaño si cambió.

## AnimarSelva.cs: niebla, rayos y motas

**Dónde va:** en AtmosferaSelva, hijo de Main Camera. Es una capa delante del fondo y detrás de las plataformas.

**Campos del Inspector:** permiten elegir cámara, suelo de referencia, altura y grosor de niebla, colores, duración del ciclo e intensidad de cada efecto.

**Identificadores:** Shader.PropertyToID guarda los nombres de las propiedades del material como números. No son objetos del nivel.

**OnEnable:** guarda MeshRenderer y prepara MaterialPropertyBlock. Ese bloque permite cambiar el aspecto sin fabricar un material nuevo cada fotograma.

**LateUpdate:** suma el tiempo y lo devuelve al inicio al terminar el ciclo. Como usa Time.deltaTime, la atmósfera se congela al ganar o perder. En edición muestra una vista quieta.

**OnDisable:** quita los valores que había enviado al material.

**ActualizarCapa:** ajusta el plano a la pantalla y envía la fase del ciclo, proporción, colores e intensidades al shader. Convierte la altura del piso a una altura de pantalla para que la niebla siga cerca del suelo aunque la cámara avance.

## AtmosferaSelva.shader: dibujar la atmósfera

Está en Assets/Materiales. No usa C#: es código que la tarjeta gráfica ejecuta sobre la capa transparente.

- Properties declara las casillas del material que recibe AnimarSelva.
- Vertice coloca el plano y conserva sus coordenadas de imagen.
- Azar da valores variados a partir de posiciones; Ruido los suaviza.
- Nube mezcla ruido grande y pequeño para evitar una mancha uniforme.
- Fragmento calcula niebla baja, haces de luz y dieciocho motas. Seno y coseno hacen que el ciclo vuelva al inicio sin un corte.
- El último bloque mezcla colores y transparencia para dejar ver la selva de atrás.

## ControlMenuPrincipal.cs: botones de la portada

Va en AdministradorMenu, dentro de MenuPrincipal. Awake restaura el tiempo normal y muestra el cursor. Jugar carga juegofuncional una sola vez por pulsación. Salir detiene Play en Unity y usa Application.Quit en el ejecutable. El botón Niveles queda sin funciones conectadas. El montaje del fondo, los botones y cada parte del archivo se explican en Menu_principal.md.

## Welcome2DScript.cs: archivo de la plantilla

Está en Assets/Welcome y no interviene en el juego. CreateAssetMenu permite crear el recurso de bienvenida. Open2DLandingPage abre la página de información 2D de Unity. Lo conservamos porque venía con la plantilla.

## Los árboles y las plataformas

Arbol1 y Arbol2 no necesitan otro script: cada uno tiene SpriteRenderer y un BoxCollider2D sólido. El dibujo muestra el límite y el collider impide atravesarlo. Las plataformas y los suelos también usan colliders estáticos. Sus posiciones se guardan en la escena; las hojas y los pinchos se crean al jugar desde ControlNivel.

## Qué puedes tocar sin reescribir el juego

- Jugador > ControlJugador: Rapidez y Fuerza Salto.
- AdministradorNivel > ControlNivel: Posiciones Hojas y Posiciones Pinchos.
- AdministradorNivel > SonidosJuego: cuatro grabaciones y sus volúmenes.
- Main Camera > CamaraSigue: Suavizado.
- AtmosferaSelva > AnimarSelva: intensidad y colores de la atmósfera.

Haz esos cambios fuera de Play y guarda con Ctrl+S. Si bajas más el salto o separas más las plataformas, vuelve a probar el recorrido: puede quedar una subida imposible.
