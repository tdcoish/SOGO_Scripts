using UnityEngine;

public class MeleeAttack : MonoBehaviour {

	[SerializeField]
	private GameEvent onEnemyHitCamShake;
	[SerializeField]
	private GameEvent onEnemyHitRumble;
	[SerializeField]
	private float timeToRumbleWhenHit = 0.20f;
	[SerializeField]
	private GameEvent addScore;
	[SerializeField]
	private float pointsForMeleeHit = 100f;

	private void OnTriggerEnter(Collider other)
	{
		AI_TakesMelee meleeReceiver = other.GetComponentInParent<AI_TakesMelee>();
		// AI_Controller enemyController = other.GetComponentInParent<AI_Controller>();
		if (meleeReceiver != null) {
				float[] shakeOptions = { 
				0.25f,	// Time
				0.04f, 	// Amplitude
				4f		// Freq
			};
			object eventData = shakeOptions;
			onEnemyHitCamShake.Raise(shakeOptions);
			onEnemyHitRumble.Raise(timeToRumbleWhenHit);
			meleeReceiver.GetHitByMelee();
			addScore.Raise(pointsForMeleeHit);
		}
	}
}
