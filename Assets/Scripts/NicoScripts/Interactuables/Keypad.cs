using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private AudioClip doorOpenSound; // Agregamos una variable para el clip de audio
    private AudioSource audioSource; // Variable para el componente AudioSource
    private bool doorOpen;

    // Start is called before the first frame update
    void Start()
    {
        // Obtenemos o añadimos el componente AudioSource al GameObject del Keypad
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = doorOpenSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact() // En esta función entra el código del interactuable
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);

        // Reproducimos el sonido al abrir la puerta
        if (doorOpen && audioSource != null && doorOpenSound != null)
        {
            audioSource.Play();
        }
    }
}
