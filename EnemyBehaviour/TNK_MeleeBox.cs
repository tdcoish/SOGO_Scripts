using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNK_MeleeBox : MonoBehaviour {

	// TODO: This potentially triggers multiple times if the player moves slightly in and out of the damage. We could disable the collisions for a small time 
	// to make sure this doesn't happen
	// Also, make the tank do less damage based upon difficulty
	[SerializeField]
	private StringVariable 				Difficulty;

	private float		mLastHitStamp;
	private float 		mCooldown = 0.1f;

	private void OnTriggerEnter(Collider other){

		if(Time.time - mLastHitStamp > mCooldown){
			if(other.GetComponent<PlayerHealth>()){
				mLastHitStamp = Time.time;

				PlayerHealth pHealth = other.GetComponent<PlayerHealth>();
				float dam = 50f;
				if(Difficulty.Value == "NORMAL")
					dam *= 0.5f;
				
				if(Difficulty.Value == "EASY")
					dam *= 0.2f;
				pHealth.TakeDamage(dam);
			}	

		}
	}
}
