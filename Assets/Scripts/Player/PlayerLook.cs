using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 100f;
    public float ySensitivity = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //Calculating camera rotation for looking around ( up and down )
        xRotation -= mouseY * Time.deltaTime * ySensitivity;
        xRotation = Math.Clamp(xRotation, -80f, 80f);

        //apply to camera transform

        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);

        //rotaring character
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
