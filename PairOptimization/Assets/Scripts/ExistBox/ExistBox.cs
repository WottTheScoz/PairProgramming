using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistBox : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        IExistBoxLogic existBoxLogic = collider.gameObject.GetComponent<IExistBoxLogic>();
        if(existBoxLogic != null)
        {
            existBoxLogic.OnExitExistBox();
        }
    }
}
