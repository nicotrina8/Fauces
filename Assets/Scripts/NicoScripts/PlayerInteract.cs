using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;

    [SerializeField]   
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }


    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward); //crear el rayo que sale de la camara para interactuar
        Debug.DrawRay(ray.origin,ray.direction*distance); //debuggear el rayo en modo play
        RaycastHit hitInfo;
        
        if(Physics.Raycast(ray, out hitInfo,distance,mask)){ //el if solo se ejecuta si hay hit
            if(hitInfo.collider.GetComponent<Interactable>() != null){// comprueba si el objeto es interactuable

                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promyMEssage);
                if (inputManager.OnFoot.Interact.triggered){

                    interactable.baseInteract();
                }
            }
        }
    }
}
