using UnityEngine;

// just controls the state of audio.
public class AD_Bathroom : MonoBehaviour {

	private void OnTriggerEnter(Collider other){
		if(other.GetComponent<PlayerController>()){
			AUD_Manager.SetState("LocationReverb", "Interior");
		}
	}

	private void OnTriggerExit(Collider other){
		if(other.GetComponent<PlayerController>()){
			AUD_Manager.SetState("LocationReverb", "Exterior");
		}
	}
}
