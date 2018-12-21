using UnityEngine;

public class AD_Jump : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue		mJumpStart;

	[SerializeField]
	private SO_AD_Dialogue		mJumpLand;

	[SerializeField]
	private StringVariable		Player;

	public void HandleJump(){
		AUD_Manager.PostEvent("PC_JumpLand_ST", gameObject);
		AUD_Manager.PlayerDialogue(mJumpStart, gameObject, Player.Value);
	}

	public void HandleLand(){
		AUD_Manager.PostEvent("PC_JumpStart_ST", gameObject);	
		AUD_Manager.PlayerDialogue(mJumpLand, gameObject, Player.Value);	
	}
}
