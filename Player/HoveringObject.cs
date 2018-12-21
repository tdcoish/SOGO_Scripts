using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringObject : MonoBehaviour {

	[SerializeField]
	private float checkingDistance = 1.2f;
	[SerializeField]
	private LayerMask masks;
	[SerializeField]
	float distanceFromTheGround = 0.5f;
	[SerializeField]
	float defaultDistance = 0.9f;
	[SerializeField]
	private GameObject objectToHover;

	private Vector3 hoverUpDir = Vector3.zero;

	// Update is called once per frame
	void Update () {
		Vector3 newPosition = objectToHover.transform.position;

		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, checkingDistance, masks)) {
			newPosition.y = (hit.point + Vector3.up * distanceFromTheGround).y;
			hoverUpDir = hit.normal;
		} else {
			newPosition = gameObject.transform.position + Vector3.down * defaultDistance;
			hoverUpDir = Vector3.up;
		}

		objectToHover.transform.position = newPosition;
		objectToHover.transform.up = Vector3.Slerp(objectToHover.transform.up, hoverUpDir, 0.25f);
	}
}
