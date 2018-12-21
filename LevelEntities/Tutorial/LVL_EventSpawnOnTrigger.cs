using UnityEngine;

// So for example, the hit a platform, spawns HitJumpPlatformEvent
// They hit the area where they pose, spawns OnPoseAreaHit
public class LVL_EventSpawnOnTrigger : MonoBehaviour {

	[SerializeField]
	private GameEvent		OnThisTriggersEvent;

	private bool			mEventTriggered = false;

	private void OnTriggerEnter(Collider other){
		if(!mEventTriggered){
			if(other.GetComponent<PlayerController>()){
				mEventTriggered = true;
				OnThisTriggersEvent.Raise(null);
			}
		}
	}
}
