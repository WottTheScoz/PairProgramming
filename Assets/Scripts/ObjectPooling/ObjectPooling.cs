using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : OnOffEnum
{
    [System.NonSerialized]
    public GameObject[] objectPool;

    public void SetUpPool(Transform parentObject)
    {
        // Sets the size of the object pool array
        int objectCount = parentObject.childCount;
        objectPool = new GameObject[objectCount];

        // Fills the object pool array with all bullets
        for(int i = 0; i < objectCount; ++i)
        {
            objectPool[i] = parentObject.GetChild(i).gameObject;
        }
    }
}
