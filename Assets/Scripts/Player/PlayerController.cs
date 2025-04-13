using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerShooting
{
    [Space(20)]
    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;

    Rigidbody2D rb;

    #region Unity Methods
    void Start()
    {
        // Rigidbody2D is used for movement calculations
        rb = GetComponent<Rigidbody2D>();

        // Gets notified when movement input is detected
        inputDetector.OnMove += MoveForward;
        inputDetector.OnRotate += Rotate;
    }

    /*void FixedUpdate()
    {
        Rotate();
    }*/
    #endregion

    #region Movement Logic
    void MoveForward()
    {
        rb.AddForce(transform.up * moveSpeed * inputDetector.moveDirection * Time.deltaTime);
    }

    void Rotate()
    {
        if(inputDetector.rotateDirection != Vector3.zero)
        {
            // -1 inverts the rotation direction; A is now counterclockwise and vice versa w/ D
            transform.Rotate(inputDetector.rotateDirection * rotateSpeed * Time.deltaTime);
        }
    }
    #endregion
}
