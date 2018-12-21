using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour {

	[SerializeField]
	private GameObject objectToFollow;
	[SerializeField]
	private float movementSpeed = 0.25f;
	public float rotateSpeed = 1000f;
	[HideInInspector]
	public bool useInterpolation = true;

	private bool useCustomRotation = false;
	
	private void FixedUpdate () {
		if (useInterpolation) {
			transform.position = transform.position.Hermite(objectToFollow.transform.position, movementSpeed);
		} else {
			transform.position = objectToFollow.transform.position;
		}

		transform.rotation = Quaternion.RotateTowards(transform.rotation, objectToFollow.transform.rotation, rotateSpeed * Time.deltaTime);
		if (useCustomRotation) {
			transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
		}
	}

	public void OnPlayerRedirecting (object data) {
		useCustomRotation = (bool) data;
	}
}

