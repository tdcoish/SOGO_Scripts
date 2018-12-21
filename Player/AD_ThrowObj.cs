using UnityEngine;

public class AD_ThrowObj : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue		mThrowDialogue;

	[SerializeField]
	private StringVariable		Player;

	public void OnObjectThrown(){
		AUD_Manager.PlayerDialogue(mThrowDialogue, gameObject, Player.Value);
	}
}
