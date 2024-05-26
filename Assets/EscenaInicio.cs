using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaInicio : MonoBehaviour
{
    private Animator animatorInicio;
    [SerializeField] private AnimationClip animacionFInal;

    private void Start()
    {
        animatorInicio = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CambiarEscena());
        }
    }

    IEnumerator CambiarEscena()
    {
        animatorInicio.SetTrigger("Iniciar");

       yield return new WaitForSeconds(animacionFInal.length);

       SceneManager.LoadScene(1); 
    }

}
