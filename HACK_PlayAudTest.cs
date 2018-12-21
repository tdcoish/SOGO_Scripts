using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HACK_PlayAudTest : MonoBehaviour {

	[SerializeField]
	private string nameOfEvent;

	[SerializeField]
	private bool	usesSwitch;

	[SerializeField]
	private string switchGroup;
	[SerializeField]
	private string switchState;

	[SerializeField]
	private bool	usesSwitch2;

	[SerializeField]
	private string switchGroup2;
	[SerializeField]
	private string switchState2;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			// AUD_Manager.SetSwitch("Voice", "Banter", gameObject);
			// AUD_Manager.PostEvent("VC_ALIENGRUNT_ST", gameObject);

			// Play a different sound as well
			// AUD_Manager.PostEvent("PC_OilSplash_LP_PL", gameObject);
			// AUD_Manager.PostEvent("PC_OilThrow_LP_PL", gameObject);

			if(usesSwitch){
				Debug.Log("Switch Group: " + switchGroup);
				Debug.Log("Switch State: " + switchState);
				AUD_Manager.SetSwitch(switchGroup, switchState, gameObject);
			}

			if(usesSwitch2){
				Debug.Log("Switch Group: " + switchGroup2);
				Debug.Log("Switch State: " + switchState2);
				AUD_Manager.SetSwitch(switchGroup2, switchState2, gameObject);
			}

			AUD_Manager.PostEvent(nameOfEvent, gameObject);
		}
	}
}
