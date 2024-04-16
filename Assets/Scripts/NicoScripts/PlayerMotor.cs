using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    public bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 Input){  //Esta función recibe los inputs del input manager y los aplica al character controller 

        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = Input.x;
        moveDirection.z = Input.y;
        controller.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)              //solo se incrementa gravedad si no esta grounded = false
            playerVelocity.y = -2f;
        controller.Move(playerVelocity*Time.deltaTime);

        Debug.Log(playerVelocity.y);
    }

    public void Jump(){

        if(isGrounded){

            playerVelocity.y = Mathf.Sqrt(jumpHeight*-3.0f*gravity);
        }

    }


}
