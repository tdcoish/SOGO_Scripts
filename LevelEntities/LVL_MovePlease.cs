using UnityEngine;

/************************************************************
This script has a reference to the player, keeps track of where 
the player has moved, and if the player hasn't moved very far in x 
seconds, it will damage the player.

Update, actually it's just a drill that constantly fires at the player.
********************************************************** */

struct PositionTime{
	public Vector3 pos;
	public float time;
}

public class LVL_MovePlease : MonoBehaviour {

	// Make us update this at a certain interval
	[SerializeField]
	private float 				mUpdatePosTime = 1;
	private float				mLastUpdateTime;

	[SerializeField]
	private float 				mDelay = 4;

	[SerializeField]
	private TransformVariable 	mPlayerRef;

	int act;
	private PositionTime[]			mOldPositions;

	// What are the rules here?
	private void Update(){

		mOldPositions[act].pos = mPlayerRef.Value.position;
		mOldPositions[act].time = Time.time;
		act++;

		// now here we move to the position that is x seconds away.
		for(int i=0; i<mOldPositions.Length; i++){

		}
	}

	private void UpdatePosition(){
		if(Time.time - mLastUpdateTime > mUpdatePosTime){
			mOldPositions[act].pos = mPlayerRef.Value.position;
			mOldPositions[act].time = Time.time;
		}
	}

}
