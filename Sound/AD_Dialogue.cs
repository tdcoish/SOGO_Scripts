using UnityEngine;

public class AD_Dialogue : MonoBehaviour {

	[SerializeField]
	private string 				mEventName;

	[SerializeField]
	private string[]			mArgs;

	public void PostYourself(){
		AUD_Manager.DynamicDialogue(mEventName, mArgs, gameObject);
	}
}
