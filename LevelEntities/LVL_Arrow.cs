using UnityEngine;

public class LVL_Arrow : MonoBehaviour {

	// Just rotate around a point.
	[SerializeField]
	private float		mDegreesPerSecond;

	// actually, also gently go up and down.
	[SerializeField]
	private float		mVertDis;

	[SerializeField]
	private float 		mCyclesPerSecond;

	private Vector3   	startPos;

	private void Awake(){
		startPos = transform.position;	
	}

	private void Update(){
		transform.Rotate(Vector3.up * Time.deltaTime*mDegreesPerSecond);

		Vector3 pos = startPos;
		pos.y += Mathf.Cos(Time.time*mCyclesPerSecond) * mVertDis;
		transform.position = pos;
	}
}
