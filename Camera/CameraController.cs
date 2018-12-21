using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private bool isAiming;
	private CameraOrbit cameraOrbit;

	private void Awake() {
		cameraOrbit = FindObjectOfType<CameraOrbit>();
	}

	public void OnPlayerRedirect(object data) {
		cameraOrbit.gameObject.SetActive(!(bool) data);
	}
}
