using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class InputDetector : OnOffEnum
{
    public PlayerInputActions playerControls;

    public Vector3 rotateDirection { get; private set; }
    public float moveDirection { get; private set; }
    InputAction movement;
    InputAction fire;

    public delegate void InputDelegate();
    public event InputDelegate OnMove;
    public event InputDelegate OnFire;
    public event InputDelegate OnRotate;

    State detectorState;

    #region Unity Methods
    void Awake()
    {
        Application.targetFrameRate = 60;
        detectorState = State.Off;

        playerControls = new PlayerInputActions();
    }

    void OnEnable()
    {
        movement = playerControls.Player.Movement;
        movement.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
        Debug.Log("Enabled");
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    void FixedUpdate()
    {
        if(detectorState == State.On)
        {
            Vector2 inputs = movement.ReadValue<Vector2>();
            Debug.Log(inputs);
            if (inputs.y != 0)
            {
                moveDirection = inputs.y;
                OnMove?.Invoke();
            }

            if (inputs.x != 0)
            {
                rotateDirection = Vector3.forward * -inputs.x;
                OnRotate?.Invoke();
            }
        }
    }
    #endregion

    #region Callbacks

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
