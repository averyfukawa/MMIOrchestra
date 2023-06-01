using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Security;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class RayCastInteraction : MonoBehaviour
{
    [Tooltip("Message displayed to player when looking at an interactable.")]
    public string promptMessage;

    public Image radialImage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // No code in this function, it is a template to be overriden by subclasses
    }
}
