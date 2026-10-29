using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
// Este archivo hace que la imagen de atrás llene la ventana sin verse estirada.
// También trabaja cuando estamos editando, para poder ver el resultado.
public class AjustarFondo : MonoBehaviour
{
    [Tooltip("Cámara cuya pantalla debe cubrir el fondo.")]
    // Conectamos la cámara y guardamos el dibujo que vamos a ajustar.
    public Camera camara;

    private SpriteRenderer imagen;

    // Al encenderse buscamos esas partes. Si falta la cámara, la buscamos en el padre.
    void OnEnable()
    {
        imagen = GetComponent<SpriteRenderer>();
        if (camara == null) camara = GetComponentInParent<Camera>();
        AjustarTamano();
    }

    // Revisamos el tamaño por si la ventana cambió de forma o tamaño.
    void LateUpdate()
    {
        AjustarTamano();
    }

    // Si falta una imagen o cámara válida, no hacemos nada para evitar errores.
    public void AjustarTamano()
    {
        if (camara == null || !camara.orthographic) return;
        if (imagen == null) imagen = GetComponent<SpriteRenderer>();
        if (imagen.sprite == null) return;

        // Leemos cuánto mide el dibujo antes de agrandarlo.
        Vector2 tamanoImagen = imagen.sprite.bounds.size;
        if (tamanoImagen.x <= 0f || tamanoImagen.y <= 0f) return;

        // Calculamos cuánto espacio ve la cámara a lo alto y a lo ancho.
        float altoPantalla = camara.orthographicSize * 2f;
        float anchoPantalla = altoPantalla * camara.aspect;
        // Elegimos el aumento que alcance para cubrir toda la pantalla.
        // Puede esconder un borde, pero conserva la forma del dibujo.
        float escala = Mathf.Max(
            anchoPantalla / tamanoImagen.x,
            altoPantalla / tamanoImagen.y
        ) * 1.002f; // Dejamos un poquito de margen para que no salga una raya en el borde.

        // También tomamos en cuenta si el objeto que lo contiene ya estaba agrandado.
        Vector3 escalaPadre = transform.parent == null
            ? Vector3.one : transform.parent.lossyScale;
        if (Mathf.Abs(escalaPadre.x) < .0001f || Mathf.Abs(escalaPadre.y) < .0001f) return;

        // Preparamos el tamaño final usando la misma proporción en los dos lados.
        Vector3 nuevoTamano = new Vector3(
            escala / Mathf.Abs(escalaPadre.x),
            escala / Mathf.Abs(escalaPadre.y),
            1f
        );

        // Solo cambiamos el tamaño cuando haga falta, no una y otra vez sin motivo.
        if ((transform.localScale - nuevoTamano).sqrMagnitude > .00000001f)
            transform.localScale = nuevoTamano;
    }
}
