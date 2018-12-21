using UnityEngine;

public class AD_Poser : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue		mPoseDialogue;

	private string 		mEvent = "PC_FlexPose_ST_PL";

	[SerializeField]
	private StringVariable		Player;

	public void OnPoseStarted(object data){
		AUD_Manager.PlayerDialogue(mPoseDialogue, gameObject, Player.Value);
		AUD_Manager.PostEvent(mEvent, gameObject);
	}
	
}

