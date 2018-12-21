using System.Collections;
using UnityEngine;

public class EnemyForceField : MonoBehaviour, IDamageable {
	
	[SerializeField]
	internal float initialHealth = 300f;
	internal float health;
	private MeshRenderer meshRenderer;
	private ParticleSystem particles;

	private void Awake() {
		health = initialHealth;
		meshRenderer = GetComponent<MeshRenderer>();
		particles = GetComponentInChildren<ParticleSystem>();
	}

	public void TakeDamage(float damage)
    {
        health -= damage;
		if (health <= 0) {
			StartCoroutine(DestroySelf());
			AUD_Manager.PostEvent("EN_LankyShieldBreak_ST", gameObject);
		} else {
			if (particles != null) {
				Destroy(particles.gameObject);
			}
			AUD_Manager.PostEvent("EN_LankyShieldHit_ST", gameObject);
		}
    }

	private IEnumerator DestroySelf() {
		yield return null;
		Destroy(gameObject); 
		yield break;
	}
}
