using UnityEngine;

/****************************************************************************
This is the logic for after we have already been hit by a grenade.
************************************************************************** */
public class AI_GrenadedLogic : MonoBehaviour {

	[HideInInspector]
	public float		mTimeHitGrenade;

	private Rigidbody 			mRigid;
	private AI_Controller		mEntity;
	private AN_Enemies			mAnim;
	private StunnedParticles	mStunParticles;

	private void Awake(){
		mRigid = GetComponent<Rigidbody>();
		mEntity = GetComponent<AI_Controller>();
		mAnim = UT_FindComponent.FindComponent<AN_Enemies>(gameObject);
		mStunParticles = GetComponentInChildren<StunnedParticles>();
	}

	public void Run(){
		mAnim.mState = AnimState.GROSSED_OUT;

        mRigid.constraints = RigidbodyConstraints.FreezeAll;
        mRigid.velocity = Vector3.zero;

        // after a certain amount of time, switch states back to combat
        float timePassed = Time.time - mTimeHitGrenade;
        if(timePassed > mEntity.GetBase().mTimeInGrenadeStun){
            mEntity.mStateChange = STATE.COMBAT;
			mStunParticles.Deactivate();
        }
	}
}
