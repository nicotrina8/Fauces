using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    

    public void ProcessLook(Vector2 input){

        float mouseX = input.x;
        float mouseY = input.y;
        //calcular la rotacion  de la camara al mirar arriba y abajo
        xRotation -= (mouseY *Time.deltaTime)*ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80f); //restringe la rotacion en el x
        //aplicarselo a la camara
        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        //rotar al player
        transform.Rotate(Vector3.up*(mouseX*Time.deltaTime)*xSensitivity);
    }


    
}
