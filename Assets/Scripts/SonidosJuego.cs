using UnityEngine;

[RequireComponent(typeof(AudioSource))]
// Este archivo va en AdministradorNivel. Aquí elegimos qué sonido se escucha
// al saltar, recoger una hoja, perder o ganar.
public class SonidosJuego : MonoBehaviour
{
    // Aquí ponemos las cuatro grabaciones, como elegir canciones para un reproductor.
    public AudioClip salto;
    public AudioClip hoja;
    public AudioClip derrota;
    public AudioClip victoria;
    // Este es otro reproductor: se ocupa de la canción que suena mientras jugamos.
    public AudioSource musicaFondo;

    // Estas casillas cambian el volumen. Cero no se escucha; uno es el volumen completo.
    [Range(0f, 1f)] public float volumenSalto = 1f;
    [Range(0f, 1f)] public float volumenHoja = 0.4f;
    [Range(0f, 1f)] public float volumenResultado = 0.65f;

    // Guardamos el reproductor de efectos y si ya pusimos un resultado final.
    private AudioSource fuente;
    private bool partidaTerminada;

    // Preparamos el reproductor: sin repetirse solo y sin depender de dónde esté la cámara.
    void Awake()
    {
        fuente = GetComponent<AudioSource>();
        fuente.playOnAwake = false;
        fuente.loop = false;
        fuente.spatialBlend = 0f; // Se escucha igual en cualquier parte del nivel.
        // Dejamos los archivos listos antes de usarlos para evitar una espera al primer sonido.
        foreach (AudioClip clip in new[] { salto, hoja, derrota, victoria })
            if (clip != null) clip.LoadAudioData();
    }

    // Cuando el salto sí ocurrió, reproducimos su sonido una vez.
    public void Saltar()
    {
        if (!partidaTerminada && salto != null)
            fuente.PlayOneShot(salto, volumenSalto);
    }

    // Cada hoja pide su efecto. Puede sonar aunque el anterior aún esté terminando.
    public void RecogerHoja()
    {
        if (!partidaTerminada && hoja != null)
            fuente.PlayOneShot(hoja, volumenHoja);
    }

    // Perder y Ganar usan el mismo proceso, pero le pasan una grabación diferente.
    public void Perder() { ReproducirResultado(derrota); }
    public void Ganar() { ReproducirResultado(victoria); }

    // Primero evitamos que el final suene varias veces por contactos repetidos.
    private void ReproducirResultado(AudioClip sonido)
    {
        if (partidaTerminada) return;
        partidaTerminada = true;
        // Quitamos la canción de fondo y los efectos anteriores para escuchar el resultado.
        if (musicaFondo != null) musicaFondo.Stop();
        fuente.Stop();
        // Ponemos el audio elegido. Aunque se pare el movimiento, el audio puede terminar.
        if (sonido != null) fuente.PlayOneShot(sonido, volumenResultado);
    }
}
