using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    // Fields
    [SerializeField] float lookSensitivity = 10f;
    float minimumYTilt = -17f;
    float maximumYTilt = 71f;
    [SerializeField] float zoomSpeed = 5f;
    int currentZoomLevel = 1;
    [SerializeField] float maximumFocalLength = 2000f;
    [SerializeField] float minimumFocalLength = 25f;
    [SerializeField] float currentYTilt;
    [SerializeField] float currentXTilt;
    [SerializeField] int ammoRemaining;
    bool laserIsOn = false;

    // References
    [SerializeField] GameObject cameraArray;
    [SerializeField] TextMeshProUGUI selectedWeaponText;
    [SerializeField] TextMeshProUGUI rangeText;
    [SerializeField] TextMeshProUGUI laserStatusText;
    [SerializeField] TextMeshProUGUI cameraZoomText;
    [SerializeField] TextMeshProUGUI ammoRemainingText;
    [SerializeField] GameObject laserDot;
    [SerializeField] GameObject lockBox;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ammoRemainingText.text = ammoRemaining.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        AdjustZoom();
        ToggleLaser();

        RaycastHit hit;
        Ray laserRay = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(laserRay, out hit))
        {
            rangeText.text = "RANGE: " + Math.Round(hit.distance) + "m";
        }
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
        if (zoom > 0f && Camera.main.focalLength < maximumFocalLength)
        {
            Camera.main.focalLength *= 2;
            cameraZoomText.text = "CAM MAG: " + ++currentZoomLevel + "x";
        }
        else if (zoom < 0f && Camera.main.focalLength > minimumFocalLength)
        {
            Camera.main.focalLength /= 2;
            cameraZoomText.text = "CAM MAG: " + --currentZoomLevel + "x";
        }
    }

    void ToggleLaser()
    {
        if (Input.GetButtonUp("Toggle Laser"))
        {
            if (!laserIsOn)
            {
                laserIsOn = true;
                laserStatusText.gameObject.SetActive(true);
                laserDot.SetActive(true);
            }
            else
            {
                laserIsOn = false;
                laserStatusText.gameObject.SetActive(false);
                laserDot.SetActive(false);
            }
        }
    }
}