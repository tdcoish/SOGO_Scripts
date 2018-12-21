using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherBullet : Bullet {

	[Space]
	[Header("Effects - Random Deflection")]
	[SerializeField]
	private ParticleSystem smokeParticles;
	[SerializeField]
	private GameObject normalParticles;

	[Space]
	[Header("Effects - Perfect Deflection")]
	[SerializeField]
	private Material perfectColorMaterial;
	[SerializeField]
	private MeshRenderer missileMesh;
	[SerializeField]
	private ParticleSystem[] perfectParticles;
	
	protected override void SpawnSmokeParticles() {
		Destroy(normalParticles);
		smokeParticles.Play();
	}

	protected override void ChangeColor() {
		Destroy(normalParticles);
		foreach(ParticleSystem ps in perfectParticles) {
			ps.Play();
		}
		missileMesh.material = perfectColorMaterial;
	}

	protected override void BulletSpecificDestroy() {
		smokeParticles.Stop();
		smokeParticles.gameObject.transform.SetParent(null);
		Destroy(smokeParticles.gameObject, 5f);
	}

}
