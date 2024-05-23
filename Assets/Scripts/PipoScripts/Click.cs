using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip sound; // El clip de sonido a reproducir
    private AudioSource audioSource; // El componente AudioSource

    void Awake()
    {
        // Añade el componente AudioSource si no está presente
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Asegúrate de que no se reproduzca automáticamente
    }

    // Método para reproducir el sonido
    public void PlaySound()
    {
        Debug.Log("PlaySound called"); // Mensaje de depuración
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
            Debug.Log("Sound played"); // Mensaje de depuración
        }
        else
        {
            Debug.Log("No sound assigned"); // Mensaje de depuración
        }
    }
}