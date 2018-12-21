/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public class SlowMotionEffect : MonoBehaviour {

	[SerializeField]
	private float slowDownFactor = 0.05f;
	[SerializeField]
	private float slowDownRecoveryTime = 2f;
	[SerializeField]
	private float timeBeforeStartRecovering = 1f;
	[SerializeField]
	private float defaultTimeBeforeStartRecovering = 1f;
	[SerializeField]
	private GameEvent onSlowMotionEffectEnded;

	private bool isGamePaused = false;
	private bool triggeredBySpecialMove = false;

	private float cachedTimeScale = 1f;
	private float lastSlowMotionEffectStartTimestamp = 0f;

	private void Awake() {
		isGamePaused = false;
	}

	private void Update() {
		if (isGamePaused == false && Time.unscaledTime >= lastSlowMotionEffectStartTimestamp + timeBeforeStartRecovering) {
			Time.timeScale += (1f / slowDownRecoveryTime) * Time.unscaledDeltaTime; // Not affected 
			Time.fixedDeltaTime = Time.timeScale * 0.02f; // Because fixed update runs at 50fps
			Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

			if (Time.timeScale == 1f && cachedTimeScale != Time.timeScale) {
				AUD_Manager.PostEvent("FX_Slomo_LP_SP", gameObject);
				if (triggeredBySpecialMove) {
					onSlowMotionEffectEnded.Raise(null);
					triggeredBySpecialMove = false;
				}
			}

			cachedTimeScale = Time.timeScale;
		}
	}

	public void OnSlowMotionEffectTriggered(object data) {
		timeBeforeStartRecovering = defaultTimeBeforeStartRecovering;
		StartSlowMotion();
	}

	public void OnSpecialPowerTriggered(object data) {
		float timeInSlowMotion = data == null ? 0f : (float) data;
		timeBeforeStartRecovering = timeInSlowMotion;
		triggeredBySpecialMove = true;
		StartSlowMotion();
	}

	private void StartSlowMotion() {
		lastSlowMotionEffectStartTimestamp = Time.unscaledTime;
		Time.timeScale = slowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.02f; // Because fixed update runs at 50fps
		AUD_Manager.PostEvent("FX_Slomo_LP_PL", gameObject);
	}

	public void OnGamePaused(object data) {
		isGamePaused = true;
	}
	
	public void OnGameUnpaused(object data) {
		isGamePaused = false;
	}
}
