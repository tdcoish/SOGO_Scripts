using UnityEngine;

public class Billboard : MonoBehaviour {

	private Camera mainCam;

	private void Awake() {
		mainCam = Camera.main;
	}

	private void Update() {
		transform.forward = mainCam.transform.forward;
	}

}
