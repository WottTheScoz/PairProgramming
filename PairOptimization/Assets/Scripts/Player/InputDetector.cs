using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDetector : OnOffEnum
{
    public PlayerInputActions playerControls;

    public Vector3 rotateDirection {get; private set;}
    InputAction move;
    InputAction rotate;
    InputAction fire;

    public delegate void InputDelegate();
    public event InputDelegate OnMove;
    public event InputDelegate OnFire;

    State detectorState;

    #region Unity Methods
    void Awake()
    {
        detectorState = State.Off;

        playerControls = new PlayerInputActions();
    }

    void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        rotate = playerControls.Player.Rotate;
        rotate.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    void OnDisable()
    {
        move.Disable();
        rotate.Disable();
        fire.Disable();
    }

    void Update()
    {
        if(detectorState == State.On)
        {
            Move();
            Rotate();
        }
    }
    #endregion

    #region Callbacks
    void Move()
    {
        if(move.IsPressed())
        {
            OnMove?.Invoke();
        }
    }

    void Rotate()
    {
        rotateDirection = Vector3.forward * rotate.ReadValue<Vector2>().x;
    }

    void Fire(InputAction.CallbackContext context)
    {
        OnFire?.Invoke();
    }
    #endregion

    #region State Modifiers
    // Turns on/off input detection. detectorState does not toggle Fire detection.
    public void ToggleGameplayInput(bool turnOn)
    {
        detectorState = ToggleState(turnOn);
    }
    #endregion
}
