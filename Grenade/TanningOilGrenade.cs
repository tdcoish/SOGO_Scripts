/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public class TanningOilGrenade : MonoBehaviour
{   

    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private Vector3 throwingRotation;    
    
    private Rigidbody rb;
    internal float throwingForce = 0f;
    private Collider coll;
    private bool hasBeenThrown = false;
    private ParticleSystem[] particles;
        
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        coll = GetComponent<Collider>();
        coll.enabled = false;
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (transform.parent != null) {
            transform.localPosition = new Vector3(0, 0.045f, 0.1f);
        }
    }

    private void FixedUpdate()
    {
        if (hasBeenThrown) {
            Quaternion deltaRotation = Quaternion.Euler(throwingRotation * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);    
        }
    }

    private void Explode() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Throw(float throwForce, Vector3 direction) {
        transform.SetParent(null);
        transform.forward = direction;
        throwingForce = throwForce;
        rb.useGravity = true;
        coll.enabled = true;
        rb.velocity = transform.forward * throwingForce;
        hasBeenThrown = true;
        foreach (ParticleSystem ps in particles) {
            ps.Play();
        }
    }
    
    // Needs to check that it hit something that can destroy it, which is basically everthing.
    private void OnTriggerEnter(Collider other) {

        EnemyForceField forceField = other.GetComponent<EnemyForceField>();
        if (forceField != null) {
            forceField.TakeDamage(forceField.health);
            Explode();
        }

        DestroyObjectOnCollision dBul = UT_FindComponent.FindComponent<DestroyObjectOnCollision>(other.gameObject);
        AI_Controller npc = UT_FindComponent.FindComponent<AI_Controller>(other.gameObject);
        if (npc != null || dBul != null) {
            Explode();
        }
    }
}