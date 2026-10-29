using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
// el movimiento lo hace ControlJugador.
public class AnimarJugador : MonoBehaviour
{
    // Conectamos el que reproduce los dibujos y el que los muestra en pantalla.
    public Animator animador;
    public SpriteRenderer imagen;
    // Guardamos el cuerpo, hitbox o pregunta salte?
    private Rigidbody2D cuerpo;
    private BoxCollider2D caja;
    private ControlJugador controles;
    // Recordamos si estaba apoyado y cuánto falta para terminar la pose de aterrizaje.
    private bool primerFotograma = true;
    private bool estabaEnSuelo;
    private float tiempoAterrizaje;
    private int estadoActual;
    // Guardamos animaciones
    static readonly int Quieto = Animator.StringToHash("Base Layer.Quieto");
    static readonly int Caminar = Animator.StringToHash("Base Layer.Caminar");
    static readonly int Saltar = Animator.StringToHash("Base Layer.Saltar");
    // Buscamos las partes lde player.
    void Awake()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        caja = GetComponent<BoxCollider2D>();
        controles = GetComponent<ControlJugador>();
    }

    // Mientras el juego se mueve, revisamos la velocidad y si hay piso debajo de los pies.
    void Update()
    {
        if (Time.timeScale == 0f || controles == null) return;
        Vector2 pies = new Vector2(caja.bounds.center.x, caja.bounds.min.y - .03f);
        Vector2 zona = new Vector2(caja.bounds.size.x * .8f, .1f);
        bool enSuelo = cuerpo.linearVelocity.y <= .1f &&
            Physics2D.OverlapBox(pies, zona, 0f, controles.capaSuelo) != null;
        MostrarMovimiento(cuerpo.linearVelocity, enSuelo, Time.deltaTime);
    }

    // Aquí decidimos qué dibujo mostrar con los datos que acabamos de revisar.
    public void MostrarMovimiento(Vector2 velocidad, bool enSuelo, float segundos)
    {
        if (animador == null || imagen == null) return;
        // La primera revisión solo sirve para recordar cómo estaba al empezar.
        if (primerFotograma)
        {
            estabaEnSuelo = enSuelo;
            primerFotograma = false;
        }

        // Si avanza a la izquierda, volteamos el dibujo. Quieto conserva hacia dónde miraba.
        if (Mathf.Abs(velocidad.x) > .05f) imagen.flipX = velocidad.x < 0f;

        // En el aire mostramos la pose de despegar
        if (!enSuelo)
        {
            tiempoAterrizaje = 0f;
            float fuerza = controles == null ? 12f : Mathf.Max(1f, controles.fuerzaSalto);
            float fase = Mathf.Lerp(.25f, .5f, 1f - Mathf.Clamp01(velocidad.y / fuerza));
            MostrarPoseSalto(fase);
        }
        else
        {
            // Al volver al piso dejamos una décima de segundo para ver cómo aterriza.
            if (!estabaEnSuelo) tiempoAterrizaje = .10f;
            if (tiempoAterrizaje > 0f)
            {
                tiempoAterrizaje -= segundos;
                MostrarPoseSalto(.75f);
            }
            else
            {
                // Después elegimos caminar o quedarse quieto.
                // Solo empezamos otra animación si de verdad cambió lo que hace.
                int siguiente = Mathf.Abs(velocidad.x) > .05f ? Caminar : Quieto;
                animador.speed = 1f;
                if (estadoActual != siguiente)
                {
                    animador.Play(siguiente, 0, 0f);
                    estadoActual = siguiente;
                }
            }
        }
        estabaEnSuelo = enSuelo;
    }

    // Dejamos quieta la animación en el dibujo que corresponde a esa parte del salto.
    // Así no parece aterrizar mientras todavía está arriba.
    void MostrarPoseSalto(float fase)
    {
        animador.speed = 0f;
        animador.Play(Saltar, 0, fase);
        estadoActual = Saltar;
    }
}
