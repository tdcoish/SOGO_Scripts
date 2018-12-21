using UnityEngine;

public class LVL_SectionTrigger : MonoBehaviour {

	public LVL_Section			mOwner;

	private void OnTriggerStay(Collider other){

		if(mOwner.mSectionBeingRun && !mOwner.mPlayerHasHit){
			// mOwner.mPlayerHasHit = true;
			if(other.GetComponent<PlayerController>()){
				mOwner.mPlayerHasHit = true;
				mOwner.ActivateWallsAndArrowAndSpawnersOnStart();
				mOwner.OnSectionHit.Raise(null);
			}
		}
	}
}
