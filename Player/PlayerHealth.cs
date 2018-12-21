using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable {

	public FloatVariable health;
	
	[SerializeField]
	private BooleanVariable canDie;
	[SerializeField]
	private GameEvent playerDied;
	[SerializeField]
	private GameEvent playerTookDamage;
	[SerializeField]
	private GameEvent onCameraShake;
	[SerializeField]
	private GameEvent onControllerRumble;
	[SerializeField]
	private float timeToRumbleWhenHit = 0.5f;
	[SerializeField]
	private IntegerVariable comboCounter;
	[SerializeField]
	private DeflectorProperties deflectorProperties;
	[SerializeField]
	private PlayerState playerState;

	private void Awake() {
		playerState.isPlayerDead = false;
		health.Value = 100f;
		comboCounter.Value = 0;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.D)) {
			TakeDamage(100f);
		}
	}

	public void TakeDamage(float damage) {
		if (playerState.isPlayerDead) return;

		comboCounter.Value = 0;
		onControllerRumble.Raise(timeToRumbleWhenHit);
		float[] shakeOptions = { 
			0.25f,	// Time
			0.1f, 	// Amplitude
			3f		// Freq
		};
		object eventData = shakeOptions;
		onCameraShake.Raise(shakeOptions);
		
		health.Value -= damage;
		if (health.Value < 0f) health.Value = 0f;

		if (health.Value == 0f && canDie.Value == false) {
			playerState.isPlayerDead = true;
			playerDied.Raise(null);
		}else{
			playerTookDamage.Raise(null);
		}
	}

	public void IncreaseHealth (object data) {
		if (playerState.isPlayerDead) { health.Value = 0f; return; };

		float multiplier = (float) data;
		float healthToIncrease = multiplier.Denormalize(deflectorProperties.minHealthIncrease, deflectorProperties.maxHealthIncrease);
		health.Value += healthToIncrease;
		if (health.Value > 100f) {
			health.Value = 100f;
		}
	}

	// Called by listening to the OnCheckpointRestarted event
	public void ResetHealth(object data){
		playerState.isPlayerDead = false;
		health.Value = 100f;
	}
}
