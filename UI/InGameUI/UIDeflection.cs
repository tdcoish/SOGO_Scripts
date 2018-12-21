using UnityEngine;
using UnityEngine.UI;


public class UIDeflection : MonoBehaviour {


	[Header("Variables")]
	[SerializeField]
	private BooleanVariable redDeflectorActive;
	[SerializeField]
	private BooleanVariable blueDeflectorActive;
	[SerializeField]
	private StringVariable selectedCharacter;
	
	[Space]
	[Header("Blue Deflector Images")]
	[SerializeField]
	private Image blueDeflectorImg; 
	[SerializeField]
	private Image redDeflectorImg; 
	[SerializeField]
	private Sprite jonDeflectorActiveImg; 
	[SerializeField]
	private Sprite jonDeflectorInactiveImg; 
	[SerializeField]
	private Sprite domDeflectorActiveImg; 
	[SerializeField]
	private Sprite domDeflectorInactiveImg; 
	
	[Space]
	[Header("Red Deflector Images")]
	[SerializeField]
	private Sprite redDeflectorActiveImg; 
	[SerializeField]
	private Sprite redDeflectorInactiveImg; 

	[Space]
	[Header("Arm Particles")]
	[SerializeField]
	private ParticleSystem leftTriggerParticle;
	[SerializeField]
	private ParticleSystem rightTriggerParticle;
	[SerializeField]
	private ParticleSystem leftArmParticles;
	[SerializeField]
	private ParticleSystem rightArmParticles;

	[Space]
	[Header("Sun Particles")]
	[SerializeField]
	private ParticleSystem[] sunParticleSystems;
	
	private bool cachedBlueDeflectorState = false;
	private bool cachedRedDeflectorState = false;

	private void Update() {
		if (blueDeflectorActive.Value) {
			blueDeflectorImg.sprite = jonDeflectorActiveImg;
			if (selectedCharacter.Value == "dom") {
				blueDeflectorImg.sprite = domDeflectorActiveImg;
			}
			if (blueDeflectorActive.Value != cachedBlueDeflectorState) {
				leftTriggerParticle.Simulate(0f, false, true, true);
				leftArmParticles.Simulate(0f, false, true, true);
			}
		} else {
			blueDeflectorImg.sprite = jonDeflectorInactiveImg;
			if (selectedCharacter.Value == "dom") {
				blueDeflectorImg.sprite = domDeflectorInactiveImg;
			}
			if (!blueDeflectorActive.Value != cachedBlueDeflectorState) {
				leftArmParticles.Stop();
				leftArmParticles.Clear();
			}
		}

		if (redDeflectorActive.Value) {
			redDeflectorImg.sprite = jonDeflectorActiveImg;
			if (selectedCharacter.Value == "dom") {
				redDeflectorImg.sprite = domDeflectorActiveImg;
			}
			if (redDeflectorActive.Value != cachedRedDeflectorState) {
				rightTriggerParticle.Simulate(0f, false, true, true);
				rightArmParticles.Simulate(0f, false, true, true);
			}
		} else {
			redDeflectorImg.sprite = jonDeflectorInactiveImg;
			if (selectedCharacter.Value == "dom") {
				redDeflectorImg.sprite = domDeflectorInactiveImg;
			}
			if (!redDeflectorActive.Value != cachedBlueDeflectorState) {
				rightArmParticles.Stop();
				rightArmParticles.Clear();
			}
		}

		cachedBlueDeflectorState = blueDeflectorActive.Value;
		cachedRedDeflectorState = redDeflectorActive.Value;
	}

	public void TriggerSunAnimation(object data) {
		foreach (ParticleSystem ps in sunParticleSystems) {
			ps.Simulate(0f, false, true, true);
		}
	}
}
