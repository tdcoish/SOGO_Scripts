using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeflectorParticles : MonoBehaviour {

	[SerializeField]
	private ParticleSystem[] redDeflectionParticles;
	[SerializeField]
	private ParticleSystem[] blueDeflectionParticles;
	[SerializeField]
	private BooleanVariable redDeflectorActive;
	[SerializeField]
	private BooleanVariable blueDeflectorActive;

	public void EmitDeflectionParticles(object data) {
		if (redDeflectorActive.Value && !blueDeflectorActive.Value) {
			foreach(ParticleSystem ps in redDeflectionParticles) {
				ps.Play();
			}
		} else if (blueDeflectorActive.Value && !redDeflectorActive.Value) {
			foreach(ParticleSystem ps in blueDeflectionParticles) {
				ps.Play();
			}
		}
	}
}
