using System.Collections;
using UnityEngine;

public class DisappearOnInteract : Interactable
{
    [SerializeField]
    private GameObject targetObject; // El objeto que desaparecerá
    [SerializeField]
    private float animationDuration = 1.0f; // Duración de la animación de color

    private Renderer targetRenderer;

    void Start()
    {
        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<Renderer>();
            if (targetRenderer == null)
            {
                Debug.LogError("El objeto objetivo no tiene un componente Renderer.");
            }
        }
        else
        {
            Debug.LogError("No se ha asignado ningún objeto objetivo.");
        }
    }

    void Update()
    {
        // Código adicional si es necesario
    }

    protected override void Interact()
    {
        if (targetObject != null)
        {
            StartCoroutine(DisappearAnimation());
        }
    }

    private IEnumerator DisappearAnimation()
    {
        if (targetRenderer != null)
        {
            Color originalColor = targetRenderer.material.color;
            Color targetColor = Color.red;
            float elapsedTime = 0.0f;

            // Animación de cambio de color
            while (elapsedTime < animationDuration)
            {
                targetRenderer.material.color = Color.Lerp(originalColor, targetColor, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Asegurarse de que el color final sea exactamente rojo
            targetRenderer.material.color = targetColor;

            // Espera un pequeño momento para que el cambio de color sea visible
            yield return new WaitForSeconds(0.1f);

            // Destruir el objeto
            Destroy(targetObject);
        }
    }
}
