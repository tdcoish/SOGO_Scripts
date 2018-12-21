/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System.Collections.Generic;
using UnityEngine;

public class PumpingIron : MonoBehaviour
{
    [SerializeField, Range(3, 10)]
    private int minBulletsNeeded = 5;
    [SerializeField]
    private float spreadAngle = 60.0f;
    [SerializeField]
    private float cooldownTime = 2f;
    [SerializeField]
    private float reach = 1f;

    public bool isActive { get; private set; }
    private float lastShotTimestamp = 0f;

    private void Update() {
        EvaluatePumpingIron();
    }

    private void EvaluatePumpingIron() {
        if(isActive) {
            if (Time.time >= lastShotTimestamp + cooldownTime) {
                ResetOriginalState();
            }
        }
    }

    public void Activate(Queue<Bullet> bullets) {
        if (bullets.Count < minBulletsNeeded) return;

        isActive = true;
        lastShotTimestamp = Time.time;
        Shoot(bullets);
    }

    private void ResetOriginalState() {
        isActive = false;
        lastShotTimestamp = 0f;
    }

    private void Shoot(Queue<Bullet> bullets) {
        
        float midAngle = spreadAngle / 2;
        
        Vector3 leftVector = Quaternion.Euler(0f, -midAngle, 0f) * transform.forward;
        FireBullet(bullets.Dequeue(), leftVector);

        Vector3 rightVector = Quaternion.Euler(0f, midAngle, 0f) * transform.forward;
        FireBullet(bullets.Dequeue(), rightVector);

        float angleStep = spreadAngle / (bullets.Count + 1);
        float angleAcum = angleStep;
        while (bullets.Count > 0) {
            Vector3 nextDirection = Quaternion.Euler(0f, angleAcum, 0f) * leftVector;
            FireBullet(bullets.Dequeue(), nextDirection);
            angleAcum += angleStep;
        }

    }

    private void FireBullet(Bullet bullet, Vector3 direction) {
        bullet.gameObject.SetActive(true);
        // bullet.ResetLife();
        // bullet.SetPosition(transform.position);
        // bullet.SetDirection(direction.normalized);
    }

    
}