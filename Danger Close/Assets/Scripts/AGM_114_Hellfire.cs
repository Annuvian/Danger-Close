using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGM_114_Hellfire : MonoBehaviour
{
    // Fields
    private float thrust = 4412.9925f;
    private float maxSpeedMps = 424.688f;
    private float armingDistance = 300;
    private Vector3 launchPosition;
    private bool hasLaunched = false;
    private bool isArmed = false;
    public bool hasLock = false;
    private bool motorBurning = false;

    // References
    [SerializeField] Vector3 target;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioSource explosionSound;
    private Player player;

    // [SerializeField] DestroyedObject temp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>();
    }

    void Update()
    {
        Track();
    }

    private void FixedUpdate()
    {

    }

    public void Launch()
    {
        if (hasLock && !hasLaunched)
        {
            transform.parent = null;
            rb.isKinematic = false;
            launchPosition = transform.position;
            hasLaunched = true;
            motorBurning = true;
        }
    }

    void Track()
    {
        target = player.laser.point;
        if (hasLaunched)
        {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, maxSpeedMps * Time.deltaTime);
            Arm();
            if (Vector3.Distance(transform.position, target) <= 0.1f)
            {
                Detonate();
            }
            if (rb.velocity.magnitude >= maxSpeedMps && motorBurning)
            {
                motorBurning = false;
            }
        }
    }

    void Detonate()
    {
        if (isArmed)
        {
            // Play explosion sound
            // Spawn explosion/smoke/fragment particles
            // Destroy missile
            // Damage things nearby
            // Throw fragments etc.
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            // temp.DestroyUnit();
            Destroy(gameObject);
        }
        else
        {
            // Play a thud sound as it hits the ground
            // Dust and stuff, but no explosion
            Destroy(gameObject);
        }
    }

    void Arm()
    {
        if (hasLaunched && !isArmed)
        {
            if (Vector3.Distance(launchPosition, transform.position) >= armingDistance)
            {
                isArmed = true;
            }
        }
    }

    public void StopThrust()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Detonate();
    }
}