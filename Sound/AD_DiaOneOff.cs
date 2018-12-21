using UnityEngine;

public class AD_DiaOneOff : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue 		mDialogue;

	private void Start(){
		AUD_Manager.DynamicDialogueFromData(mDialogue, gameObject);
	}
}
