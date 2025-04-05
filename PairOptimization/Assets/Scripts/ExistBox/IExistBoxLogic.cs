using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExistBoxLogic
{
    // ExistBox calls this method on any object that exits it. Used by PlayerCollision, Bullet, and Asteroid.
    public void OnExitExistBox();
}
