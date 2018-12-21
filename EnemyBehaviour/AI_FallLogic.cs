using UnityEngine;
using UnityEditor;

public class AI_FallLogic : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue		mHitsGround;

	[HideInInspector]
	public float		mTimeStartedFalling;

	private Rigidbody 			mRigid;
	private AI_Controller		mEntity;
	private AN_Enemies			mAnim;

	private void Awake(){
		mRigid = GetComponent<Rigidbody>();
		mEntity = GetComponent<AI_Controller>();
		mAnim = UT_FindComponent.FindComponent<AN_Enemies>(gameObject);
	}

	public void Run(){
		// mAnim.mState = AnimState.FLYING;

        // after a certain amount of time, switch states back to combat
		// before that, let us handle collisions again.
        float timePassed = Time.time - mTimeStartedFalling;
		if(timePassed > 4f){
			// if they haven't hit the ground in 4 seconds, then it's a bug, so just kill it.
			mEntity.mStateChange = STATE.COMBAT;
		}
	}

	private void OnCollisionEnter(Collision other){
		if(mEntity.mState == STATE.FLYING){

			// Only collisions with the floor matter.
			Debug.Log("Collision while falling with: " + other.gameObject);
			if(other.gameObject.tag == "Ground"){
				AUD_Manager.DynamicDialogueFromData(mHitsGround, gameObject);
				mEntity.TakeDamage(20f);
				mEntity.mStateChange = STATE.COMBAT;
			}

		}
	}
}
