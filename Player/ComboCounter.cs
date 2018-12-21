using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCounter : MonoBehaviour {

	[SerializeField]
	private IntegerVariable comboCounter;
	[SerializeField]
	private GameEvent OnMultiplePerfectDeflections;
	[SerializeField]
	private float timeWindowToTriggerSlowMotion = 0.5f;
	[SerializeField]
	private GameEvent addScore;
	[SerializeField]
	private float pointsForDeflectionCombo = 500f;

	private float lastPerfectDeflectionTimestamp = 0f;

	// Use this for initialization
	private void Awake () {
		ResetComboCounter();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastPerfectDeflectionTimestamp + timeWindowToTriggerSlowMotion && comboCounter.Value != 0) {
			ResetComboCounter();
		}
	}

	public void OnPerfectDeflection(object data) {
		if (Time.time <= lastPerfectDeflectionTimestamp + timeWindowToTriggerSlowMotion) {
			comboCounter.Value++;
			OnMultiplePerfectDeflections.Raise(null);
			addScore.Raise(pointsForDeflectionCombo);
			lastPerfectDeflectionTimestamp = Time.time;
		} else {
			comboCounter.Value = 1;
			lastPerfectDeflectionTimestamp = Time.time;
		}
	}

	private void ResetComboCounter() {
		lastPerfectDeflectionTimestamp = 0f;
		comboCounter.Value = 0;
	}
}
