using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [Header("Camera to point the ray from")]
    [SerializeField] private Camera cam;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;

    [Header("Interaction Events")]
    private PlayerUI UIText;

    [Header("Radius filled of the circle")]
    [SerializeField] private float indicatorTimer = 0.0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [Header("Loading Bar Image")]
    //[SerializeField] private Cellos[] loadingBar;
    //[SerializeField] private Image radialImage = null;
    [SerializeField] private Image[] radialImage = null;

    [Header("Events that can be placed, like animations")]
    [SerializeField] private UnityEvent myEvent = null;

    //Different bools to make sure we can update the looked at instrument
    private bool shouldUpdate = false;
    private bool hasBeenClicked = false;
    private bool hasReachedMax = false;

    [Header("Key needed to be pressed to interact")]
    [SerializeField] private KeyCode pressCommand = KeyCode.Mouse0;

    [Header("Instrument Volume Interaction")]
    [SerializeField] private AudioRandomizer randomizer;

    [Header("Instrument Animations")]
    [SerializeField] private Animator[] animators;

    private void Start()
    {
        UIText = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        UIText.UpdateText(string.Empty);

        // Create a new ray at the centre of the camera, shooting forwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, mask))
        {
            string nameOfInstrument = hitInfo.collider.name.ToString();
            if (hitInfo.collider.GetComponent<RayCastInteraction>() != null)
            {
                if (Input.GetKey(pressCommand) && !hasReachedMax && hitInfo.transform.CompareTag("Interact"))
                {
                    hasBeenClicked = true;

                    // Experiment to not have a giant array?
                    if (nameOfInstrument.Contains("Clarinet") && hitInfo.transform.CompareTag("Interact"))
                    {
                        StartIncreaseVol(0);
                        IncreaseRadial(0);
                    }
                    if (nameOfInstrument.Contains("Piano") && hitInfo.transform.CompareTag("Interact"))
                    {
                        StartIncreaseVol(1);
                        IncreaseRadial(1);
                    }
                    if (nameOfInstrument.Contains("Violin") && hitInfo.transform.CompareTag("Interact"))
                    {
                        StartIncreaseVol(2);
                        IncreaseRadial(2);
                    }
                    if (nameOfInstrument.Contains("Saxophone") && hitInfo.transform.CompareTag("Interact"))
                    {
                        StartIncreaseVol(3);
                        IncreaseRadial(3);
                    }
                    if (nameOfInstrument.Contains("French Horn") && hitInfo.transform.CompareTag("Interact"))
                    {
                        StartIncreaseVol(4);
                        IncreaseRadial(4);
                    }
                    if (nameOfInstrument.Contains("Trumpet") && hitInfo.transform.CompareTag("Interact"))
                    {
                        StartIncreaseVol(5);
                        IncreaseRadial(5);
                    }
                    if (nameOfInstrument.Contains("Cello") && hitInfo.transform.CompareTag("Interact"))
                    {
                        StartIncreaseVol(6);
                        IncreaseRadial(6);
                    }
                }
                    
                if (hitInfo.transform.CompareTag("Interact"))
                {
                    if (Input.GetKeyUp(pressCommand) && hasBeenClicked)
                        KeyReleased();

                    if (shouldUpdate && indicatorTimer != maxIndicatorTimer && !hasReachedMax)
                    {
                        if (nameOfInstrument.Contains("Clarinet") && hitInfo.transform.CompareTag("Interact"))
                        {
                            StopAutoVolDecrease(0);
                            DecreaseRadial(0);
                        }
                        if (nameOfInstrument.Contains("Piano") && hitInfo.transform.CompareTag("Interact"))
                        {
                            StopAutoVolDecrease(1);
                            animators[1].SetBool("openLidBool", true);
                            DecreaseRadial(1);
                        }
                        if (nameOfInstrument.Contains("Violin") && hitInfo.transform.CompareTag("Interact"))
                        {
                            StopAutoVolDecrease(2);
                            DecreaseRadial(2);
                        }
                        if (nameOfInstrument.Contains("Saxophone") && hitInfo.transform.CompareTag("Interact"))
                        {
                            StopAutoVolDecrease(3);
                            DecreaseRadial(3);
                        }
                        if (nameOfInstrument.Contains("French Horn") && hitInfo.transform.CompareTag("Interact"))
                        {
                            StopAutoVolDecrease(4);
                            DecreaseRadial(4);
                        }
                        if (nameOfInstrument.Contains("Trumpet") && hitInfo.transform.CompareTag("Interact"))
                        {
                            StopAutoVolDecrease(5);
                            DecreaseRadial(5);
                        }
                        if (nameOfInstrument.Contains("Cello") && hitInfo.transform.CompareTag("Interact"))
                        {
                            StopAutoVolDecrease(6);
                            DecreaseRadial(6);
                        }
                    }
                }

                UIText.UpdateText(hitInfo.collider.GetComponent<RayCastInteraction>().promptMessage);
            }
        }
    }

    private void StopAutoVolDecrease(int lookedInstrument)
    {
        randomizer.fixingInstrumentIndex = lookedInstrument;
        StopCoroutine(randomizer.DecreaseVolCoroutine(lookedInstrument));
    }

    private void StartIncreaseVol(int lookedInstrument)
    {
        randomizer.fixingInstrumentIndex = lookedInstrument;
        StartCoroutine(randomizer.IncreaseVolCoroutine(lookedInstrument));
    }

    private void IncreaseRadial(int rad)
    {
        shouldUpdate = false;
        radialImage[rad].enabled = true;
        float clampedIndicator = Mathf.Clamp(indicatorTimer += Time.deltaTime, 0, 1);
        radialImage[rad].fillAmount = clampedIndicator;

        if (indicatorTimer >= maxIndicatorTimer)
        {
            myEvent.Invoke();
            hasReachedMax = true;
            indicatorTimer = 0.0f;
            radialImage[rad].fillAmount = maxIndicatorTimer;
            shouldUpdate = false;
            radialImage[rad].enabled = false;
        }
    }

    private void DecreaseRadial(int rad)
    {
        radialImage[rad].enabled = true;
        float clampedIndicator = Mathf.Clamp(indicatorTimer -= Time.deltaTime, 0, 1);
        radialImage[rad].fillAmount = clampedIndicator;
        
        if (indicatorTimer <= 0.0f)
        {
            indicatorTimer = 0.0f;
            radialImage[rad].fillAmount = 0;
            radialImage[rad].enabled = false;
            shouldUpdate = false;
        }
    }

    private void KeyReleased()
    {
        hasBeenClicked = false;
        shouldUpdate = true;
        hasReachedMax = false;
        randomizer.fixingInstrumentIndex = 101;
    }
}
