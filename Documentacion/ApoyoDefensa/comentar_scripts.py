from pathlib import Path
import re, json, hashlib

root = Path(r'C:\Users\soyel\Marihuanero')
rules = {
'ControlJugador.cs': [
('public class ControlJugador', 'Este archivo va en Jugador. Aquí leemos las teclas, movemos al señor\ny avisamos si recoge una hoja, llega al Patrón o toca un peligro.'),
('    public float rapidez', 'Aquí elegimos qué tan rápido corre y qué tan alto salta.\nSon valores para objetos nuevos; en Unity podemos tener otros guardados.'),
('    public LayerMask', 'Esta casilla dice cuáles objetos cuentan como suelo.'),
('    private Rigidbody2D', 'Guardamos el cuerpo que se mueve y la caja invisible que choca con el piso.'),
('    private float direccion', 'direccion vale -1 a la izquierda, 0 quieto y 1 a la derecha.\nsaltoPendiente recuerda que acabamos de pedir un salto.'),
('    void Awake()', 'Antes de empezar, buscamos el cuerpo y la caja del mismo jugador.'),
('    void Update()', 'Aquí revisamos el teclado mientras jugamos.\nSi la partida terminó, ya no aceptamos movimientos.'),
('        if (transform.position.y < -7f)', 'Si el señor cae muy abajo, entendemos que se fue por un hueco y perdió.'),
('        direccion = 0f;', 'Al principio de cada revisión lo dejamos quieto; las teclas deciden el lado.'),
('        if (Keyboard.current.aKey', 'A o la flecha izquierda piden moverse hacia la izquierda.'),
('        if (Keyboard.current.dKey', 'D o la flecha derecha piden moverse hacia la derecha.'),
('        if (Keyboard.current.spaceKey', 'Una pulsación de Espacio guarda la petición.\nLa usamos después, cuando Unity vaya a mover el cuerpo.'),
('    void FixedUpdate()', 'Aquí aplicamos el movimiento del cuerpo por pasos regulares de Unity.'),
('        Vector2 posicionPies', 'Ponemos una cajita pequeña debajo de los pies para preguntar si hay piso.'),
('        bool enSuelo', 'La respuesta es sí o no. Solo buscamos en los objetos marcados como suelo.'),
('        Vector2 velocidad', 'Cambiamos el avance a los lados, pero conservamos la subida o caída que ya lleva.'),
('        if (saltoPendiente && enSuelo)', 'Solo saltamos si se pidió y los pies están apoyados.\nAsí no se puede seguir saltando en el aire. También hacemos sonar el salto.'),
('        cuerpo.linearVelocity = velocidad;', 'Ahora sí entregamos al cuerpo la velocidad que acabamos de preparar.'),
('        saltoPendiente = false;', 'Borramos la petición para no usar dos veces la misma pulsación.'),
('    void OnTriggerEnter2D', 'Unity entra aquí cuando tocamos una zona de aviso.\nMiramos su etiqueta para saber qué objeto era.'),
('        if (otro.CompareTag("Hoja"))', 'Al tocar una hoja, apagamos su aviso, sumamos una y quitamos ese dibujo.'),
('        else if (otro.CompareTag("Meta"))', 'Al tocar la meta pedimos revisar si ya tenemos las diez hojas.'),
('        else if (otro.CompareTag("Pinchos"))', 'Al tocar pinchos avisamos que el jugador perdió.')],
'ControlNivel.cs': [
('public class ControlNivel', 'Este archivo va en AdministradorNivel. Es el encargado de contar las hojas,\nponer los objetos del recorrido y decidir si ganamos o perdimos.'),
('    public TMP_Text textoHojas;', 'Aquí conectamos los textos, los dibujos que se van a copiar, los paneles\ny los sonidos. Esas casillas se llenan arrastrando objetos en Unity.'),
('    private int hojasRecogidas;', 'Guardamos cuántas hojas van y si la partida ya terminó. La meta pide diez.'),
('    public Vector2[] posicionesHojas', 'Esta lista dice dónde aparece cada hoja: el primer número es a los lados\ny el segundo es la altura. También se puede cambiar desde Unity.'),
('    public float[] posicionesPinchos', 'Estos números dicen dónde ponemos los grupos de pinchos sobre el piso.'),
('    void Start()', 'Al iniciar una partida dejamos el tiempo normal, el contador en cero\ny los paneles finales escondidos. Después ponemos hojas y pinchos.'),
('    void CrearHojas()', 'Por cada lugar de la lista hacemos una copia del molde de hoja.\nPor eso no hay que dibujar ni programar diez hojas por separado.'),
('    public void RecogerHoja()', 'Si todavía jugamos, sumamos una hoja, hacemos su sonido y cambiamos el contador.'),
('    void ActualizarContador()', 'Escribimos la cuenta en el texto que ya existe, por ejemplo Hojas: 3 / 10.'),
('    public void IntentarGanar()', 'Esto se revisa al tocar al Patrón. Primero miramos si la partida sigue activa.'),
('        if (hojasRecogidas < totalHojas)', 'Si faltan hojas mostramos un aviso y dejamos que el jugador siga buscando.'),
('        victoriaMostrada = true;', 'Con diez hojas marcamos la victoria una vez, ponemos su audio y su pantalla,\ny detenemos el movimiento del juego.'),
('    public void ReiniciarNivel()', 'El botón Reintentar usa esto: vuelve a cargar el nivel desde el principio.'),
('    void CrearPinchos()', 'Hacemos una copia del molde de pinchos en cada lugar de su lista.\nLa altura está elegida para que la base apoye en el piso.'),
('    public void Morir()', 'Si tocamos pinchos o caemos por un hueco, marcamos la derrota una vez.\nEl sonido empieza enseguida, aparece el panel y se detiene el juego.')],
'ControlMenuPrincipal.cs': [
('public class ControlMenuPrincipal', 'Este archivo va en AdministradorMenu. Atiende los botones de la portada.'),
('    public string escenaJuego', 'Guardamos el nombre del nivel y recordamos si ya se pidió abrirlo.'),
('    void Awake()', 'Al entrar al menú dejamos el tiempo normal y el ratón visible.'),
('    public void Jugar()', 'El botón JUGAR viene aquí. Abrimos el nivel una sola vez,\naunque alguien pulse muy rápido varias veces.'),
('    public void Salir()', 'El botón SALIR viene aquí. En Unity dejamos de probar;\nen el juego instalado cerramos la ventana.'),
('#if UNITY_EDITOR', 'Unity elige esta parte cuando probamos dentro del editor.\nLa parte de abajo se usa cuando el juego ya está exportado.')],
'SonidosJuego.cs': [
('public class SonidosJuego', 'Este archivo va en AdministradorNivel. Aquí elegimos qué sonido se escucha\nal saltar, recoger una hoja, perder o ganar.'),
('    public AudioClip salto;', 'Aquí ponemos las cuatro grabaciones, como elegir canciones para un reproductor.'),
('    public AudioSource musicaFondo;', 'Este es otro reproductor: se ocupa de la canción que suena mientras jugamos.'),
('    [Range(0f, 1f)] public float volumenSalto', 'Estas casillas cambian el volumen. Cero no se escucha; uno es el volumen completo.'),
('    private AudioSource fuente;', 'Guardamos el reproductor de efectos y si ya pusimos un resultado final.'),
('    void Awake()', 'Preparamos el reproductor: sin repetirse solo y sin depender de dónde esté la cámara.'),
('        foreach (AudioClip clip', 'Dejamos los archivos listos antes de usarlos para evitar una espera al primer sonido.'),
('    public void Saltar()', 'Cuando el salto sí ocurrió, reproducimos su sonido una vez.'),
('    public void RecogerHoja()', 'Cada hoja pide su efecto. Puede sonar aunque el anterior aún esté terminando.'),
('    public void Perder()', 'Perder y Ganar usan el mismo proceso, pero le pasan una grabación diferente.'),
('    private void ReproducirResultado', 'Primero evitamos que el final suene varias veces por contactos repetidos.'),
('        if (musicaFondo != null)', 'Quitamos la canción de fondo y los efectos anteriores para escuchar el resultado.'),
('        if (sonido != null)', 'Ponemos el audio elegido. Aunque se pare el movimiento, el audio puede terminar.')],
'CamaraSigue.cs': [
('public class CamaraSigue', 'Este archivo va en Main Camera. Acompaña al señor por el nivel\ny deja ver un poco de camino por delante.'),
('    public Transform jugador;', 'Aquí conectamos a quién seguir. Suavizado ayuda a corregir el encuadre sin un salto brusco.'),
('    private Vector3 velocidad;', 'Recordamos la posición anterior y unos datos que necesita la cámara para seguir suave.'),
('    void OnEnable()', 'Cada vez que se activa, empezamos a preparar un seguimiento nuevo.'),
('    void LateUpdate()', 'Movemos la cámara después de que el personaje ya se haya movido.'),
('        if (!encuadrePreparado)', 'La primera vez ponemos la cámara dos unidades por delante del señor.\nAsí no tiene que alcanzarlo desde lejos al empezar.'),
('        float avance', 'Miramos cuánto avanzó desde la última imagen y acompañamos esa misma distancia.\nEsto evita que la cámara se quede atrás y parezca empujarlo al saltar.'),
('        Vector3 destino', 'Conservamos la altura y la distancia de la cámara; solo seguimos hacia los lados.'),
('        transform.position = Vector3.SmoothDamp', 'Si el encuadre se desajustó un poco, lo corregimos despacio.')],
'AnimarJugador.cs': [
('public class AnimarJugador', 'Este archivo cambia el dibujo del señor según lo que esté haciendo.\nNo lo empuja: el movimiento lo hace ControlJugador.'),
('    public Animator animador;', 'Conectamos el que reproduce los dibujos y el que los muestra en pantalla.'),
('    private Rigidbody2D cuerpo;', 'Guardamos el cuerpo, la caja y el control para preguntar cómo va el personaje.'),
('    private bool primerFotograma', 'Recordamos si estaba apoyado y cuánto falta para terminar la pose de aterrizaje.'),
('    static readonly int Quieto', 'Guardamos los nombres de las tres animaciones para poder elegirlas después.'),
('    void Awake()', 'Buscamos las partes del mismo jugador que vamos a consultar.'),
('    void Update()', 'Mientras el juego se mueve, revisamos la velocidad y si hay piso debajo de los pies.'),
('    public void MostrarMovimiento', 'Aquí decidimos qué dibujo mostrar con los datos que acabamos de revisar.'),
('        if (primerFotograma)', 'La primera revisión solo sirve para recordar cómo estaba al empezar.'),
('        if (Mathf.Abs(velocidad.x)', 'Si avanza a la izquierda, volteamos el dibujo. Quieto conserva hacia dónde miraba.'),
('        if (!enSuelo)', 'En el aire mostramos la pose de despegar y luego la de volar o caer,\nsegún la subida que todavía le quede.'),
('            if (!estabaEnSuelo)', 'Al volver al piso dejamos una décima de segundo para ver cómo aterriza.'),
('                int siguiente', 'Después elegimos caminar o quedarse quieto.\nSolo empezamos otra animación si de verdad cambió lo que hace.'),
('    void MostrarPoseSalto', 'Dejamos quieta la animación en el dibujo que corresponde a esa parte del salto.\nAsí no parece aterrizar mientras todavía está arriba.')],
'AjustarFondo.cs': [
('public class AjustarFondo', 'Este archivo hace que la imagen de atrás llene la ventana sin verse estirada.\nTambién trabaja cuando estamos editando, para poder ver el resultado.'),
('    public Camera camara;', 'Conectamos la cámara y guardamos el dibujo que vamos a ajustar.'),
('    void OnEnable()', 'Al encenderse buscamos esas partes. Si falta la cámara, la buscamos en el padre.'),
('    void LateUpdate()', 'Revisamos el tamaño por si la ventana cambió de forma o tamaño.'),
('    public void AjustarTamano()', 'Si falta una imagen o cámara válida, no hacemos nada para evitar errores.'),
('        Vector2 tamanoImagen', 'Leemos cuánto mide el dibujo antes de agrandarlo.'),
('        float altoPantalla', 'Calculamos cuánto espacio ve la cámara a lo alto y a lo ancho.'),
('        float escala =', 'Elegimos el aumento que alcance para cubrir toda la pantalla.\nPuede esconder un borde, pero conserva la forma del dibujo.'),
('        Vector3 escalaPadre', 'También tomamos en cuenta si el objeto que lo contiene ya estaba agrandado.'),
('        Vector3 nuevoTamano', 'Preparamos el tamaño final usando la misma proporción en los dos lados.'),
('        if ((transform.localScale', 'Solo cambiamos el tamaño cuando haga falta, no una y otra vez sin motivo.')],
'AnimarSelva.cs': [
('public class AnimarSelva', 'Este archivo da movimiento a la niebla, las luces y los puntitos de la selva del nivel.\nNo se usa en el menú actual, que conserva su portada quieta.'),
('    public Camera camara;', 'Estas casillas eligen la cámara, el piso de referencia, los colores y qué tanto se ven los efectos.'),
('    private MeshRenderer capa;', 'Guardamos la capa donde se dibujan y una lista de valores para cambiar su aspecto.'),
('    static readonly int Fase', 'Estos nombres señalan las casillas del dibujo de niebla que vamos a cambiar.'),
('    void OnEnable()', 'Preparamos la capa y la lista de valores cuando se enciende este objeto.'),
('    void LateUpdate()', 'Hacemos avanzar un reloj y volvemos a empezar al terminar el ciclo.\nAl ganar o perder también se queda quieta la niebla.'),
('    void OnDisable()', 'Cuando se apaga, quitamos los cambios que habíamos puesto en esa capa.'),
('    void ActualizarCapa()', 'Esta parte ajusta el efecto al tamaño de la ventana y le manda sus valores al dibujo.'),
('        float alto =', 'Agrandamos la capa hasta cubrir lo que se ve por la cámara.'),
('        propiedades.SetFloat(Fase', 'Le decimos al dibujo por qué parte del ciclo va y cuánta niebla, luz y brillo queremos.'),
('        float alturaMundo', 'Buscamos la parte de arriba del piso para que la niebla quede cerca de la tierra.'),
('        propiedades.SetColor', 'Por último mandamos los colores y aplicamos todo a la capa.')],
}

