using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChargeVisualIndicator : MonoBehaviour {

	private ParticleSystem[] particles;

	private void Awake() {
		particles = GetComponentsInChildren<ParticleSystem>();
	}

	public void Activate() {
		foreach(ParticleSystem ps in particles) {
			ps.Play();
		}
	}
	
	public void Deactivate() {
		foreach(ParticleSystem ps in particles) {
			ps.Stop();
		}
	}
}
