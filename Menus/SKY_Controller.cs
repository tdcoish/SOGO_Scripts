using UnityEngine;

public class SKY_Controller : MonoBehaviour {

	[SerializeField]
	private GameObject[]		Clouds;
	[SerializeField]
	private float[] 			RotClouds;

	private void Update(){
		for(int i=0; i<Clouds.Length; i++){
			UpdateCloudRotation(Clouds[i], RotClouds[i]);
		}
	}

	private void UpdateCloudRotation(GameObject clouds, float rotSpd){
		Vector3 rot = clouds.transform.rotation.eulerAngles;
		rot.y += rotSpd;
		clouds.transform.rotation = Quaternion.Euler(rot);
	}
}
