using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedParticles : MonoBehaviour {

	private ParticleSystem particles;

	private void Awake() {
		particles = GetComponentInChildren<ParticleSystem>();
	}

	public void Activate() {
		particles.Play();
	}
	
	public void Deactivate() {
		particles.Stop();
		particles.Clear();
	}
}
