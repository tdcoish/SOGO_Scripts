using System.Collections;
using UnityEngine;

public class TanningOilExplosion : MonoBehaviour {

	[SerializeField]
    private GameObject explosionParticles;
	[SerializeField]
	private float damageToAreaForceField = 25f;
	[SerializeField]
	private float damageToEnemyForceField = 100f;
	[SerializeField]
	private GameEvent addScore;
	[SerializeField]
	private float pointsForStunning = 40f;
	[SerializeField]
	private float pointsForDestroyingShield = 150f;

	private void Awake() {
		Instantiate(explosionParticles, transform.position, Quaternion.identity);
		AUD_Manager.PostEvent("PC_OilSplash_ST_PL", gameObject);
		Destroy(gameObject, 0.2f);
	}

	private void OnTriggerEnter(Collider other) {
		Destroy(gameObject);

		IDamageable damageableObject = other.GetComponent<IDamageable>();
		if (damageableObject != null) {
			if (other.GetComponent<AreaForceField>() != null) {
				damageableObject.TakeDamage(damageToAreaForceField);
				addScore.Raise(pointsForStunning);
				return;
			}
			if (other.GetComponent<EnemyForceField>() != null) {
				damageableObject.TakeDamage(damageToEnemyForceField);
				addScore.Raise(pointsForDestroyingShield);
				return;
			}
		}
		
		AI_TakesGrenade npc = UT_FindComponent.FindComponent<AI_TakesGrenade>(other.gameObject);
		if (npc != null) {
			npc.GetHitByGrenade();
			addScore.Raise(pointsForStunning);
		}
    }

}
