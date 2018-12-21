using UnityEngine;

public class AD_PauseMenu : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue		mOpenPauseMenu;

	[SerializeField]
	private SO_AD_Dialogue		mClosePauseMenu;

	[SerializeField]
	private StringVariable		Player;

	public void OnStartPause(){
		AUD_Manager.PlayerDialogue(mOpenPauseMenu, gameObject, Player.Value);
	}

	public void OnStopPause(){
		AUD_Manager.PlayerDialogue(mClosePauseMenu, gameObject, Player.Value);
	}
}
