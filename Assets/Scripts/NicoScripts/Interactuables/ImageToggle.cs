using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToggle : Interactable
{
    [SerializeField]
    private RectTransform imageUI; // La imagen que se va a mostrar en el centro de la pantalla
    [SerializeField]
    private float scale = 1.0f; // Factor de escala de la imagen
    [SerializeField]
    private AudioClip toggleImageSound; // Agregamos una variable para el clip de audio
    private AudioSource audioSource; // Variable para el componente AudioSource

    private bool imageVisible;
    private Vector2 originalImageSize;
    private float previousTimeScale;

    void Start()
    {
        imageVisible = false;
        imageUI.gameObject.SetActive(imageVisible);

        // Obtener o añadir el componente AudioSource al GameObject del ImageToggle
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = toggleImageSound;

        // Guardar el tamaño original de la imagen
        Image imageComponent = imageUI.GetComponent<Image>();
        if (imageComponent != null && imageComponent.sprite != null)
        {
            originalImageSize = new Vector2(imageComponent.sprite.texture.width, imageComponent.sprite.texture.height);
        }
        else
        {
            Debug.LogError("El RectTransform no tiene un componente Image o no tiene una sprite asignada.");
        }

        // Ajustar tamaño y centrar la imagen
        CenterImage();
    }

    private void CenterImage()
    {
        if (originalImageSize != Vector2.zero)
        {
            imageUI.sizeDelta = originalImageSize * scale;
        }
        imageUI.anchorMin = new Vector2(0.5f, 0.5f);
        imageUI.anchorMax = new Vector2(0.5f, 0.5f);
        imageUI.pivot = new Vector2(0.5f, 0.5f);
        imageUI.anchoredPosition = Vector2.zero;
    }

    protected override void Interact() // Esta función se llama cuando se pulsa la tecla de interacción
    {
        imageVisible = !imageVisible;
        imageUI.gameObject.SetActive(imageVisible);

        if (imageVisible)
        {
            // Reproducir el sonido solo al mostrar la imagen
            if (audioSource != null && toggleImageSound != null)
            {
                audioSource.Play();
            }

            // Guardar el timeScale actual y congelar el tiempo
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else
        {
            // Restaurar el timeScale anterior
            Time.timeScale = previousTimeScale;
        }
    }

    // Método para cambiar la escala de la imagen en tiempo de ejecución
    public void SetScale(float newScale)
    {
        scale = newScale;
        CenterImage(); // Actualizar el tamaño de la imagen
    }
}
