using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

	[SerializeField]
	private float rotationSpeedX;
	[SerializeField]
	private float rotationSpeedY;
	[SerializeField]
	private float rotationSpeedZ;
	
	void Update () {
		Quaternion nextRotation = Quaternion.Euler(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);
		transform.rotation *= nextRotation;
	}
}
