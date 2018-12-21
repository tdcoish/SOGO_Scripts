/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Properties")]
    public BulletProperties properties;
    public EnumVariable bulletType;
    public BulletTracker bulletTracker;

    [SerializeField]
    private StringVariable      Difficulty;
    
    [Space]
    [Header("Particles")]
    [SerializeField]
    private GameObject particlesWhenBouncedPerfectly;
    [SerializeField]
    private GameObject particlesWhenBouncedRandomly;
    [SerializeField]
	private GameObject particlesWhenDestroyed;
    [SerializeField]
	private GameObject particlesWhenDestroyedPerfect;

    // Components
    private Rigidbody rb;
    private Transform target;
    private Transform enemyBulletOwner = null;
    private float remainingLifespan = 0f;
    internal bool bouncedByPlayer = false;
    internal bool bouncedPerfectly = false;
    internal bool isHoming = false;
    private bool isTurnedOff = false;
    internal float damage = 0f;
    private float speed = 0f;
    private float turnSpeed = 0f;
    
    private void Awake() {
        rb = GetComponent<Rigidbody>();

        damage = properties.defaultDamage;
        speed = properties.defaultSpeed;
        if(Difficulty.Value == "EASY"){
            damage *= properties.easyDamageMult;
            speed *= properties.easySpeedMult;
        }
        if(Difficulty.Value == "NORMAL"){
            damage *= properties.normalDamageMult;
            speed *= properties.normalSpeedMult;
        }

        SetVelocity();
        bulletTracker.bullets.Add(this);
        isHoming = properties.startHomingInmediatly;
        AUD_Manager.PostEvent(properties.ad_loop, gameObject);
    }

    public void SetOwner(Transform enemyShootingTheBullet) {
        enemyBulletOwner = enemyShootingTheBullet;
    }
    
    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }

    private void Update() {
        CheckBulletLifespan();
    }

    private void FixedUpdate() {
        if (isTurnedOff) { RotateWithDownwardsVelocity(); return; };

        if (bouncedByPlayer) isHoming = true;

        if (isHoming && target != null) {
            Vector3 relativePos = target.position - transform.position;
            isHoming = Vector3.Dot(relativePos.normalized, transform.forward) > properties.stopHomingLimit;
            if (isHoming == false) bulletTracker.bullets.Remove(this);
            Quaternion destRotation = Quaternion.LookRotation(relativePos);
            float rotationSpeed = bouncedByPlayer ? turnSpeed : properties.enemyShotHomingTurnSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, destRotation, rotationSpeed * Time.fixedDeltaTime);
            SetVelocity();
        }
    }

    private void CheckBulletLifespan() {
        remainingLifespan += Time.deltaTime;
        if (remainingLifespan >= properties.lifeSpan) {
            DestroyBullet();
        }
    }

    private void SetVelocity() {
        rb.velocity = transform.forward * speed;
    }

    private void SetStartingDeflectionState() {
        bulletTracker.bullets.Remove(this);
        bouncedByPlayer = true;
        remainingLifespan = 0f;
        transform.Rotate(0f, 180f, 0f, Space.Self);
    }

    private void SetRandomDirection() {
        isHoming = false;
        speed = Random.Range(properties.defaultSpeed, properties.maxRandomSpeed);
        Quaternion randomRotation = Quaternion.Euler(-Random.Range(0f, properties.maxXAngleRandomLimit), Random.Range(-properties.maxYAngleRandomLimit, properties.maxYAngleRandomLimit), 0f);
        transform.rotation *= randomRotation;
        TurnOff();
        SetVelocity();
    }

    private void TurnOff() {
        isTurnedOff = true;
        rb.useGravity = true;
        SpawnSmokeParticles();
    }

    private void RotateWithDownwardsVelocity() {
        Vector3 nextVelocity = rb.velocity + Physics.gravity * Time.deltaTime;
        Vector3 nextPosition = transform.position + nextVelocity * Time.deltaTime;

        Vector3 nextPositionDirection = nextPosition - transform.position;
        
        Quaternion destRotation = Quaternion.LookRotation(nextPositionDirection);
        transform.rotation = destRotation;
    }

    private void SetPerfectDirection(float multiplier) {
        // First rotate completely towards the enemy who shot
        Vector3 dir = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation(dir.normalized);
        // Change damage and speed
        damage = properties.maxPerfectDamage;
        speed = multiplier.Denormalize(properties.maxRandomSpeed, properties.maxPerfectSpeed);
        // Change rotation to be random
        float xAngle = properties.minXAnglePerfectLimit + multiplier.Denormalize(properties.minXAnglePerfectLimit, properties.maxXAnglePerfectLimit, true);
        float yAngle = properties.minYAnglePerfectLimit + multiplier.Denormalize(properties.minYAnglePerfectLimit, properties.maxYAnglePerfectLimit, true);
        float randomYDir = Random.Range(-1f, 1f) >= 0f ? 1f : -1f;
        Quaternion randomRotation = Quaternion.Euler(-xAngle, yAngle * randomYDir, 0f);
        transform.rotation *= randomRotation;
        // Turn Speed
        turnSpeed = multiplier.Denormalize(properties.minHomingTurnSpeed, properties.maxHomingTurnSpeed);
        SetVelocity();
    }

    public void DeflectRandomly() {
        SetStartingDeflectionState();
        SetRandomDirection();
        Instantiate(particlesWhenBouncedRandomly, transform.position, transform.rotation);
        AUD_Manager.SetSwitch("Parry", "Almost", gameObject);
        AUD_Manager.PostEvent("PC_Perry_ST", gameObject);
    }

    public void DeflectPerfeclty(float multiplier) {
        // Starts as random
        SetStartingDeflectionState();
        
        // If the enemy was killed in the process...we try to get another enemy, if it is the last one we make it look like random
        SetTarget(enemyBulletOwner);
        if (target == null) {
            AI_Master AI = FindObjectOfType<AI_Master>();
            if (AI != null) {
                Transform newEnemy = AI.GetClosestEnemy(transform.position);
                if (newEnemy != null) {
                    SetTarget(newEnemy);
                }
            }
        };

        if (target == null) {
            SetRandomDirection();
            Instantiate(particlesWhenBouncedRandomly, transform.position, transform.rotation);
            return;
        }

        SetPerfectDirection(multiplier);
        ChangeColor();
        Instantiate(particlesWhenBouncedPerfectly, transform.position, transform.rotation);
        AUD_Manager.SetSwitch("Parry", "Success", gameObject);
		AUD_Manager.PostEvent("PC_Perry_ST", gameObject);
        bouncedPerfectly = true;
    }

    public void DestroyBullet() {
        BulletSpecificDestroy();
        if (bouncedPerfectly) {
            Instantiate(particlesWhenDestroyedPerfect, transform.position, Quaternion.identity);
        } else {
            Instantiate(particlesWhenDestroyed, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (bouncedByPlayer == false) {
            bulletTracker.bullets.Remove(this);
        }
        AUD_Manager.PostEvent(properties.ad_stop, gameObject);
    }

    protected virtual void SpawnSmokeParticles() {}
    protected virtual void ChangeColor() {}
    protected virtual void BulletSpecificDestroy() {}
}