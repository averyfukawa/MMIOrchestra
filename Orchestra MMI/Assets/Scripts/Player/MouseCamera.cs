using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    [Tooltip("The rotation acceleration, in degrees per second")]
    [SerializeField] private Vector2 acceleration;

    [Tooltip("Multiplier to the input. Describes the maximum speed in degrees per second. To invert rotation, set Y to negative.")]
    [SerializeField] private Vector2 sensitivity;

    [Tooltip("The period to wait until resetting the input value. Set this as low as possible, without encountering stuttering")]
    [SerializeField] private float inputLagPeriod;

    [Tooltip("The maximum angle from the horizon the player can rotate, in Degrees")]
    [SerializeField] private float maxVerticalAngle;

    private Vector2 rotation; // The current rotation in degrees
    private Vector2 velocity; // The current rotation velocity, in degrees

    private Vector2 lastInputEvent; // The last received non-zero input value
    private float inputLagTimer; // The time since the last received non-zero input value

    private float ClampVerticalAngle (float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngle, maxVerticalAngle);
    }

    private Vector2 GetInput()
    {
        // Add to the lag timer
        inputLagTimer += Time.deltaTime;

        // Get the input vector.
        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Sometimes at fast framerates unity will not receive input events every frame, which results in zero values being given above.
        // This can cause stuttering and make it difficult to finetune the acceleration setting.
        // To fix this, disregard zero values.
        // If the lag timer has passed the lag period, we can assume that the user is not giving any input, so we actually want to set the input value to zero at that time.
        // Thus, save the input value if it is non-zero or the lag timer is met.

        if((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer >= inputLagPeriod)
        {
            lastInputEvent = input;
            inputLagTimer = 0;
        }

        return lastInputEvent;
    }

    private void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // The wanted velocity is the current input scaled by the sensitivity
        // This is also the maximum velocity
        Vector2 wantedVelocity = GetInput() * sensitivity;

        // Calculate new rotation
        velocity = new Vector2(Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.deltaTime),
                               Mathf.MoveTowards(velocity.y, wantedVelocity.y, acceleration.y * Time.deltaTime));
        
        rotation += velocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);

        // Convert the rotation to euler angles
        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }
}
