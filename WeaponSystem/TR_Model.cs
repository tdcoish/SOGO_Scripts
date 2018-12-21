using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR_Model : MonoBehaviour {

	[SerializeField]
	private float			yRotBias;

	[SerializeField]
	private TR_ModelCannon	mCannonMod;

	public void SetRotation(Quaternion parentRot){

		float xRot = 0f;

		// We set our rotation, without y, then we set the cannon rotation, with y.
		Vector3 noXRot = parentRot.eulerAngles;
		xRot = noXRot.x;
		noXRot.x = 0f;
		Quaternion newRot = Quaternion.Euler(noXRot);
		transform.rotation = newRot;
		transform.RotateAround(transform.position, transform.up, yRotBias);

		// Now we gotta set the turrets cannon still with the x rotation.
		Vector3 canRot = mCannonMod.transform.rotation.eulerAngles;
		canRot.z = xRot;
		mCannonMod.transform.rotation = Quaternion.Euler(canRot);
	}
}
