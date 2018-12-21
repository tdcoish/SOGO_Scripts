using UnityEngine;

public class DeflectionStamina : MonoBehaviour {

	[SerializeField]
	private FloatVariable stamina;
	[SerializeField]
	private BooleanVariable blueDeflectorActive;
	[SerializeField]
	private BooleanVariable redDeflectorActive;
	[SerializeField, Range(0f, 1f)]
	private float staminaDrainSpeed = 0.1f;
	[SerializeField, Range(0f, 1f)]
	private float staminaRechargeSpeed = 0.25f;
	[SerializeField]
	private float timeBeforeStaminaRecharges = 2f;
	private float lastTimeActiveTimestamp = 0f;

	private void Awake() {
		stamina.Value = 1f;
	}

	private void Update() {
		if (blueDeflectorActive.Value || redDeflectorActive.Value) {
			DrainStamina();
			return;
		} else if (Time.time > lastTimeActiveTimestamp + timeBeforeStaminaRecharges && stamina.Value < 1f) {
			RechargeStamina();
		}
	}

	private void DrainStamina() {
		lastTimeActiveTimestamp = Time.time;
		stamina.Value -= Time.deltaTime * staminaDrainSpeed;
		if (stamina.Value < 0f) {
			stamina.Value = 0f;
		}
	}

	private void RechargeStamina() {
		stamina.Value += Time.deltaTime * staminaRechargeSpeed;
		if (stamina.Value > 1f) {
			stamina.Value = 1f;
			lastTimeActiveTimestamp = 0f;
		}
	}
}
