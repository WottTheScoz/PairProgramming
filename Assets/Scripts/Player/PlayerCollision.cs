using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour, IExistBoxLogic
{
    public StartEndGame startEndGame;

    readonly Vector3 startPosition = Vector3.zero;
    readonly Quaternion startRotation = Quaternion.identity;
    readonly Vector3 deathPosition = new Vector3(20, 20, 0);

    Rigidbody2D rb;

    #region Unity Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Death();
    }
    #endregion

    #region Player Death Logic
    void Death()
    {
        // Moves the player off-screen
        transform.position = deathPosition;

        // Stops all forms of collision and movement
        rb.Sleep();

        // Induces the game over state
        startEndGame.EndGame();
    }

    // Called by StartEndGame
    public void Rebirth()
    {
        // Returns the player to the screen
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Reintroduces all collision and movement
        rb.WakeUp();
    }
    #endregion

    #region Exist Box Logic
    public void OnExitExistBox()
    {
        transform.position *= -1;
    }
    #endregion
}
