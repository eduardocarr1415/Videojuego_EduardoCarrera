# Menú principal de Marihuanero

La escena está en Assets/Scenes/MenuPrincipal.unity. Va antes de juegofuncional en Build Profiles, así el ejecutable comienza por el menú.

## Lo que aparece

- Frase de arriba: «PLATA O PLOMO».
- Nombre: MARIHUANERO.
- JUGAR: entra al nivel.
- NIVELES: queda visible y se puede seleccionar, pero no tiene ninguna acción conectada.
- SALIR: cierra el juego exportado. Dentro de Unity sale del modo Play para poder probarlo.

## La portada y el fondo

Ahora usa la imagen vertical enviada por el usuario, guardada en Assets/Imagenes/Menu/PortadaMarihuanero.jpg. El JPG original no se recortó ni se pintó de nuevo. PortadaCompleta muestra toda la ilustración a la izquierda, conservando sus proporciones. El panel y los botones se colocaron a la derecha, con tonos naranja, rosa y morado que combinan con la imagen.

La versión actual es Gemini_Generated_Image_b3hzb4b3hzb4b3hz.jpg, con selva, cabañas y camioneta. Se sustituyó el contenido del JPG del proyecto conservando su archivo .meta, de modo que el póster y el fondo usan la nueva imagen sin reconectar casillas. La portada anterior quedó en Documentacion/HistorialPortadas/PortadaAnteriorCiudad.jpg.

La misma imagen aparece ampliada y oscurecida detrás del menú para llenar la pantalla. Esa copia de fondo puede quedar recortada; el póster lateral sí se muestra completo. El fondo del menú ahora es estático: retiramos la capa de niebla, rayos y motas que venía de la selva.

CamaraMenu conserva FondoPortada, que muestra el JPG actual. AjustarFondo mantiene las proporciones y cubre la pantalla cuando cambia el tamaño de la ventana. AtmosferaSelva y su componente AnimarSelva se eliminaron de esta escena.

El objeto que antes se llamaba FondoSelva ahora se llama FondoPortada. OscurecerSelva pasó a llamarse OscurecerFondo: solo es una capa de contraste para leer bien la interfaz. Los recursos de selva y niebla siguen disponibles para juegofuncional, donde sí se usan.

## Cómo están montados los botones

CanvasMenu contiene el fondo oscuro y Contenido. Dentro de Contenido están el slogan, el título, los tres botones y el recordatorio del teclado. Los textos usan TextMeshPro. El CanvasScaler toma como referencia 1920 por 1080 y ajusta el tamaño a la ventana.

PortadaCompleta es un Image adicional dentro del Canvas. Preserve Aspect evita que la ilustración se ensanche. Raycast Target está desactivado para que la portada no intercepte clics de los botones. Para cambiarla, arrastra otro sprite al campo Source Image.

El botón principal es naranja; los otros dos usan morado oscuro. Button cambia el tono al pasar el ratón o seleccionarlos. Las imágenes de fondo y las letras no bloquean el ratón. Se puede navegar con el teclado y confirmar con el botón de envío del sistema de entrada.

## ControlMenuPrincipal.cs, explicado por partes

**escenaJuego:** guarda el nombre juegofuncional, que es la escena del nivel. No incluye la extensión .unity.

**cargando:** recuerda si ya pulsaste Jugar para evitar intentar cargar dos veces seguidas.

**Awake:** devuelve Time.timeScale a 1 y deja visible el cursor, por si venimos de una escena con la partida detenida.

**Jugar:** marca cargando, devuelve el tiempo normal y llama a SceneManager.LoadScene(escenaJuego). Unity sustituye el menú por el nivel; ControlNivel empieza una partida nueva.

**Salir:** usa Application.Quit en el juego exportado. Dentro del bloque UNITY_EDITOR detiene Play. Esa parte solo se compila para el editor, no se incluye en el ejecutable.

**Niveles:** no necesita un método vacío: simplemente dejamos su lista On Click sin funciones.

## Cómo cambiarlo tú

1. Abre MenuPrincipal desde Assets/Scenes, fuera de Play.
2. En CanvasMenu > Contenido selecciona el texto o botón.
3. Cambia Text Input en TextMeshPro para escribir otra frase.
4. Para ver qué hace un botón, selecciona su objeto principal y revisa Button > On Click.
5. JUGAR apunta a AdministradorMenu > ControlMenuPrincipal.Jugar.
6. SALIR apunta a AdministradorMenu > ControlMenuPrincipal.Salir.
7. Para cambiar el fondo, selecciona CamaraMenu > FondoPortada. Para cambiar el póster, selecciona CanvasMenu > PortadaCompleta.
8. Guarda con Ctrl+S y prueba en Play.

No añadí todavía la pausa con Escape ni una pantalla de selección de niveles.

El objeto interno del botón principal conserva el nombre BotonPlataOPlomo, pero su texto visible ahora dice JUGAR. Su acción sigue siendo ControlMenuPrincipal.Jugar.

