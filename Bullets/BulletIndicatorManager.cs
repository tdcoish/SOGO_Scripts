using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletIndicatorManager : MonoBehaviour {

	[SerializeField]
	private DeflectorProperties deflectorProperties;
	[SerializeField]
	private BulletIndicatorProperties indicatorProperties;
	[SerializeField]
	private TransformVariable player;
	[SerializeField]
	private BulletTracker blueBulletTracker;
	[SerializeField]
	private BulletTracker redBulletTracker;
	[SerializeField]
	private EnumVariable blueBulletType;
	[SerializeField]
	private EnumVariable redBulletType;
	
	[SerializeField]
	private PoolableObject redIndicatorPrefab;
	[SerializeField]
	private PoolableObject blueIndicatorPrefab;
	[SerializeField]
	private int poolSize = 5;

	private void Awake() {
		blueBulletTracker.bullets = new List<Bullet>();
		redBulletTracker.bullets = new List<Bullet>();
	}

	private void Start() {
		PoolManager.CreatePool(redIndicatorPrefab, poolSize);
		PoolManager.CreatePool(blueIndicatorPrefab, poolSize);
	}

	private void Update() {
		int blueBulletsCount = blueBulletTracker.bullets.Count;
		int redBulletsCount = redBulletTracker.bullets.Count;
		
		if (blueBulletsCount == 0 && redBulletsCount == 0) return;

		if (blueBulletsCount > 0) {
			foreach (Bullet bullet in blueBulletTracker.bullets) {
				if (bullet != null) {
					DetermineVisibility(bullet, blueIndicatorPrefab);
				}
			}
		}

		if (redBulletsCount > 0) {
			foreach (Bullet bullet in redBulletTracker.bullets) {
				DetermineVisibility(bullet, redIndicatorPrefab);
			}
		}
	}

	private void DetermineVisibility(Bullet bullet, PoolableObject pObject) {
		Vector3 direction = bullet.transform.position - player.Value.transform.position;
		direction.y = 0;

		if (direction.magnitude <= indicatorProperties.minDistanceRange) {
			var clone = PoolManager.GetObjectFromPool(pObject);
			BulletIndicator indicator = clone.GetComponent<BulletIndicator>();
			indicator.transform.position = transform.position;
			indicator.transform.forward = transform.up;
			indicator.SetScale(direction.magnitude);
			indicator.gameObject.SetActive(true);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * deflectorProperties.maxDistanceToPerfectDeflection);
		Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * deflectorProperties.minDistanceToPerfectDeflection);
	}
}