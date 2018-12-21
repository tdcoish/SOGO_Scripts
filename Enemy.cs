/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using Photon.Pun;
using UnityEngine;


public class Enemy : MonoBehaviourPun
{
    public PlayerController[] players;
    private Weapon weapon;

    [SerializeField]
    private float timeBetweenShots = 2f;

    void Awake ()
    {
        weapon = GetComponentInChildren<Weapon>();
        if (PhotonNetwork.IsMasterClient) {
            players = FindObjectsOfType<PlayerController>();
            InvokeRepeating("Shoot", timeBetweenShots, timeBetweenShots);
        }
    }

    private void Shoot()
    {
        if (players.Length > 0) {
            int randomIndex = -1;
            do {
                randomIndex = (int) Random.Range(0, players.Length - 1);
            } while (randomIndex == -1);
            Vector3 direction = players[randomIndex].gameObject.transform.position - transform.position;
            direction.Normalize();
            weapon.Shoot(direction);    
        }
    }

}