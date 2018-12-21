using UnityEngine;

/**********************************************************************************
This room spawns 3 grunts when a trigger is hit. The grunts have no weapons, and 
basically just run around.
******************************************************************************** */
public class TU_MeleeRoom : MonoBehaviour {

	[SerializeField]
	private GameObject				mTutorialGrunt;
	private GameObject[]			mEntities;

	[SerializeField]
	private GameObject[]			mSpawnPoints;

	private bool 			mRoomEntered = false;

	private void Awake(){
		mEntities = new GameObject[mSpawnPoints.Length];
	}

	// maybe we spawn up some walls or something like that.
	// We definitely spawn some enemies.
	public void OnRoomEntered(){
		if(mRoomEntered) return;

		mRoomEntered = true;
		// now do the stuff.

		for(int i=0; i<mSpawnPoints.Length; i++){
			if(mEntities[i] == null){
				mEntities[i] = Instantiate(mTutorialGrunt, mSpawnPoints[i].transform.position, mSpawnPoints[i].transform.rotation);
			}
		}
	}
	
	// Basically reverse what we did here.
	public void OnRoomExit(){
		for(int i=0; i<mEntities.Length; i++){
			if(mEntities[i] != null){
				mEntities[i].GetComponentInChildren<AI_Controller>().TakeDamage(10000f);
			}
		}
		mRoomEntered = false;
	}
}
