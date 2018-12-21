using UnityEngine;

// Basically, enemies within a small range will constantly attack.
// Enemies in the back will take turns attacking.
public class AI_AttackFromRange : MonoBehaviour {

	private AI_Controller 	mEntity;
	private AI_Conditions	mCons;
	private AI_Orienter		mOrienter;
	private AI_Strafer		mStrafer;
	private WP_Gun			mGun;
	private Rigidbody		mRigid;
	private AN_Enemies		mAnim;

	private bool 			mWasWithinRange = false;

	private void Awake(){
		mEntity = GetComponent<AI_Controller>();
		mCons = GetComponent<AI_Conditions>();
		mOrienter = GetComponent<AI_Orienter>();
		mStrafer = GetComponent<AI_Strafer>();
		mGun = GetComponentInChildren<WP_Gun>();
		mRigid = GetComponent<Rigidbody>();
		mAnim = UT_FindComponent.FindComponent<AN_Enemies>(gameObject);
	}

	// Lots of side effects. May have to make one that doesn't have master tell us to fire.
	public void Run(){

		float dis = Vector3.Distance(mEntity.mPlayerTrans.Value.position, transform.position);

		bool doStrafe = false;
		if(dis < mGun.mGunProperties.mRange*0.8f && mCons.mCanSeePlayer && mWasWithinRange){
			doStrafe = true;
		}
		if(dis < mGun.mGunProperties.mRange && mCons.mCanSeePlayer){
			doStrafe = true;
		}

		if(doStrafe){

			// now we strafe and stuff.
			mRigid.rotation = mOrienter.OrientToSpot(mEntity.mPlayerTrans.Value.position);
			
			if(Vector3.Distance(transform.position, mEntity.mPlayerTrans.Value.position) < mEntity.GetBase().mAlwaysFireRange){
				mGun.TryToFireGun(mEntity.mPlayerTrans.Value);
			}else{
				mEntity.mMaster.ThisEnemyWantsToFire(mEntity);
			}

			// now we have to check if we're really close. Let's say within 5f meters for now. Eventually, put this in as a back up range in SO_AI_Base.
			// If so, we move backwards, and animate for backwards.
			if(dis < 5f){
				Vector3 dir = transform.position - mEntity.mPlayerTrans.Value.position;
				dir = Vector3.Normalize(dir);
				mEntity.mGoalPos = transform.position + dir*10f;
				mAnim.mState = AnimState.BACKING_UP;
				mEntity.mCurMaxVel = mEntity.GetBase().mMaxSpd * 0.5f;
				return;
			}

			// we get the enemies to slowly move side to side when in range.
			if(mStrafer.NeedsNewStrafeSpot()){
				mEntity.mGoalPos = mStrafer.FindStrafeSpot();
			}
			mEntity.mCurMaxVel = mEntity.GetBase().mMaxSpd * 0.4f;

			// eventually set us to strafing. For now, just set us to idle.
			// Actually, we have to figure out if we're strafing left or right.
			float dot = Vector3.Dot(transform.right, (mEntity.mGoalPos - transform.position));
			if(dot > 0f){
				// means we're going to our right.
				mAnim.mState = AnimState.STRAFING_RIGHT;
			}else{
				mAnim.mState = AnimState.STRAFING_LEFT;
			}
		}else{
			// now we just try to move to the player.
			mEntity.mGoalPos = mEntity.mPlayerTrans.Value.position;
			mEntity.mCurMaxVel = mEntity.GetBase().mMaxSpd;
			mRigid.rotation = mOrienter.OrientToDirection(mRigid.velocity);

			mAnim.mState = AnimState.MOVING_FORWARD;
		}
	}
}
