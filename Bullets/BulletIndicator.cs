using UnityEngine;

public class BulletIndicator : PoolableObject {

	[SerializeField]
	private BulletIndicatorProperties indicatorProperties;
	[SerializeField]
	private DeflectorProperties deflectorProperties;

	private SpriteRenderer sr;

	private void Awake() {
		sr = GetComponent<SpriteRenderer>();
	}

	public void SetScale(float distance) {
		Color opacityChange = sr.color;
		
		if (distance > deflectorProperties.maxDistanceToPerfectDeflection) {
			// It is outside the "perfect range
			float normalizedScale = distance.Normalize(deflectorProperties.maxDistanceToPerfectDeflection, indicatorProperties.minDistanceRange);
			float denormalizedScale = normalizedScale.Denormalize(indicatorProperties.outerRingSize, indicatorProperties.incommingBulletRingMaxSize);
			transform.localScale = Vector3.one * denormalizedScale;
			opacityChange.a = 1f - normalizedScale;
		} else {
			// Is within range
			float normalizedScale = distance.Normalize(0f, deflectorProperties.maxDistanceToPerfectDeflection);
			float denormalizedScale = normalizedScale.Denormalize(indicatorProperties.innerRingSize, indicatorProperties.outerRingSize);
			opacityChange.a = 1f;	
			transform.localScale = Vector3.one * denormalizedScale;
		}
		
		sr.color = opacityChange;
	}

}
