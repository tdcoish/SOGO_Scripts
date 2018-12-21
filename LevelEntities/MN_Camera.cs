using UnityEngine;

/**************************************************************************************
This script controls the camera in the main menu. For right now it just rotates around 
the scene. Eventually it will do something different.
************************************************************************************ */
public class MN_Camera : MonoBehaviour {

	[SerializeField]
	private float		mRad;

	// How often a circle is completed.
	[SerializeField]
	private float		mPeriod;

	private float		PERIOD_OF_CIRCLE;

	[SerializeField]
	public Vector3		mRotPoint;


	private void Awake(){
		PERIOD_OF_CIRCLE = 2*Mathf.PI;
	}

	// Just rotate around in a circle.
	private void Update(){

		// multiply the time by the period of a circle, so that the natural perdiod is set to one
		float modTime = Time.time*PERIOD_OF_CIRCLE;

		// Now modify it so that our inserted perdiod is valid
		modTime/=mPeriod;			// so 6 translates to 6 seconds, 0.5 translates to 0.5 seconds.

		Vector3 pos = new Vector3(); 
		pos.y = transform.position.y;

		pos.x = Mathf.Cos(modTime) * mRad;
		pos.z = Mathf.Sin(modTime) * mRad;

		transform.position = pos;
		
		// gotta also change the rotation.
		Vector3 dir = mRotPoint - transform.position;
		transform.rotation = Quaternion.LookRotation(dir);
	}
}
