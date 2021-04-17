using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedObject : MonoBehaviour
{
    // References
    [SerializeField] GameObject destroyedMesh;

    public void DestroyUnit()
    {
        Instantiate(destroyedMesh, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}