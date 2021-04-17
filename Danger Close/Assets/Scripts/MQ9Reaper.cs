using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MQ9Reaper : MonoBehaviour
{
    // Fields
    [SerializeField] float ias = 87f;

    // References
    [SerializeField] GameObject propeller;

    void Start()
    {
        
    }

    void Update()
    {
        // transform.position += transform.forward * Time.deltaTime * ias;
        propeller.transform.RotateAroundLocal(Vector3.forward, 25f * Time.deltaTime);
    }
}