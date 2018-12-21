using UnityEngine;


/**************************************************************************************
It's really simple. Listen to when the player has transitioned to different areas. Also,
listen to events that govern the estimated intensity of any individual wave.
************************************************************************************ */
public class AD_LvlMusic : MonoBehaviour {

	// All music sounds have 
	[SerializeField]
	SO_AD_Sound				mFirstStage;

	[SerializeField]
	SO_AD_Sound				mSecondStage;

	[SerializeField]
	SO_AD_Sound				mThirdStage;

	[SerializeField]
	SO_AD_Sound				mFourthStage;

	private int 			mInd = 0;

	private void Awake(){
		AUD_Manager.SetState("BattleState", "NonEngaged");
	}

	// Actually probably shouldn't be called.
	public void OnBeachBegun(){
		AUD_Manager.PostEvent("MS_GAMEPLAY_LP_SP", gameObject);		
		AUD_Manager.EventFromData(mFirstStage, gameObject);
		mInd = 0;
	}
	public void OnBeachOver(){
		AUD_Manager.PostEvent("MS_GAMEPLAY_LP_SP", gameObject);
		AUD_Manager.EventFromData(mSecondStage, gameObject);
		mInd = 1;
	}
	public void OnFirstSectionOver(){
		AUD_Manager.PostEvent("MS_GAMEPLAY_LP_SP", gameObject);		
		AUD_Manager.EventFromData(mSecondStage, gameObject);
		mInd = 1;
	}
	public void OnSecondSectionOver(){
		Debug.Log("Third section audio");
		AUD_Manager.PostEvent("MS_GAMEPLAY_LP_SP", gameObject);		
		AUD_Manager.EventFromData(mThirdStage, gameObject);
		mInd = 2;
	}
	public void OnThirdSectionOver(){
		Debug.Log("Fourth Section audio");
		AUD_Manager.PostEvent("MS_GAMEPLAY_LP_SP", gameObject);
		AUD_Manager.EventFromData(mFourthStage, gameObject);
		mInd = 3;
	}

	public void OnPlayerDied(){
		AUD_Manager.PostEvent("MS_PCLose_ST", gameObject);
	}
	public void OnCheckpointRestart(){
		AUD_Manager.PostEvent("MS_GAMEPLAY_LP_SP", gameObject);
		switch(mInd){
			case 0: AUD_Manager.EventFromData(mFirstStage, gameObject); break;
			case 1: AUD_Manager.EventFromData(mSecondStage, gameObject); break;
			case 2: AUD_Manager.EventFromData(mThirdStage, gameObject); break;
			case 3: AUD_Manager.EventFromData(mFourthStage, gameObject); break;
		}
	}

	public void OnLowIntensity(){
		AUD_Manager.SetState("BattleState", "NonEngaged");
	}
	public void OnHighIntensity(){
		AUD_Manager.SetState("BattleState", "Engaged");
	}

	public void OnGameEnd(){
		AUD_Manager.PostEvent("FX_Slomo_LP_SP", gameObject);
		AUD_Manager.PostEvent("MS_GAMEPLAY_LP_SP", gameObject);
		AUD_Manager.PostEvent("IG_EndGame_SPALL", gameObject);
	}
}
