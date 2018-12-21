using UnityEngine;

public class AD_DeathAndDam : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue 		mTookDamage;

	[SerializeField]
	private SO_AD_Dialogue		mDied;

	[SerializeField]
	private StringVariable		Player;

	public void OnPlayerTookDamage(){
		AUD_Manager.PlayerDialogue(mTookDamage, gameObject, Player.Value);
	}

	public void OnPlayerDied(){
		AUD_Manager.PlayerDialogue(mDied, gameObject, Player.Value);
	}
}
