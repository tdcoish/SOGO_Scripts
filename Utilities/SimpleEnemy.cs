using UnityEngine;
using Photon.Pun;
using System.Collections;

public class SimpleEnemy : MonoBehaviour, IDamageable {

	[SerializeField]
	private TransformVariable player;
	[SerializeField]
	private float timeBetweenShots = 3f;
	[SerializeField]
	private bool canFire = true;
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private float health = 100f;

	private void Awake()
	{
		StartCoroutine(StateFiring());
	}

	private void OnEnable()
	{
		canFire = true;
	}

	private void Disable()
	{
		canFire = false;
	}

	private void Shoot() 
	{
		Vector3 dir = player.Value.position - transform.position;
		GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(dir.normalized, Vector3.up));
		newBullet.GetComponent<Bullet>().SetOwner(transform);
		newBullet.GetComponent<Bullet>().SetTarget(player.Value);
	}

	IEnumerator StateFiring() {
		while (true) {
			if (canFire == false) yield return null;
			yield return new WaitForSeconds(timeBetweenShots);
			Shoot();
		}
	}

    public void TakeDamage(float damage)
    {
        health -= damage;
		if (health <= 0f) Destroy(gameObject);
    }
}
