using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	[SerializeField]
	private Bullet		bulletPrefab;

	private void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			//shoot 
			Instantiate(bulletPrefab, transform.position, transform.rotation);
		}
	}

}
