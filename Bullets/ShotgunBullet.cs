using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet {

	[Space]
	[Header("Effects - Random Deflection")]
	[SerializeField]
	private ParticleSystem smokeParticles;
	[SerializeField]
	private ParticleSystem trailsParticles;

	[Space]
	[Header("Effects - Perfect Deflection")]
	[SerializeField]
	private GameObject normalParticles;
	[SerializeField]
	private ParticleSystem[] perfectParticles;

	protected override void SpawnSmokeParticles() {
		smokeParticles.Play();
		trailsParticles.Stop();
	}

	protected override void ChangeColor() {
		Destroy(normalParticles);
		foreach(ParticleSystem ps in perfectParticles) {
			ps.Play();
		}
	}

}
