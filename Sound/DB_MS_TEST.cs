using UnityEngine;

public class DB_MS_TEST : MonoBehaviour {

	[SerializeField]
	private SO_AD_Sound			mMusic;

	private string 		mIntensity = "NonEngaged";

	private void Start(){
		AUD_Manager.SetState("BattleState", "NonEngaged");
		AUD_Manager.EventFromData(mMusic, gameObject);
	}

	private void Update(){

		if(Input.GetMouseButtonDown(0)){
			if(mIntensity == "NonEngaged"){
				mIntensity = "Engaged";
			}else{
				mIntensity = "NonEngaged";
			}

			AUD_Manager.SetState("BattleState", mIntensity);
		}
	}
}
