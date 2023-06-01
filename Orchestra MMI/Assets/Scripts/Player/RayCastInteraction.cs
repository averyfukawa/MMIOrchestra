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
    [Header("Interaction Events")]
    [Tooltip("Message displayed to player when looking at an interactable.")]
    public string promptMessage;

    [Tooltip("Radius filled of the circle")]
    [SerializeField] private float indicatorTimer = 0.0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [Tooltip("Loading bar image")]
    [SerializeField] private Image radialImage = null;

    [Tooltip("Events that can be placed, like animations")]
    [SerializeField] private UnityEvent myEvent = null;

    private bool shouldUpdate = false;

    [HideInInspector] public bool isLooking = false;

    [SerializeField] private KeyCode pressCommand = KeyCode.Mouse0;

    private bool hasreachedMax;

    private void Update()
    {
        if (Input.GetKey(pressCommand) && isLooking && !hasreachedMax)
        {
            shouldUpdate = false;
            radialImage.enabled = true;
            float clampedIndicator = Mathf.Clamp(indicatorTimer += Time.deltaTime, 0, 1);
            radialImage.fillAmount = clampedIndicator;

            if (indicatorTimer >= maxIndicatorTimer)
            {
                hasreachedMax = true;
                indicatorTimer = 0.0f;
                radialImage.fillAmount = maxIndicatorTimer;
                shouldUpdate = false;
                radialImage.enabled = false;
            }
        }
        else
        {
            if (shouldUpdate && indicatorTimer != maxIndicatorTimer && !hasreachedMax)
            {
                radialImage.enabled = true;
                float clampedIndicator = Mathf.Clamp(indicatorTimer -= Time.deltaTime, 0, 1);
                radialImage.fillAmount = clampedIndicator;

                if (indicatorTimer <= 0.0f)
                {
                    indicatorTimer = 0.0f;
                    radialImage.fillAmount = 0;
                    radialImage.enabled = false;
                    shouldUpdate = false;
                }
            }
        }
        
        if (Input.GetKeyUp(pressCommand))
        {
            shouldUpdate = true;
            hasreachedMax = false;

            myEvent.Invoke();
        }
        
    }

    private void IncreaseRadial()
    {
        if (Input.GetKey(pressCommand) && isLooking && !hasreachedMax)
        {
            shouldUpdate = false;
            radialImage.enabled = true;
            float clampedIndicator = Mathf.Clamp(indicatorTimer += Time.deltaTime, 0, 1);
            radialImage.fillAmount = clampedIndicator;

            if (indicatorTimer >= maxIndicatorTimer)
            {
                hasreachedMax = true;
                indicatorTimer = 0.0f;
                radialImage.fillAmount = maxIndicatorTimer;
                shouldUpdate = false;
                radialImage.enabled = false;
            }
        }
    }

    private void DecreaseRadial()
    {
        if (shouldUpdate && indicatorTimer != maxIndicatorTimer && !hasreachedMax)
        {
            radialImage.enabled = true;
            float clampedIndicator = Mathf.Clamp(indicatorTimer -= Time.deltaTime, 0, 1);
            radialImage.fillAmount = clampedIndicator;

            if (indicatorTimer <= 0.0f)
            {
                indicatorTimer = 0.0f;
                radialImage.fillAmount = 0;
                radialImage.enabled = false;
                shouldUpdate = false;
            }
        }
    }

    private void KeyReleased()
    {
        if (Input.GetKeyUp(pressCommand))
        {
            shouldUpdate = true;
            hasreachedMax = false;

            myEvent.Invoke();
        }
    }

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // No code in this function, it is a template to be overriden by subclasses
    }
}
