using UnityEngine;

public class AD_Melee : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue 		mAttack;

	[SerializeField]
	private StringVariable		Player;

	public void PlayMeleeSounds(){
		AUD_Manager.PlayerDialogue(mAttack, gameObject, Player.Value);
		AUD_Manager.PostEvent("PC_MeleeWhoosh_ST_PL", gameObject);
	}
}
