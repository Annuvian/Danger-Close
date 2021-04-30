using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedObject : MonoBehaviour
{
    // References
    [SerializeField] GameObject destroyedMesh;

    private void Update()
    {
        transform.position += transform.forward * 13 * Time.deltaTime;
    }

    public void DestroyUnit()
    {
        Instantiate(destroyedMesh, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}