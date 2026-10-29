using UnityEngine;
using UnityEngine.InputSystem;

// Este archivo va en Jugador. Aquí leemos las teclas, movemos al señor
// y avisamos si recoge una hoja, llega al Patrón o toca un peligro.
public class ControlJugador : MonoBehaviour
{
    public float rapidez = 6f;
    public float fuerzaSalto = 10.5f;
    // Esta casilla dice cuáles objetos cuentan como suelo.
    public LayerMask capaSuelo;
    private Rigidbody2D cuerpo;
    private BoxCollider2D caja;
    // direccion -1 a la izquierda, 0 quieto y 1 a la derecha.
    private float direccion;
    private bool saltoPendiente;
    public ControlNivel nivel;

    // Antes de empezar, buscamos el cuerpo y la caja del mismo jugador.
    void Awake()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        caja = GetComponent<BoxCollider2D>();
    }

    // Si la partida terminó, ya no aceptamos movimientos.
    void Update()
    {
        if (nivel != null && nivel.PartidaTerminada) return;
        // huevo=murio
        if (transform.position.y < -7f)
        {
            nivel.Morir();
            return;
        }

        // principio quito y ayd direccion
        direccion = 0f;

        if (Keyboard.current == null) return;

        // A <-
        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            direccion -= 1f;
        }

        // D ->
        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            direccion += 1f;
        }

        // Una pulsación de Espacio guarda la petición.
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            saltoPendiente = true;
        }
    }

    // Aquí aplicamos el movimiento del cuerpo por pasos regulares de Unity.
    void FixedUpdate()
    {
        // hay hitbox? podemos saltar?
        Vector2 posicionPies = new Vector2(
            caja.bounds.center.x,
            caja.bounds.min.y - 0.03f
        );

        Vector2 tamanoDeteccion = new Vector2(
            caja.bounds.size.x * 0.8f,
            0.1f
        );

        //  si o no
        bool enSuelo = Physics2D.OverlapBox(
            posicionPies,
            tamanoDeteccion,
            0f,
            capaSuelo
        ) != null;

        // moverse en pleno salto mientras caemos
        Vector2 velocidad = new Vector2(
            direccion * rapidez,
            cuerpo.linearVelocity.y
        );

        // saltar si esque hay peticion con sonido 
        if (saltoPendiente && enSuelo)
        {
            velocidad.y = fuerzaSalto;
            nivel.sonidos?.Saltar();
        }
        cuerpo.linearVelocity = velocidad;
        // Borramos la petición 
        saltoPendiente = false;
    }
    // Unity entra aquí cuando tocamos una zona de aviso.
    void OnTriggerEnter2D(Collider2D otro)
    {
        if (nivel != null && nivel.PartidaTerminada) return;
        // Al tocar una hoja quitamos el aviso y sumamos una
        if (otro.CompareTag("Hoja"))
        {
            otro.enabled = false;
            nivel.RecogerHoja();
            Destroy(otro.gameObject);
        }
        // Al tocar la meta pedimos revisar
        else if (otro.CompareTag("Meta"))
        {
            nivel.IntentarGanar();
        }
        // Al tocar pinchos avisamos que el jugador perdió
        else if (otro.CompareTag("Pinchos"))
        {
            nivel.Morir();
        }
    }
}
