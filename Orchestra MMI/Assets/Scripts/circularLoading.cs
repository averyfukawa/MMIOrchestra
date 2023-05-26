using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class circularLoading : MonoBehaviour
{
    /*
    [SerializeField] private float indicatorTimer = 1.0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [SerializeField] private Image radialImage = null;

    [SerializeField] private UnityEvent myEvent = null;

    private bool shouldUpdate = false;

    [HideInInspector] public bool isLooking = false;

    private void Update()
    {
        if (isLooking)
        {
            shouldUpdate = false;
            indicatorTimer -= Time.deltaTime;
            radialImage.enabled = true;
            radialImage.fillAmount = indicatorTimer;

            if (indicatorTimer <= 0.0f)
            {
                indicatorTimer = maxIndicatorTimer;
                radialImage.fillAmount = maxIndicatorTimer;
                radialImage.enabled = false;
                
                myEvent.Invoke();
            }
        }
        else
        {
            if (shouldUpdate)
            {
                indicatorTimer += Time.deltaTime;
                radialImage.fillAmount = indicatorTimer;

                if (indicatorTimer >= maxIndicatorTimer)
                {
                    indicatorTimer = maxIndicatorTimer;
                    radialImage.fillAmount = maxIndicatorTimer;
                    radialImage.enabled = false;
                    shouldUpdate = false;
                }
            }
        }
        if (!isLooking)
        {
            shouldUpdate = true;
        }
    }
    */
}
