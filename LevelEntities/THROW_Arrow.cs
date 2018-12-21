using UnityEngine;

public class THROW_Arrow : MonoBehaviour {

	private Grounded 		mGrounder;

	private void Awake(){
		mGrounder = GetComponentInChildren<Grounded>();
	}

	private void Update(){
		if(mGrounder.DisFromGround(mGrounder.transform.position) > 0.1f){
			Vector3 pos = transform.position;
			pos.y *= 0.9f;
			transform.position = pos;
		}
	}
}
