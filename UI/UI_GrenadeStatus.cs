using UnityEngine;
using UnityEngine.UI;

public class UI_GrenadeStatus : MonoBehaviour {

	[SerializeField]
	private GrenadeLauncherProperties grenadeLauncherProperties;
	[SerializeField]
    private FloatVariable lastGrenadeThrowTimestamp;
    [SerializeField]
    private BooleanVariable canThrowGrenade;

	[SerializeField]
	private float grenadeRotationSpeed = 25f;

	[SerializeField]
	private Image grenadeImage;
	[SerializeField]
	private Image grenadeRing;

	private RectTransform grenadeTransform;
	
	private void Awake() {
		grenadeTransform = grenadeImage.GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update () {
		if (canThrowGrenade.Value) {
			grenadeTransform.rotation *= Quaternion.Euler(0f, 0f, grenadeRotationSpeed * Time.deltaTime);
			Color grenadeColor = grenadeImage.color;
			grenadeColor.a = 1f;
			grenadeImage.color = grenadeColor;
		} else {
			float normalizedTime = Time.time.Normalize(lastGrenadeThrowTimestamp.Value, lastGrenadeThrowTimestamp.Value + grenadeLauncherProperties.coolDownTime);
			Color grenadeColor = grenadeImage.color;
			grenadeColor.a = 0f;
			grenadeImage.color = grenadeColor;

			Color ringColor = grenadeRing.color;
			ringColor.g = normalizedTime;
			grenadeRing.color = ringColor;
		}
	}
}
