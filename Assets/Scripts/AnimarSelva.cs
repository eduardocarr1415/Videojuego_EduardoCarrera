using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
// Este archivo da movimiento a la niebla, las luces y los puntitos de la selva del nivel.
// No se usa en el menú actual, que conserva su portada quieta.
public class AnimarSelva : MonoBehaviour
{
    // Estas casillas eligen la cámara, el piso de referencia, los colores y qué tanto se ven los efectos.
    public Camera camara;
    [Tooltip("Suelo cuya superficie marca la altura de la niebla.")]
    public BoxCollider2D sueloReferencia;
    [Min(0f)] public float alturaSobreSuelo = .35f;
    [Min(.1f)] public float espesorNeblina = 1.4f;
    public Color colorNeblina = new Color(.45f, .65f, .67f, 1f);
    public Color colorRayos = new Color(.61f, .77f, .81f, 1f);

    [Min(1f), Tooltip("Segundos que tarda en repetirse la animación.")]
    public float duracionCiclo = 10f;
    [Range(0f, 1f)] public float cantidadNeblina = .20f;
    [Range(0f, 1f)] public float intensidadRayos = .20f;
    [Range(0f, 1f)] public float brilloParticulas = .65f;

    // Guardamos la capa donde se dibujan y una lista de valores para cambiar su aspecto.
    private MeshRenderer capa;
    private MaterialPropertyBlock propiedades;
    private float segundos;

    // Estos nombres señalan las casillas del dibujo de niebla que vamos a cambiar.
    static readonly int Fase = Shader.PropertyToID("_Fase");
    static readonly int Aspecto = Shader.PropertyToID("_Aspecto");
    static readonly int Neblina = Shader.PropertyToID("_Neblina");
    static readonly int Rayos = Shader.PropertyToID("_Rayos");
    static readonly int Particulas = Shader.PropertyToID("_Particulas");
    static readonly int Altura = Shader.PropertyToID("_AlturaNeblina");
    static readonly int Espesor = Shader.PropertyToID("_EspesorNeblina");
    static readonly int ColorNiebla = Shader.PropertyToID("_ColorNeblina");
    static readonly int ColorLuz = Shader.PropertyToID("_ColorRayos");

    // Preparamos la capa y la lista de valores cuando se enciende este objeto.
    void OnEnable()
    {
        capa = GetComponent<MeshRenderer>();
        propiedades = new MaterialPropertyBlock();
        if (camara == null) camara = GetComponentInParent<Camera>();
        ActualizarCapa();
    }

    // Hacemos avanzar un reloj y volvemos a empezar al terminar el ciclo.
    // Al ganar o perder también se queda quieta la niebla.
    void LateUpdate()
    {
        if (Application.isPlaying)
            segundos = Mathf.Repeat(segundos + Time.deltaTime, Mathf.Max(1f, duracionCiclo));
        ActualizarCapa();
    }

    // Cuando se apaga, quitamos los cambios que habíamos puesto en esa capa.
    void OnDisable()
    {
        if (capa != null) capa.SetPropertyBlock(null);
    }

    // Esta parte ajusta el efecto al tamaño de la ventana y le manda sus valores al dibujo.
    void ActualizarCapa()
    {
        if (camara == null || !camara.orthographic || capa == null) return;

        // Agrandamos la capa hasta cubrir lo que se ve por la cámara.
        float alto = camara.orthographicSize * 2f * 1.002f;
        Vector3 escalaPadre = transform.parent == null ? Vector3.one : transform.parent.lossyScale;
        if (Mathf.Abs(escalaPadre.x) < .0001f || Mathf.Abs(escalaPadre.y) < .0001f) return;
        Vector3 escala = new Vector3(
            alto * camara.aspect / Mathf.Abs(escalaPadre.x),
            alto / Mathf.Abs(escalaPadre.y), 1f);
        if ((transform.localScale - escala).sqrMagnitude > .00000001f)
            transform.localScale = escala;

        // Le decimos al dibujo por qué parte del ciclo va y cuánta niebla, luz y brillo queremos.
        propiedades.SetFloat(Fase, segundos / Mathf.Max(1f, duracionCiclo));
        propiedades.SetFloat(Aspecto, camara.aspect);
        propiedades.SetFloat(Neblina, cantidadNeblina);
        propiedades.SetFloat(Rayos, intensidadRayos);
        propiedades.SetFloat(Particulas, brilloParticulas);
        // Buscamos la parte de arriba del piso para que la niebla quede cerca de la tierra.
        float alturaMundo = sueloReferencia == null
            ? camara.transform.position.y - camara.orthographicSize * .5f
            : sueloReferencia.bounds.max.y;
        Vector3 centroNiebla = new Vector3(camara.transform.position.x,
            alturaMundo + alturaSobreSuelo, 0f);
        propiedades.SetFloat(Altura, camara.WorldToViewportPoint(centroNiebla).y);
        propiedades.SetFloat(Espesor, espesorNeblina / (camara.orthographicSize * 2f));
        // Por último mandamos los colores y aplicamos todo a la capa.
        propiedades.SetColor(ColorNiebla, colorNeblina);
        propiedades.SetColor(ColorLuz, colorRayos);
        capa.SetPropertyBlock(propiedades);
    }
}
