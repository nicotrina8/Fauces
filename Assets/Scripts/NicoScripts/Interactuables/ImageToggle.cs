using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageToggle : Interactable
{
    [SerializeField]
    private RectTransform imageUI; // La imagen que se va a mostrar en el centro de la pantalla
    [SerializeField]
    private Vector2 imageSize = new Vector2(200, 200); // Tamaño de la imagen

    private bool imageVisible;

    void Start()
    {
        imageVisible = false;
        imageUI.gameObject.SetActive(imageVisible);

        // Ajustar tamaño y centrar la imagen
        CenterImage();
    }

    private void CenterImage()
    {
        imageUI.sizeDelta = imageSize;
        imageUI.anchorMin = new Vector2(0.5f, 0.5f);
        imageUI.anchorMax = new Vector2(0.5f, 0.5f);
        imageUI.pivot = new Vector2(0.5f, 0.5f);
        imageUI.anchoredPosition = Vector2.zero;
    }

    protected override void Interact() // Esta función se llama cuando se pulsa la tecla de interacción
    {
        imageVisible = !imageVisible;
        imageUI.gameObject.SetActive(imageVisible);
    }
}
