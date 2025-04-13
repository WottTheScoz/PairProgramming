using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Tooltip("Used to check when a Fire input is received.")]
    public InputDetector inputDetector;
    [Tooltip("Locates the pool of bullets to use when shooting.")]
    public BulletPooling bulletPooling;

    // Allows other scripts to turn on/off the player's ability to shoot
    public void CanShoot(bool canShoot)
    {
        if(canShoot)
        {
            inputDetector.OnFire += Shoot;
        }
        else
        {
            inputDetector.OnFire -= Shoot;
        }
    }

    // Shoots a bullet by calling bulletPooling's method
    void Shoot()
    {
        bulletPooling.ShootBullet();
    }
}
