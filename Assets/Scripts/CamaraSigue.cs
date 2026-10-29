using UnityEngine;

// Este archivo va en Main Camera. Acompaña al señor por el nivel
// y deja ver un poco de camino por delante.
public class CamaraSigue : MonoBehaviour
{
    // Aquí conectamos a quién seguir. Suavizado ayuda a corregir el encuadre sin un salto brusco.
    public Transform jugador;
    public float suavizado = 0.2f;

    // Recordamos la posición anterior y unos datos que necesita la cámara para seguir suave.
    private Vector3 velocidad;
    private float ultimaXJugador;
    private bool encuadrePreparado;

    // Cada vez que se activa, empezamos a preparar un seguimiento nuevo.
    void OnEnable()
    {
        encuadrePreparado = false;
        velocidad = Vector3.zero;
    }

    // Movemos la cámara después de que el personaje ya se haya movido.
    void LateUpdate()
    {
        if (jugador == null) return;

        // La primera vez ponemos la cámara dos unidades por delante del señor.
        // Así no tiene que alcanzarlo desde lejos al empezar.
        if (!encuadrePreparado)
        {
            transform.position = new Vector3(jugador.position.x + 2f,
                transform.position.y, transform.position.z);
            ultimaXJugador = jugador.position.x;
            encuadrePreparado = true;
            return;
        }

        // Miramos cuánto avanzó desde la última imagen y acompañamos esa misma distancia.
        // Esto evita que la cámara se quede atrás y parezca empujarlo al saltar.
        float avance = jugador.position.x - ultimaXJugador;
        ultimaXJugador = jugador.position.x;
        transform.position += new Vector3(avance, 0f, 0f);

        // Conservamos la altura y la distancia de la cámara; solo seguimos hacia los lados.
        Vector3 destino = new Vector3(
            jugador.position.x + 2f,
            transform.position.y,
            transform.position.z
        );

        // Si el encuadre se desajustó un poco, lo corregimos despacio.
        transform.position = Vector3.SmoothDamp(
            transform.position,
            destino,
            ref velocidad,
            suavizado
        );
    }
}
