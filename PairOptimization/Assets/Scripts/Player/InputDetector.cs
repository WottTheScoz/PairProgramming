using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDetector : MonoBehaviour
{
    public PlayerInputActions playerControls;

    public Vector3 rotateDirection {get; private set;}
    InputAction move;
    InputAction rotate;
    InputAction fire;

    public delegate void InputDelegate();
    public event InputDelegate OnMove;
    public event InputDelegate OnFire;

    #region Unity Methods
    void Awake()
    {
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
        Move();
        Rotate();
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
}
