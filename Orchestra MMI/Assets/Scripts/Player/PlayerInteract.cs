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
            //loadingBar[i].shouldUpdate = false;
        }

        // Create a new ray at the centre of the camera, shooting forwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo; // var to store collision information

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<RayCastInteraction>() != null)
            {
                hitInfo.collider.GetComponent<RayCastInteraction>().isLooking = true;
                UIText.UpdateText(hitInfo.collider.GetComponent<RayCastInteraction>().promptMessage);
            }
        }
    }
}