token_re = re.compile(r'"(?:\\.|[^"\\])*"|\'(?:\\.|[^\'\\])*\'|//[^\n]*|/\*.*?\*/', re.S)
def code_only(s):
    s = token_re.sub(lambda m: '' if m.group().startswith(('//','/*')) else m.group(), s)
    return re.findall(r'"(?:\\.|[^"\\])*"|\'(?:\\.|[^\'\\])*\'|\w+|[^\s]',s)

prepared=[]
for name, anchors in rules.items():
    path=root/'Assets'/'Scripts'/name
    before=path.read_text(encoding='utf-8-sig')
    after='\n'.join(line for line in before.splitlines() if not line.lstrip().startswith('//'))+'\n'
    # Cambiamos solo comentarios; las líneas de instrucciones se dejan tal como estaban.
    for anchor,comment in anchors:
        lines=after.splitlines(True)
        found=[i for i,line in enumerate(lines) if line.startswith(anchor)]
        if not found: raise RuntimeError(f'No se encontró {name}: {anchor}')
        i=found[0]; indent=re.match(r'\s*',lines[i]).group()
        lines[i:i]=[indent+'// '+part+'\n' for part in comment.split('\n')]
        after=''.join(lines)
    after=after.replace('// Sonido 2D: se oye igual en todo el nivel.','// Se escucha igual en cualquier parte del nivel.')
    after=after.replace('// Un margen pequeño evita líneas en los bordes.','// Dejamos un poquito de margen para que no salga una raya en el borde.')
    if code_only(before)!=code_only(after): raise RuntimeError('Cambió una instrucción en '+name)
    prepared.append((path,before,after))

backup=root/'Documentacion'/'RespaldoScriptsAntesDeComentarios'
backup.mkdir(exist_ok=True)
report=[]
for path,before,after in prepared:
    saved=backup/(path.name+'.txt')
    if not saved.exists(): saved.write_text(before,encoding='utf-8')
    path.write_text(after,encoding='utf-8')
    report.append({'archivo':path.name,'instrucciones_iguales':True,'comentarios':sum(l.lstrip().startswith('//') for l in after.splitlines()),'sha_instrucciones':hashlib.sha256(json.dumps(code_only(after),ensure_ascii=False).encode()).hexdigest()})
(root/'Documentacion'/'ApoyoDefensa'/'Verificacion_comentarios.json').write_text(json.dumps(report,ensure_ascii=False,indent=2),encoding='utf-8')
print(json.dumps(report,ensure_ascii=False,indent=2))
