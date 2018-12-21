using UnityEngine;

public class AI_Retreat : MonoBehaviour {

	[HideInInspector]
	public bool			mIsFleeing;
	[HideInInspector]
	public bool			mCanFlee = true;			// is on cooldown.
	private float		mTimeStartedFlee;

	private AI_Controller	mEntity;
	private AI_Orienter		mOrienter;
	private Rigidbody		mRigid;
	private AN_Enemies		mAnim;
	private AI_MoveToGoal	mMover;

	private void Awake(){
		mEntity = GetComponent<AI_Controller>();
		mOrienter = GetComponent<AI_Orienter>();
		mRigid = GetComponent<Rigidbody>();
		mAnim = UT_FindComponent.FindComponent<AN_Enemies>(gameObject);
		mMover = GetComponent<AI_MoveToGoal>();
	}

	// Run from the player, also plays sound at this point.
	public void Run(){

		mMover.mCanUsePath = false;
		// if we're not already charging, set our state to charging 
		if(!mIsFleeing){
			string[] args = new string[]{ "Grunt", "Cower" };
			AUD_Manager.DynamicDialogue("VO_Negatives_E", args, gameObject);

			mAnim.mState = AnimState.FLEEING;
			mIsFleeing = true;
			mTimeStartedFlee = Time.time;

			// set the goal to somewhere behind the player.
			Vector3 dir = transform.position - mEntity.mPlayerTrans.Value.position;
			dir.y = 0f;
			Vector3 spot = transform.position + Vector3.Normalize(dir) * 100f;
			mEntity.mGoalPos = spot;
		}

		mEntity.mCurMaxVel = mEntity.GetBase().mMaxSpd * 1.2f;

		mRigid.rotation = mOrienter.OrientToDirection(mRigid.velocity);

		// handle if the flee state has finished.
		if(Time.time - mTimeStartedFlee > mEntity.GetBase().mFleeTime.Value){
			mMover.mCanUsePath = true;
			mIsFleeing = false;
			mCanFlee = false;
			Invoke("CanFlee", 0.2f);
			mAnim.mState = AnimState.IDLE; 
		}
	}

	private void CanFlee(){
		mCanFlee = true;
	}
}
