using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTrailImage : MonoBehaviour {

	[SerializeField]
    private float lifespan = 5f;

	private SpriteRenderer sr;
    private float spawnTime = 0f;
	
	private void Awake() {
        sr = GetComponentInChildren<SpriteRenderer>();
        spawnTime = Time.time;
        Destroy(gameObject, lifespan);
    }

    private void Update() {
        Color nextColor = sr.color;
        nextColor.a = 1f - Time.time.Normalize(spawnTime, spawnTime + lifespan);
        sr.color = nextColor;
    }
}
