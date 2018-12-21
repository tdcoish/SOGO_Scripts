using UnityEngine;

public class FollowTransformVariable : MonoBehaviour {

	[SerializeField]
	private TransformVariable target;
	[SerializeField]
	private bool syncPosition = true;
	[SerializeField]
	private bool syncRotation = true;
	
	// Update is called once per frame
	void Update () {
		if (target.Value != null) {
			if (syncPosition) transform.position = target.Value.position;
			if (syncRotation) transform.rotation = target.Value.rotation;
		}
	}
}
