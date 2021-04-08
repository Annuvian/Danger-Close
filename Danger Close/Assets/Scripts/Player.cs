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
            Debug.Log(zoom);
        }
        else if (zoom < 0f && Camera.main.fieldOfView < maximumFieldOfView)
        {
            Camera.main.fieldOfView -= zoom;
            Debug.Log(zoom);
        }
    }
}