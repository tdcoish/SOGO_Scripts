using UnityEngine;


/**************************************************************************************
This script is attached to the menus, and makes the menus follow the camera around.
All the various menu screens attached to the menus have a certain position, all on the same 
plane, and the active one is transported in front of the camera.
************************************************************************************ */
public class UI_Menus : MonoBehaviour {

	[SerializeField]
	private Camera			mCamera;

	[SerializeField]
	private float			mDisInFrontOfCamera = 10f;
	
	[SerializeField]
	private float 			mLeftSkew = 1f;

	// Script also has 

	// Snap our position to be somewhere in front of the camera at all times.
	private void Update(){
		// Gets a new position somewhat in front of the camera.
		Vector3 camDir = mCamera.transform.forward;
		camDir = Vector3.Normalize(camDir);

		Vector3 leftDir = mCamera.transform.right * -1;
		leftDir = Vector3.Normalize(leftDir);
		
		Vector3 pos = mCamera.transform.position + camDir*mDisInFrontOfCamera + leftDir*mLeftSkew; 

		transform.position = pos;

		transform.rotation = mCamera.transform.rotation;
	}
}
