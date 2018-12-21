using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LVL_WallDeactivator : MonoBehaviour {

	[SerializeField]
	private LVL_Wall		mWallToDeactivate;

	private void OnTriggerEnter(Collider other){
		if(UT_FindComponent.FindComponent<PlayerController>(other.gameObject)){
			mWallToDeactivate.gameObject.SetActive(false);
		}
	}
}
