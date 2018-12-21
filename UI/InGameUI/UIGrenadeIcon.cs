using UnityEngine;
using UnityEngine.UI;

public class UIGrenadeIcon : UIAbilityIcon {

	[Space]
	[SerializeField]
	private Image grenadeIconImg;
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
	private ParticleSystem grenadeOutlineParticle;

	public void ActivateIcon(object data) {
		shouldGlow = true;
		rimParticle.Simulate(0f, false, true, true);
		circleParticle.Simulate(0f, false, true, true);
		grenadeOutlineParticle.Simulate(0f, false, true, true);
		grenadeIconImg.sprite = activeIcon;
	}
	
	public void DeactivateIcon(object data) {
		shouldGlow = false;
		rimParticle.Simulate(0f, false, true, true);
		grenadeIconImg.sprite = inactiveIcon;
	}
}
