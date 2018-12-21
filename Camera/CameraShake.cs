using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	[Header("Shake Default Values")]
	[SerializeField]
	private float timeToShake = 0.5f;
	[SerializeField]
	private float amplitude = 1f;
	[SerializeField]
	private float frequency = 1f;

	private float timeShaking = 0f;
	private bool isShaking = false;
	private CinemachineBasicMultiChannelPerlin camNoise;

	private void Awake() {
		camNoise = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		ResetCameraShake();
	}
	
	public void OnCameraShake(object data) {
		if (data != null) {
			float[] shakeOptions = (float[]) data;
			amplitude = shakeOptions[1];
			frequency = shakeOptions[2];
			if (isShaking == false) {
				timeToShake = shakeOptions[0];
				isShaking = true;
			} else {
				timeToShake = shakeOptions[0];
			}
		}
	}

	public void OnDefaultCameraShake(object data) {
		timeToShake = 0.25f;
		amplitude = -0.3f;
		frequency = 5f;
		isShaking = true;
	}

	private void LateUpdate() {
		if (isShaking) {
			camNoise.m_FrequencyGain = frequency;
			camNoise.m_AmplitudeGain = amplitude;
			timeShaking += Time.deltaTime;
			if (timeShaking >= timeToShake) {
				ResetCameraShake();
			}
		}
	}

	private void ResetCameraShake() {
		timeShaking = 0f;
		isShaking = false;
		camNoise.m_FrequencyGain = 0f;
		camNoise.m_AmplitudeGain = 0f;
	}
}
