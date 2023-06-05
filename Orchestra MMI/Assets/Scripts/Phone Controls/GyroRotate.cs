using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotate : MonoBehaviour
{
    public float sensitivity = 50f;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate
            (
                -Input.gyro.rotationRateUnbiased.x * sensitivity * Time.deltaTime, 
                -Input.gyro.rotationRateUnbiased.y * sensitivity * Time.deltaTime, 
                0f
            );
    }
}
