using UnityEngine;

// Just the raw data for playing a certain dialogue line.
[CreateAssetMenu(fileName="DynamicDialogue", menuName="SunsOutGunsOut/AUD/DynamicDialogue")]
public class SO_AD_Dialogue : ScriptableObject {

	public string 				mEventName;

	public string[]			mArgs;
}
