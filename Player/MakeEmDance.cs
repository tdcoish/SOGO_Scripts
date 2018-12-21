/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System.Collections.Generic;
using UnityEngine;

public class MakeEmDance : MonoBehaviour
{
    [SerializeField]
    private int minBulletsNeeded = 5;
    [SerializeField]
    private float fireRate = 0.5f;

    private float lastShotTimestamp = 0f;
    private Queue<Bullet> bulletsToFire;
    public bool isActive { get; private set; } 

    private void Awake() {
        isActive = false;
        bulletsToFire = new Queue<Bullet>();
    }

    private void Update() {
        EvaluateMakeEmDance();
    }

    private void EvaluateMakeEmDance() {
        if (isActive) {
            if (bulletsToFire.Count > 0) {
                if (Time.time >= lastShotTimestamp + fireRate) {
                    Fire();
                }
            } else {
                ResetOriginalState();
            }    
        }
    }

    private void ResetOriginalState() {
        isActive = false;
        lastShotTimestamp = 0f;
        bulletsToFire.Clear();
    }

    public void Activate(Queue<Bullet> bullets) {
        if(bullets.Count < minBulletsNeeded) return;
        isActive = true;
        bulletsToFire = bullets;
        Fire();
    }

    private void Fire() {
        Bullet b = bulletsToFire.Dequeue();
        b.gameObject.SetActive(true);
        // b.ResetLife();
        // b.SetPosition(transform.position);
        // b.SetDirection(transform.forward);
        lastShotTimestamp = Time.time;
    }

}