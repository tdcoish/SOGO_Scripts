using UnityEngine.UI;
using UnityEngine;

public class UISlideIcon : UIAbilityIcon {

	[Space, Header("General")]
	[SerializeField]
	private Image slideIconImg;
	[SerializeField]
	private ParticleSystem rimParticle;
	[SerializeField]
	private ParticleSystem circleParticle;
	[SerializeField]
	private ParticleSystem outlineParticle;
	
	[Space, Header("Jon Images")]
	[SerializeField]
	private Sprite activeIcon;
	[SerializeField]
	private Sprite inactiveIcon;

	private void Awake() {
		ActivateIcon(null);
	}

	public void ActivateIcon(object data) {
		shouldGlow = true;
		rimParticle.Simulate(0f, false, true, true);
		circleParticle.Simulate(0f, false, true, true);
		slideIconImg.sprite = activeIcon;
		outlineParticle.Simulate(0f, false, true, true);
	}
	
	public void DeactivateIcon(object data) {
		shouldGlow = false;
		rimParticle.Simulate(0f, false, true, true);
		slideIconImg.sprite = inactiveIcon;
	}
}
