using UnityEngine;

[RequireComponent(typeof(AD_Dialogue))]
public class HACK_PlayDynamicDialogue : MonoBehaviour {

	private AD_Dialogue		mDialogue;

	private void Awake(){
		mDialogue = GetComponent<AD_Dialogue>();	
	}

		// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){

			mDialogue.PostYourself();
		}
	}

}
