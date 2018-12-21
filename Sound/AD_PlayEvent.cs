using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_PlayEvent : MonoBehaviour {

	[SerializeField]
	private string nameOfEvent;

	[SerializeField]
	private string[]		SwitchGroups;
	[SerializeField]
	private string[] 		SwitchStates;
	
	// Update is called once per frame
	private void Start() {

		for(int i=0; i<SwitchGroups.Length; i++){
			AUD_Manager.SetSwitch(SwitchGroups[i], SwitchStates[i], gameObject);
		}

		AUD_Manager.PostEvent(nameOfEvent, gameObject);
	}
}
