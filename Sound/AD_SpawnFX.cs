using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_SpawnFX : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AUD_Manager.PostEvent("AM_GravLift_LP_PL", gameObject);
	}
	
	private void OnDestroy(){
        AUD_Manager.PostEvent("AM_GravLift_LP_SP", gameObject);		
	}
}
