using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ScreamFX : MonoBehaviour {

	[SerializeField]
	private string 		mScream = "EN_Die_PL";

	[SerializeField]
	private string[] 		mSwitchGroups;

	[SerializeField]
	private string[]		mSwitchStates;

	// This object has a lifetime, so we can rely on that 
	private void Start(){
		for(int i=0; i<mSwitchGroups.Length; i++){
			AUD_Manager.SetSwitch(mSwitchGroups[i], mSwitchStates[i], gameObject);
		}

		AUD_Manager.PostEvent(mScream, gameObject);
	}
}
