using UnityEngine;

public class AD_TutMusic : MonoBehaviour {

	private void Awake(){
		AUD_Manager.SetState("BattleState", "NonEngaged");
		AUD_Manager.SetSwitch("Objective", "Objective1", gameObject);
		AUD_Manager.PostEvent("MS_GAMEPLAY_LP_PL", gameObject);
	}

	public void HandleTutorialOver(){
		AUD_Manager.PostEvent("MS_GAMEPLAY_LP_SP", gameObject);
		AUD_Manager.PostEvent("IG_EndGame_SPALL", gameObject);		
	}

}
