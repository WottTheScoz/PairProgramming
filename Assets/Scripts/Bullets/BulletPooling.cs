using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : ObjectPooling
{
    [Tooltip("Bullets are shot from the player's current position.")]
    public Transform player;
    [Tooltip("The parent object of all the player's bullets")]
    public Transform bullets;

    // Start is called before the first frame update
    void Start()
    {
        // Fills the object pool array with all bullet objects
        SetUpPool(bullets);
    }

    // Called by PlayerController, which inherits PlayerShooting
    public void ShootBullet()
    {
        // Loop naturally assumes all objects in pool have a Bullet component
        foreach(GameObject bulletObj in objectPool)
        {
            Bullet bullet = bulletObj.GetComponent<Bullet>();

            if(bullet.IsInPool())
            {
                bullet.ShootSelf(player);
                break;
            }
        }
    }

    // Returns the entire objectPool array. Called by ScoreManager.
    public GameObject[] GetBulletPool()
    {
        return objectPool;
    }
}
