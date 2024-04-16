using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraFPS : MonoBehaviour
{
   public float sensX;
   public float sensY;

   public Transform orientation;

   float xRotation;
   float yRotation;



   private void Start()
   {
        Cursor.lockState = CursorLockMode.Locked; //el cursor esta centrado en la pantalla con posicion fija
        Cursor.visible = false; //no se ve
    }

    private void Update()
    {
        //input del raton
        float mouseX = Input.GetAxisRaw("Mouse X")*Time.deltaTime*sensX;
        float mouseY = Input.GetAxisRaw("MouseY")*Time.deltaTime*sensY;

        yRotation += mouseX;
        xRotation -= mouseY; //Rotaciones

        xRotation =Mathf.Clamp(xRotation, -90f , 90f); //para rotar más de 90


        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}


