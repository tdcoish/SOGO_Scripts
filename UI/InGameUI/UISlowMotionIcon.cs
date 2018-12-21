using UnityEngine;
using UnityEngine.UI;

public class UISlowMotionIcon : UIAbilityIcon {

	[Space]
	[SerializeField]
	private Image clockIconImg;
	[SerializeField]
	private Sprite activeIcon;
	[SerializeField]
	private Sprite inactiveIcon;

	[Space]
	[Header("Particles")]
	[SerializeField]
	private ParticleSystem rimParticle;
	[SerializeField]
	private ParticleSystem circleParticle;
	[SerializeField]
	private ParticleSystem clockOutlineParticle;

	public void ActivateIcon(object data) {
		shouldGlow = true;
		rimParticle.Simulate(0f, false, true, true);
		circleParticle.Simulate(0f, false, true, true);
		clockOutlineParticle.Simulate(0f, false, true, true);
		clockIconImg.sprite = activeIcon;
	}
	
	public void DeactivateIcon(object data) {
		shouldGlow = false;
		rimParticle.Simulate(0f, false, true, true);
		clockIconImg.sprite = inactiveIcon;
	}	
}
