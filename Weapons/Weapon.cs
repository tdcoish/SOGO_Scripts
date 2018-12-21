/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    public void Shoot(Vector3 direction)
    {
        PhotonNetwork.Instantiate(bullet.name, transform.position, Quaternion.LookRotation(direction, Vector3.up));
    }
}