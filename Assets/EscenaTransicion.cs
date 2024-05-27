using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaTransicion : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip animacionFInal;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(CambiarEscena());
        }
    }

    IEnumerator CambiarEscena()
    {
        animator.SetTrigger("Iniciar");

       yield return new WaitForSeconds(animacionFInal.length);

       SceneManager.LoadScene(2); 
    }

}
