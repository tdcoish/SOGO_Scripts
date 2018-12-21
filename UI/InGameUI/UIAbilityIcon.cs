using UnityEngine;
using UnityEngine.UI;

public class UIAbilityIcon : MonoBehaviour {

	protected bool shouldGlow = true;

	[SerializeField]
	private Image overlayImg;
	[SerializeField]
	private float glowSpeed = 1f;
	[SerializeField]
	private FloatVariable status;

	private void Update() {
		Color nextColor = overlayImg.color;

		if (shouldGlow == false) {
			nextColor.a = 1f;
			overlayImg.fillAmount = status.Value;
		} else {
			nextColor.a = Mathf.Abs(Mathf.Sin(Time.time * glowSpeed));
			overlayImg.fillAmount = 1f;
		}

		overlayImg.color = nextColor;
	}
	
}
