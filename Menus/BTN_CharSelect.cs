using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BTN_CharSelect : MonoBehaviour, ISelectHandler {

	[SerializeField]
	private SO_AD_Dialogue		mPickMeLine;
	[SerializeField]
	private string 				Player;

	// Sigh.
	[SerializeField]
	private MN_SnapCamera				mCam;

	public void OnSelect(BaseEventData eventData){
		AUD_Manager.PlayerDialogue(mPickMeLine, mCam.gameObject, Player);
	}
}
