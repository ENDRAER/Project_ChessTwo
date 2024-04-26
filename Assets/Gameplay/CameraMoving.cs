using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [NonSerialized] private float sensitivity = 100f;
    [NonSerialized] private float cameraMaxAngle = 80f;
    [NonSerialized] private float cameraMinAngle = 30f;
    // border
    [NonSerialized] private float borderSize = 5f; // border size reduces camera viewing angle
    [NonSerialized] private float backwardForceSensitivity = 60f;
    // impulse
    [NonSerialized] private float impulseDeadZone = 0.1f;
    [NonSerialized] private float impulseSmoothnes = 80f; // increase value to reduce resistance

    [NonSerialized] private Vector2 lastCursourVelocity;


    private void FixedUpdate()
    {
        Vector2 mouseAxis = new Vector2 (0, 0);
        float backwardForce = 0;
        float borderDistance = transform.transform.eulerAngles.x < 50 ?
                math.clamp(1 - (transform.transform.eulerAngles.x - cameraMinAngle) / borderSize, 0, 1) :
                math.clamp(-(transform.transform.eulerAngles.x + borderSize * 2 - cameraMaxAngle) / (borderSize * 2), -1, 0);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            lastCursourVelocity = mouseAxis;
        }
        else //if(lastCursourVelocity.magnitude > impulseDeadZone)
        {
            lastCursourVelocity *= math.clamp(impulseSmoothnes * Time.deltaTime, 0, 0.9f);
            mouseAxis = lastCursourVelocity;
            backwardForce = backwardForceSensitivity * borderDistance;
        }

        transform.transform.eulerAngles += new Vector3((-mouseAxis.y * (1 - math.abs(borderDistance)) * sensitivity + backwardForce) * Time.deltaTime, mouseAxis.x * sensitivity * 1.5f * Time.deltaTime, 0);
        if (transform.transform.eulerAngles.x < cameraMinAngle || transform.transform.eulerAngles.x > cameraMaxAngle)
            transform.transform.eulerAngles = new Vector3(math.clamp(transform.transform.eulerAngles.x, cameraMinAngle + 0.2f, cameraMaxAngle - 0.2f), transform.transform.eulerAngles.y, 0);
    }
}
