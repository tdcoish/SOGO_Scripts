using UnityEngine;

// So for example, the hit a platform, spawns HitJumpPlatformEvent
// They hit the area where they pose, spawns OnPoseAreaHit
public class LVL_ConstantEventTrigger : MonoBehaviour {

	[SerializeField]
	private GameEvent		OnEnterEvent;
	[SerializeField]
	private bool 			mSpawnsOnLeave = false;
	[SerializeField]
	private GameEvent 		OnExitEvent;

	private bool			mEventTriggered = false;

	private void OnTriggerEnter(Collider other){
        if(other.GetComponent<PlayerController>()){
            OnEnterEvent.Raise(null);
        }
	}

	private void OnTriggerExit(Collider other){
		if(!mSpawnsOnLeave) return;

		if(other.GetComponent<PlayerController>()){
			OnExitEvent.Raise(null);
		}
	}
}
