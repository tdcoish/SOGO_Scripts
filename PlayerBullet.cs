using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************************************
Alright, so I tried, but I couldn't figure out how to do one very simple thing,
create a bullet with some flag that tells it which object to collide with.
This is so that the bullets fired by the turrets won't interact with 
turrets, but only the player and vice versa for the player bullets.

Possibly I could have created a generic projectile class, and then seperate
classes for each type of projectile, which would require much smaller 
classes, with less code repeat.
**********************************************************************/

public class PlayerBullet : MonoBehaviour {

	[Header("Damage Dealt")]
	[SerializeField]
	private float _Damage = 40f;

	[Header("Speed of Bullet")]
	[SerializeField]
	private float _Speed = 30000f;

	private Vector3 Velocity;

	private Rigidbody mRBody;


	// Use this for initialization
	void Start () {
		mRBody = GetComponent<Rigidbody>();

		//alright this is something of a hack, but we don't want any weird 
		//behaviour with the object colliding with the player
		InitialMoveForward();
	}

	void InitialMoveForward(){
		Vector3 increaseByFrame = new Vector3(0f, 0f, _Speed*2);
		Vector3 bulPos = transform.position;
		

		//Actually, translate into pure x and y coordinates first.
		transform.Translate(increaseByFrame);
	}

	void MoveForward(){
		//luckily, since they always move purely forward, movement is quite easy
		Vector3 increaseByFrame = new Vector3(0f, 0f, _Speed);
		Vector3 bulPos = transform.position;
		

		//Actually, translate into pure x and y coordinates first.
		transform.Translate(increaseByFrame);
	}
	
	private void Update()
	{
		
		MoveForward();
	}

	private void OnCollisionEnter(Collision other)
	{
		print("Collision");
		
		if(other.gameObject.CompareTag("Turret")){

			print("Player damaged turret");
			//do damage to the turret
			Destroy(other.gameObject);
			//delete yourself.
			Destroy(gameObject, 0);
		}
	}
}
