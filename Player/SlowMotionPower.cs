using System.Collections;
using UnityEngine;

public class SlowMotionPower : MonoBehaviour {

	[SerializeField]
	private BooleanVariable disableInput;
	[SerializeField]
	private InputState input;
	[SerializeField]
	private GameEvent activateSlowMotionEffect;
	[SerializeField]
	private GameEvent onSlowMotionPowerAvailable;
	[SerializeField]
	private GameEvent onSlowMotionEffectReset;
	[SerializeField]
	private float specialPowerScoreTarget;
	[SerializeField]
	private PlayerState playerState;
	[SerializeField]
	private FloatVariable specialPowerStatus;
	[SerializeField]
	private float slowMotionTimeLength = 2f;
	
	// Use this for initialization
	private void Awake () {
		ResetSpecialPowerStatus(null);
	}
	
	// Update is called once per frame
	void Update () {
		if (disableInput.Value) return;

		if (input.bButtonPressed && specialPowerStatus.Value == 1f && playerState.isUsingSpecialPower == false) {
			playerState.isUsingSpecialPower = true;
			specialPowerStatus.Value = 0f;
			activateSlowMotionEffect.Raise(slowMotionTimeLength);
		}
	}

	public void AddScore(object data) {
		if (specialPowerStatus.Value < 1f && playerState.isUsingSpecialPower == false) {
			float scoreToAdd = (float) data;
			specialPowerStatus.Value += scoreToAdd.Normalize(0f, specialPowerScoreTarget);
			if (specialPowerStatus.Value >= 1f) {
				specialPowerStatus.Value = 1f;
				onSlowMotionPowerAvailable.Raise(null);
				AUD_Manager.PostEvent("PH_UI_Up_ST", gameObject);
			}
		}
	}

	public void ResetSpecialPowerStatus(object data) {
		specialPowerStatus.Value = 0f;
		playerState.isUsingSpecialPower = false;
		StartCoroutine(DeactivateSlowMotionIcon());
	}

	private IEnumerator DeactivateSlowMotionIcon() {
		yield return null;
        onSlowMotionEffectReset.Raise(null);
        yield break;
	}

}
