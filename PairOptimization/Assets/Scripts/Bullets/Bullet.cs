using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    public delegate void BulletDelegate();
    public event BulletDelegate OnHitAsteroid;

    #region Bullet Behavior
    public override void ShootSelf(Transform player)
    {
        base.ShootSelf(player);

        // Positioning and shooting logic
        transform.position = player.position;
        rb.AddForce(player.up * speed, ForceMode2D.Impulse);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        OnHitAsteroid?.Invoke();                // Now alerts subscribers of the hit. Used by ScoreManager.
    }
    #endregion

}
