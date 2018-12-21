using UnityEngine;

public class LVL_Objective : MonoBehaviour {

	public float 			mHealth = 100f;
	private UI_HealthBarEnemy		mHealthBar;

	private void Awake(){
		mHealthBar = UT_FindComponent.FindComponent<UI_HealthBarEnemy>(gameObject);
	}

	private void Update(){
		mHealthBar.SetGraphics(mHealth);
	}

	// This used to be more complicated.
	void OnTriggerEnter(Collider other){

		// Hack this in.
		if(UT_FindComponent.FindComponent<Bullet>(other.gameObject)){
			mHealth -= 10f;
		}

	}
	
}
