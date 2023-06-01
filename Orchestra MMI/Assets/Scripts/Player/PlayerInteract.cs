using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;

    [SerializeField] private Cellos[] loadingBar;
    private PlayerUI UIText;

    private void Start()
    {
        UIText = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        UIText.UpdateText(string.Empty);

        for (int i = 0; i < loadingBar.Length; i++)
        {
            loadingBar[i].isLooking = false;
        }

        // Create a new ray at the centre of the camera, shooting forwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        // var to store collision information
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<RayCastInteraction>() != null)
            {
                hitInfo.collider.GetComponent<RayCastInteraction>().isLooking = true;
                UIText.UpdateText(hitInfo.collider.GetComponent<RayCastInteraction>().promptMessage);
                string nameOfInstrument = hitInfo.collider.name.ToString();

                if (nameOfInstrument == "")
                    StopAutoVolDecrease(0);
                if (nameOfInstrument == "")
                    StopAutoVolDecrease(1);
                if (nameOfInstrument == "")
                    StopAutoVolDecrease(2);
                if (nameOfInstrument == "")
                    StopAutoVolDecrease(3);
                if (nameOfInstrument == "")
                    StopAutoVolDecrease(4);
                if (nameOfInstrument == "")
                    StopAutoVolDecrease(6);

            }
        }
    }

    [SerializeField] private AudioRandomizer randomizer;
    
    private void StopAutoVolDecrease(int lookedInstrument)
    {
        StopCoroutine(randomizer.DecreaseVolCoroutine(lookedInstrument));
    }
}
