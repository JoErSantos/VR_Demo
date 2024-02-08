using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.WalkingActions walking;
    private PlayerInput.UIActions ui;

    private PlayerMotor motor;
    private PlayerLook  look;
    private PlayerUI menu;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        walking = playerInput.Walking;
        ui = playerInput.UI;
        playerInput.UI.Disable();

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        menu = GetComponent<PlayerUI>();
 
        walking.Jump.performed += ctx => motor.Jump();
        walking.Pause.performed += ctx => OnOpenMenu();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(walking.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(walking.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        walking.Enable();
    }

    private void OnDisable()
    {
        walking.Disable();
    }

    private void OnOpenMenu()
    {
        playerInput.UI.Enable();
        menu.ToogleIsMenuOpen();
        Cursor.lockState = CursorLockMode.None;
        playerInput.Walking.Disable();
    }

    public void OnCloseMenu()
    {
        playerInput.Walking.Enable();
        menu.ToogleIsMenuOpen();
        Cursor.lockState = CursorLockMode.Locked;
        playerInput.UI.Disable();
    }

    public void CloseGame(){
        Application.Quit();
    }

    public PlayerInput.WalkingActions getWalking(){
        return walking;
    }
}
