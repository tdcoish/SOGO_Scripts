using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour {

	[SerializeField]
	private Transform target;
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 relativePos = target.position - transform.position;
            Quaternion destRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = destRotation;
		}
	}
}
