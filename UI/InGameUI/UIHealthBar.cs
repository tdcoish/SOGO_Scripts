using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour {

	[SerializeField]
	private StringVariable selectedCharacter;
	[SerializeField]
	private FloatVariable playerHealth;
	[SerializeField]
	private float timeSettingNewImage = 1f;
	
	[Space]
	[Header("Icon Images with background")]
	[SerializeField]
	private Image playerIconImg;
	[SerializeField]
	private Sprite jonIcon;
	[SerializeField]
	private Sprite domIcon;

	[Space, Header("Health Bar BG Color Changing")]
	[SerializeField]
	private Image healthStatusBG;
	[SerializeField]
	private Color normalColor;
	[SerializeField]
	private Color hurtColor;
	[SerializeField]
	private Color healingColor;

	[Space, Header("Particles")]
	[SerializeField]
	private ParticleSystem hbGlowyBackground;
	[SerializeField]
	private ParticleSystem hbExplosion;
	[SerializeField]
	private ParticleSystem hbExplosionDark;

	[SerializeField]
	private Image healthbarFillImg;

	private enum IconState {
		Normal,
		Hurt,
		Heal
	}

	private IconState currentState;
	
	private float lastIconChangeTimestamp = 0f;
	
	private void Awake() {
		if (selectedCharacter.Value == "jon") playerIconImg.sprite = jonIcon;
		else if (selectedCharacter.Value == "dom") playerIconImg.sprite = domIcon;
		SetIconState(IconState.Normal);
	}	

	public void Update() {
		
		healthbarFillImg.fillAmount = playerHealth.Value.Normalize(0, 100f);

		if (currentState == IconState.Normal) return;
		if (Time.time >= lastIconChangeTimestamp + timeSettingNewImage) {
			SetIconState(IconState.Normal);
			
		}
	}

	private void SetIconState(IconState state) {
		switch(state) {
			case IconState.Normal: {
				healthStatusBG.color = normalColor;
				break;
			}
			case IconState.Hurt: {
				healthStatusBG.color = hurtColor;
				lastIconChangeTimestamp = Time.time;
				break;
			}
			case IconState.Heal: {
				healthStatusBG.color = healingColor;
				lastIconChangeTimestamp = Time.time;
				hbGlowyBackground.Simulate(0f, false, true, true);
				hbExplosion.Simulate(0f, false, true, true);
				hbExplosionDark.Simulate(0f, false, true, true);
				break;
			}
		}
		currentState = state;
	}

	public void IncreaseHealth(object data) {
		SetIconState(IconState.Heal);
	}
	
	public void DecreaseHealth(object data) {
		SetIconState(IconState.Hurt);
	}
}
