using UnityEngine;


/*********************************************************************************
This is a little different because we already have three in the room. Then when the room is 
entered, the ones that have been previously destroyed are respawned back in.
******************************************************************************* */
public class TU_PoseRoom : MonoBehaviour {

	[SerializeField]
	private GameObject		mTurPrefab;

	[SerializeField]
	private GameObject[]			mSpawnPoints;
	[SerializeField]
	private AI_Turret[]				mTurrets;

	private bool 			mRoomEntered = false;

	// maybe we spawn up some walls or something like that.
	// We definitely spawn some enemies.
	public void OnRoomEntered(){
		if(mRoomEntered) return;

		mRoomEntered = true;
		// now do the stuff.

		for(int i=0; i<mTurrets.Length; i++){
			if(mTurrets[i] == null){
				var clone = Instantiate(mTurPrefab, mSpawnPoints[i].transform.position, mSpawnPoints[i].transform.rotation);
				mTurrets[i] = UT_FindComponent.FindComponent<AI_Turret>(clone.gameObject);
			}
		}
	}
	
	// Basically reverse what we did here.
	public void OnRoomExit(){
		for(int i=0; i<mTurrets.Length; i++){
			if(mTurrets[i])
				mTurrets[i].TakeDamage(100000f);
		}
		mRoomEntered = false;
	}

}
