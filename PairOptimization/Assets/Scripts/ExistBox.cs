using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistBox : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        collider.gameObject.transform.position *= -1;
    }
}
