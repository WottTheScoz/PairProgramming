using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : PooledObject
{
    #region Asteroid Behavior
    public override void ShootSelf(Transform player)
    {
        base.ShootSelf(player);

        // Shooting logic
        Vector3 direction = player.position - transform.position;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }
    #endregion
}
