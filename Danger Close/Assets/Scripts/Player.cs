using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Fields
    [SerializeField] float lookSensitivity = 10f;
    [SerializeField] float currentYTilt;
    [SerializeField] float currentXTilt;

    // References
    [SerializeField] GameObject cameraArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    void LookAround()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        currentYTilt -= y * lookSensitivity;
        currentXTilt += x * lookSensitivity;
        currentYTilt = Mathf.Clamp(currentYTilt, -17, 71);
        //currentXTilt = Mathf.Clamp(currentXTilt, -90, 90);
        cameraArray.transform.localEulerAngles = new Vector3(currentYTilt, currentXTilt, 0);
    }
}