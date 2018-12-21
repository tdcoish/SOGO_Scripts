using UnityEngine;

public class BulletManager : MonoBehaviour {

	[SerializeField]
	private BulletTracker blueBullets;
	[SerializeField]
	private BulletTracker redBullets;

	public void DestroyAllBullets(object data) {
		foreach (Bullet b in blueBullets.bullets) {
			b.DestroyBullet();
		}
		foreach (Bullet b in redBullets.bullets) {
			b.DestroyBullet();
		}
	}
}
