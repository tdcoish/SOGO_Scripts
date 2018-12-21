using UnityEngine;

// The announcer, he announces what you're doing in the menu
public class AD_Announcer : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue		mIntro;

	[SerializeField]
	private SO_AD_Dialogue		mCredits;

	[SerializeField]
	private SO_AD_Dialogue		mOptions;

	[SerializeField]
	private SO_AD_Dialogue		mSelectChar;

	[SerializeField]
	private SO_AD_Dialogue		mExit;

	private void Start(){	
		AUD_Manager.DynamicDialogueFromData(mIntro, gameObject);
	}

	// public void OnMainMenuScreen(){
	// 	AUD_Manager.DynamicDialogueFromData(mIntro, gameObject);
	// }

	// nah, we not doing that anymore
	public void OnCreditsScreen(){
		// AUD_Manager.DynamicDialogueFromData(mCredits, gameObject);
	}

	public void OnOptionsScreen(){
		// AUD_Manager.DynamicDialogueFromData(mOptions, gameObject);
	}

	public void OnSelectCharScreen(){
		// AUD_Manager.DynamicDialogueFromData(mSelectChar, gameObject);
	}

	public void OnExitScreen(){
		AUD_Manager.DynamicDialogueFromData(mExit, gameObject);
	}
}
