using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffEnum : MonoBehaviour
{
    // Inherited by a few clases to act as a generic state machine;
    // namely, ObjectPooling and any classes that inherit it.

    public State ToggleState(bool turnOn)
    {
        if(turnOn)
        {
            return State.On;
        }

        return State.Off;
    }

    public enum State
    {
        Off,
        On
    }
}
