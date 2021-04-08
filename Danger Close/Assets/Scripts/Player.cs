using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Fields
    [SerializeField] float lookSensitivity = 10f;
    float minimumYTilt = -17f;
    float maximumYTilt = 71f;
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float maximumFieldOfView = 70f;
    [SerializeField] float minimumFieldOfView = 0f;
    [SerializeField] float currentYTilt;
    [SerializeField] float currentXTilt;
    bool laserIsOn = false;

    // References
    [SerializeField] GameObject cameraArray;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        AdjustZoom();
        ToggleLaser();
    }

    void LookAround()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        currentYTilt -= y * lookSensitivity;
        currentXTilt += x * lookSensitivity;
        currentYTilt = Mathf.Clamp(currentYTilt, minimumYTilt, maximumYTilt);
        //currentXTilt = Mathf.Clamp(currentXTilt, -90, 90);
        cameraArray.transform.localEulerAngles = new Vector3(currentYTilt, currentXTilt, 0);
    }

    void AdjustZoom()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (zoom > 0f && Camera.main.fieldOfView > minimumFieldOfView)
        {
            Camera.main.fieldOfView -= zoom;
        }
        else if (zoom < 0f && Camera.main.fieldOfView < maximumFieldOfView)
        {
            Camera.main.fieldOfView -= zoom;
        }
    }

    void ToggleLaser()
    {
        if (Input.GetButtonUp("Toggle Laser"))
        {
            if (!laserIsOn)
            {
                laserIsOn = true;
                Debug.Log("The laser designator has been turned on");
            }
            else
            {
                laserIsOn = false;
                Debug.Log("The laser designator has been turned off");
            }
        }
    }
}