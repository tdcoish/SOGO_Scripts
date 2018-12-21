using UnityEngine;

public class UI_BulletIndicator : MonoBehaviour {

	[SerializeField]
	private RectTransform redIndicator;
	[SerializeField]
	private RectTransform blueIndicator;
	[SerializeField]
	private FloatVariable blueBulletCount;
	[SerializeField]
	private FloatVariable redBulletCount;

	private void Awake() {
		redBulletCount.Value = 0f;
		blueBulletCount.Value = 0f;
	}

	private void Update() {
		if (blueBulletCount.Value == 0f && redBulletCount.Value == 0f) {
			redIndicator.localScale = new Vector3(1f, 0f, 1f);
			blueIndicator.localScale = new Vector3(1f, 0f, 1f);	
			return;
		};
		UpdateIndicatorScale();
	}

	private void UpdateIndicatorScale() {
		Mathf.Clamp(redBulletCount.Value, 0f, Mathf.Infinity);
		Mathf.Clamp(blueBulletCount.Value, 0f, Mathf.Infinity);

		Vector3 redIndicatorNewScale = redIndicator.localScale;
		Vector3 blueIndicatorNewScale = blueIndicator.localScale;
		
		if (blueBulletCount.Value >= redBulletCount.Value) {
			if (float.IsNaN(redBulletCount.Value.Normalize(0f, blueBulletCount.Value)) || float.IsNaN(blueBulletCount.Value.Normalize(0f, blueBulletCount.Value))) {
				// Debug.Log(redBulletCount.Value);
				// Debug.Log(blueBulletCount.Value);
				// Debug.Break();
			}
			// redIndicatorNewScale.y = redBulletCount.Value.Normalize(0f, blueBulletCount.Value);
			// blueIndicatorNewScale.y = blueBulletCount.Value.Normalize(0f, blueBulletCount.Value);
		} else {
			if (float.IsNaN(redBulletCount.Value.Normalize(0f, redBulletCount.Value)) || float.IsNaN(blueBulletCount.Value.Normalize(0f, redBulletCount.Value))) {
				// Debug.Log(redBulletCount.Value);
				// Debug.Log(blueBulletCount.Value);
				// Debug.Break();
			}
			// redIndicatorNewScale.y = redBulletCount.Value.Normalize(0f, redBulletCount.Value);
			// blueIndicatorNewScale.y = blueBulletCount.Value.Normalize(0f, redBulletCount.Value);
		}

		redIndicator.localScale = redIndicatorNewScale;
		blueIndicator.localScale = blueIndicatorNewScale;
	}
}
