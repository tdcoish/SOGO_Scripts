using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectorRing : MonoBehaviour {

	[SerializeField]
	private SpriteRenderer outerRing;
	[SerializeField]
	private GameObject outerRingWhiteRing;
	[SerializeField]
	private BulletIndicatorProperties bulletIndicatorProperties;
	[SerializeField]
	private BooleanVariable redDeflectorActive;
	[SerializeField]
	private BooleanVariable blueDeflectorActive;
	[SerializeField]
	private BulletTracker blueBulletTracker;
	[SerializeField]
	private BulletTracker redBulletTracker;
	[SerializeField]
	private Color idleColor;
	[SerializeField]
	private Color redColor;
	[SerializeField]
	private Color blueColor;
	[SerializeField]
	private float colorCoolDownTime = 1f;
	private float colorCoolDownLastTimestamp = 0f;
	private Color lastColor;

	// Use this for initialization
	private void Awake () {
		lastColor = idleColor;
		outerRing.color = idleColor;
		outerRingWhiteRing.SetActive(false);
	}
	
	void Update () {
		outerRingWhiteRing.SetActive(redDeflectorActive.Value || blueDeflectorActive.Value);

		if (redDeflectorActive.Value == true) {
			colorCoolDownLastTimestamp = Time.time;
			lastColor = redColor;
			outerRing.color= redColor;
		} else if (blueDeflectorActive.Value == true) {
			colorCoolDownLastTimestamp = Time.time;
			lastColor = blueColor;
			outerRing.color= blueColor;
		} else if (Time.time < colorCoolDownLastTimestamp + colorCoolDownTime) {
			float normalizedTime = Time.time.Normalize(colorCoolDownLastTimestamp, colorCoolDownLastTimestamp + colorCoolDownTime);
			outerRing.color = Color.Lerp(lastColor, idleColor, normalizedTime);
		}
	}
}
