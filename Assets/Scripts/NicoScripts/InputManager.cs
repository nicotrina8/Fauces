using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    public PlayerInput.OnFootActions OnFoot { get => onFoot; set => onFoot = value; }

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        OnFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>(); 
        look = GetComponent<PlayerLook>();

        OnFoot.Jump.performed += ctx => motor.Jump();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Le decimos al playermotor que se mueva usando el valor de nuestra movement action
        motor.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate(){

        look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());

    }

    private void OnEnable(){
        OnFoot.Enable();
    }

    private void OnDisable(){
        OnFoot.Disable();
    }

}
