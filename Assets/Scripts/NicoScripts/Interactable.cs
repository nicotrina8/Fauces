using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour //clase abstract gestionara los distin tos tipos de interacción
{
    public string promyMEssage;
    public void baseInteract(){
        
        Interact();
    }
    protected virtual void Interact(){
        //funcion para reescribir por subclases
    }
}
