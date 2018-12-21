using UnityEngine;

public class UIArrow : MonoBehaviour {

	[SerializeField]
	private VectorVariable target;
	[SerializeField]
	private GameObject arrow;
	
	// Update is called once per frame
	void Update () {
		if (arrow.activeInHierarchy && target.Value != null && target.Value != Vector3.zero) {
			Vector3 relativePos = target.Value - transform.position;
			Quaternion destRotation = Quaternion.LookRotation(relativePos);
			transform.rotation = destRotation;
		}
	}

	public void ShowArrow(object data) {
		arrow.SetActive(true);
	}
	
	public void HideArrow(object data) {
		arrow.SetActive(false);
	}
}
