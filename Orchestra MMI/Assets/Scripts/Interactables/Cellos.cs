using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Cellos : RayCastInteraction
{
    // This function is where I design the interaction using code
    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
