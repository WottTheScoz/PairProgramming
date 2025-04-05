using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour, IExistBoxLogic
{
    public float speed = 3f;
    public State currentState {get; private set;}

    public Vector3 poolPosition {get; private set;}
    public Rigidbody2D rb {get; private set;}

    #region Unity Methods
    void Start()
    {
        currentState = State.InPool;
        poolPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Object Behavior
    public virtual void ShootSelf(Transform player)
    {
        // This object is now in a state of being shot
        currentState = State.BeingShot;
    }

    public void BackToPool()
    {
        // This object is now back in the bullet pool
        currentState = State.InPool;

        // Resets this object's velocity and position
        rb.velocity = Vector3.zero;
        transform.position = poolPosition;
    }

    // Allows other scripts to check if this object is pooled
    public bool IsInPool()
    {
        if(currentState == State.InPool)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Returns an object to the pool after hitting another collider.
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        BackToPool();
    }
    #endregion

    
    #region Exist Box Logic
    public void OnExitExistBox()
    {
        BackToPool();
    }
    #endregion

    #region Enums
    public enum State
    {
        InPool,
        BeingShot
    }
    #endregion
}
