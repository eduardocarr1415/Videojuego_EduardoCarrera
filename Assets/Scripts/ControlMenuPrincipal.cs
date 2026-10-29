using UnityEngine;
using UnityEngine.SceneManagement;

// Este archivo va en AdministradorMenu. Atiende los botones de la portada.
public class ControlMenuPrincipal : MonoBehaviour
{
    // Guardamos el nombre del nivel y recordamos si ya se pidió abrirlo.
    public string escenaJuego = "juegofuncional";
    private bool cargando;

    // Al entrar al menú dejamos el tiempo normal y el ratón visible.
    void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // El botón JUGAR viene aquí. Abrimos el nivel una sola vez,
    // aunque alguien pulse muy rápido varias veces.
    public void Jugar()
    {
        if (cargando) return;
        cargando = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(escenaJuego);
    }

    // El botón SALIR viene aquí. En Unity dejamos de probar;
    // en el juego instalado cerramos la ventana.
    public void Salir()
    {
// Unity elige esta parte cuando probamos dentro del editor.
// La parte de abajo se usa cuando el juego ya está exportado.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
