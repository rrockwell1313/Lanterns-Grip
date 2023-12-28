using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls; //loading the player control input manager

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement() //receive movement inputs from InputManager
    {
        return playerControls.Player.Movement.ReadValue<Vector2>()*-1;
    }
    public Vector2 GetMouseDelta() //receive MouseDelta from InputManager
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerPressedInteractThisFrame()
    {
        return playerControls.Player.Interact.triggered;
    }
    public bool PlayerPressedJumpThisFrame()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerPressedFocusThisFrame()
    {
        return playerControls.Player.Focus.triggered;
    }
    public bool PlayerPressedRaiseLowerLanternThisFrame()
    {
        return playerControls.Player.RaiseLowerLantern.triggered;
    }

}
