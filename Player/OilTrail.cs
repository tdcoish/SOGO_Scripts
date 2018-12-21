/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public class OilTrail : MonoBehaviour
{
    [SerializeField]
    private GameObject oilImgPrefab;
    [SerializeField]    
    private ParticleSystem[] oilParticles;
    
    public void DropSweat() {
        if (oilImgPrefab != null) {
            Instantiate(oilImgPrefab, transform.position, transform.rotation);
        }
    }

    public void StartDropParticles() {
        foreach (ParticleSystem ps in oilParticles)
        {
            ps.Play();
        }
    }
    
    public void StopDropParticles() {
        foreach (ParticleSystem ps in oilParticles)
        {
            ps.Stop();
        }
    }
}