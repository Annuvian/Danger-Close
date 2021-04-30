using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGM_114_Hellfire : MonoBehaviour
{
    // Fields
    private float thrust = 4412.9925f;
    private float burnTime = 3f;
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
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem smokeTrail;
    private Player player;

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
            fire.Play();
            smokeTrail.Play();
        }
    }

    void Track()
    {
        target = player.laser.point;
        if (hasLaunched)
        {
            burnTime -= Time.deltaTime;
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, maxSpeedMps * Time.deltaTime);
            Arm();
            StopThrust();
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
            // Damage things nearby
            // Throw fragments etc.
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            if (GameObject.Find("HVT") != null)
            {
                if (Vector3.Distance(transform.position, GameObject.Find("HVT").transform.position) <= 20f)
                {
                    GameObject.Find("HVT").GetComponent<DestroyedObject>().DestroyUnit();
                }
            }
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, 20f);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(50000f, explosionPos, 40f, 0f, ForceMode.Impulse);
                }
            }
            smokeTrail.GetComponent<Transform>().parent = null;
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
        if (burnTime <= 0f)
        {
            motorBurning = false;
            smokeTrail.Stop();
            fire.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Detonate();
    }
}