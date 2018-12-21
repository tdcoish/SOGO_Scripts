using UnityEngine;

public class GodMode : MonoBehaviour {
	
	[SerializeField]
	private BooleanVariable godMode;

	void Update () {
		if (Input.GetKeyDown(KeyCode.G)) {
			godMode.Value = !godMode.Value;
		}
	}
}
