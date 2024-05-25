using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject footstep;  // Corrección del nombre del tipo

    // Start is called before the first frame update
    void Start()
    {
        // Asegurarse de que footstep está desactivado al inicio
        if (footstep != null)
        {
            footstep.SetActive(false);
        }
        else
        {
            Debug.LogError("Footstep GameObject no está asignado en el inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            if (footstep != null && !footstep.activeInHierarchy)
            {
                Footsteps();
            }
        }
        else
        {
            if (footstep != null && footstep.activeInHierarchy)
            {
                StopFootsteps();
            }
        }
    }

    void Footsteps()
    {
        footstep.SetActive(true);  // Activar footstep
    }

    void StopFootsteps()
    {
        footstep.SetActive(false);  // Desactivar footstep
    }
}