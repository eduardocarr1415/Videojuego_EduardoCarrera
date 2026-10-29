using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Este archivo va en AdministradorNivel. Es el encargado de contar las hojas,
// poner los objetos del recorrido y decidir si ganamos o perdimos.
public class ControlNivel : MonoBehaviour
{
    // Aquí conectamos los textos, los dibujos que se van a copiar, los paneles
    // y los sonidos. Esas casillas se llenan arrastrando objetos en Unity.
    public TMP_Text textoHojas;
    public TMP_Text textoAviso;
    public GameObject prefabHoja;
    public GameObject prefabPinchos;
    public GameObject panelDerrota;
    public GameObject panelVictoria;
    public SonidosJuego sonidos;

    // Guardamos cuántas hojas van y si la partida ya terminó. La meta pide diez.
    private int hojasRecogidas;
    private const int totalHojas = 10;
    private bool derrotaMostrada;
    private bool victoriaMostrada;
    public bool PartidaTerminada => derrotaMostrada || victoriaMostrada;

    // Esta lista dice dónde aparece cada hoja: el primer número es a los lados
    // y el segundo es la altura. También se puede cambiar desde Unity.
    public Vector2[] posicionesHojas =
    {
        new Vector2(5f, -.25f), new Vector2(12f, .85f),
        new Vector2(19f, .1f), new Vector2(26f, .65f),
        new Vector2(33f, -.3f), new Vector2(40f, .85f),
        new Vector2(47f, .15f), new Vector2(54f, .75f),
        new Vector2(68f, .7f), new Vector2(82f, .65f)
    };

    // Estos números dicen dónde ponemos los grupos de pinchos sobre el piso.
    public float[] posicionesPinchos = { 9f, 16f, 22f, 31f, 36f, 43f, 50f, 56f, 65f, 71f, 78f, 85f };

    // Al iniciar una partida dejamos el tiempo normal, el contador en cero
    // y los paneles finales escondidos. Después ponemos hojas y pinchos.
    void Start()
    {
        Time.timeScale = 1f;
        hojasRecogidas = 0;
        derrotaMostrada = false;
        victoriaMostrada = false;
        textoAviso.text = "";
        panelDerrota.SetActive(false);
        panelVictoria.SetActive(false);
        CrearHojas();
        CrearPinchos();
        ActualizarContador();
    }

    // Por cada lugar de la lista hacemos una copia del molde de hoja.
    // Por eso no hay que dibujar ni programar diez hojas por separado.
    void CrearHojas()
    {
        foreach (Vector2 posicion in posicionesHojas)
            Instantiate(prefabHoja, new Vector3(posicion.x, posicion.y, 0f), Quaternion.identity);
    }

    // Si todavía jugamos, sumamos una hoja, hacemos su sonido y cambiamos el contador.
    public void RecogerHoja()
    {
        if (PartidaTerminada) return;
        hojasRecogidas++;
        sonidos?.RecogerHoja();
        ActualizarContador();
    }

    // Escribimos la cuenta en el texto que ya existe, por ejemplo Hojas: 3 / 10.
    void ActualizarContador()
    {
        textoHojas.text = "Hojas: " + hojasRecogidas + " / " + totalHojas;
    }

    // Esto se revisa al tocar al Patrón. Primero miramos si la partida sigue activa.
    public void IntentarGanar()
    {
        if (PartidaTerminada) return;
        // Si faltan hojas mostramos un aviso y dejamos que el jugador siga buscando.
        if (hojasRecogidas < totalHojas)
        {
            textoAviso.text = "¡Te faltan hojas!";
            return;
        }
        // Con diez hojas marcamos la victoria una vez, ponemos su audio y su pantalla,
        // y detenemos el movimiento del juego.
        victoriaMostrada = true;
        sonidos?.Ganar();
        textoAviso.text = "";
        panelVictoria.SetActive(true);
        Time.timeScale = 0f;
    }

    // El botón Reintentar usa esto: vuelve a cargar el nivel desde el principio.
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Hacemos una copia del molde de pinchos en cada lugar de su lista.
    // La altura está elegida para que la base apoye en el piso.
    void CrearPinchos()
    {
        foreach (float posicionX in posicionesPinchos)
            Instantiate(prefabPinchos, new Vector3(posicionX, -2.2f, 0f), Quaternion.identity);
    }

    // Si tocamos pinchos o caemos por un hueco, marcamos la derrota una vez.
    // El sonido empieza enseguida, aparece el panel y se detiene el juego.
    public void Morir()
    {
        if (PartidaTerminada) return;
        derrotaMostrada = true;
        sonidos?.Perder();
        panelDerrota.SetActive(true);
        Time.timeScale = 0f;
    }
}
