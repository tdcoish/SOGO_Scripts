using UnityEngine;

/************************************************************************************************************
The tank prefab will have this component. Basically all it contains is the timestamp of when we started charging,
a single bool whether we are charging or not, and a length of time that we can charge for.
********************************************************************************************************** */
public class AI_ChargeLogic : MonoBehaviour {

	[HideInInspector]
	public bool					mIsCharging;
	[HideInInspector]
	public bool					mCanCharge = true;			// is on cooldown.
	[HideInInspector]
	public float                mTimeStartedCharge;

	private Vector3 			mCachedDir;

	private TNK_MeleeBox		mMeleeHitBox;
	private AI_Controller		mEntity;
	private AI_Orienter			mOrienter;
	private Rigidbody			mRigid;
	private AN_Enemies			mAnim;
	private WP_TankGun			mCannon;
	private AI_Mover			mMover;

	private void Start(){
		mMeleeHitBox = UT_FindComponent.FindComponent<TNK_MeleeBox>(gameObject);
		mMeleeHitBox.gameObject.SetActive(false);

		mEntity = GetComponent<AI_Controller>();
		mOrienter = GetComponent<AI_Orienter>();
		mRigid = GetComponent<Rigidbody>();
		mAnim = UT_FindComponent.FindComponent<AN_Enemies>(gameObject);
		mCannon = UT_FindComponent.FindComponent<WP_TankGun>(gameObject);
		mMover = GetComponent<AI_Mover>();

		mCachedDir = new Vector3();
	}

	public void Run(){
		// spawn hitbox, double velocity, do other stuff

		mCannon.mState = CANNON_STATE.INACTIVE;

		// if we're not already charging, set our state to charging 
		if(!mIsCharging){
			mIsCharging = true;
			mTimeStartedCharge = Time.time;

			// set the goal to somewhere behind the player.
			Vector3 dir = mEntity.mPlayerTrans.Value.position - transform.position;
			dir.y = 0f;
			dir = Vector3.Normalize(dir);
			mCachedDir = dir;
			Vector3 spot = transform.position + Vector3.Normalize(dir) * 10f;
			Debug.DrawLine(transform.position, spot, Color.black, 5f);
			mEntity.mGoalPos = spot;
			
			mMeleeHitBox.gameObject.SetActive(true);

			mAnim.mState = AnimState.CHARGING;
		}

		// ugh. Gotta do some manual velocity LERPing here.
		mEntity.mCurMaxVel = mEntity.GetBase().mMaxSpd * 2.5f;
		float tmStartsSlowing = 1.1f;
		if(Time.time - mTimeStartedCharge > tmStartsSlowing){
			float tmSinceStart = Time.time - mTimeStartedCharge;
			float tmAfterStartedSlowing = tmSinceStart - tmStartsSlowing;
			float tmToZero = mEntity.GetBase().mChargeTime.Value-tmStartsSlowing;
			float percent = 1f - (tmAfterStartedSlowing/tmToZero);
			mEntity.mCurMaxVel *= percent;
		}

		mRigid.rotation = mOrienter.OrientToDirection(mCachedDir);

		// handle if the charge has finished.
		if(Time.time - mTimeStartedCharge > mEntity.GetBase().mChargeTime.Value){
			mIsCharging = false;
			mCanCharge = false;
			Invoke("CanCharge", 1.1f);

			mMeleeHitBox.gameObject.SetActive(false);
		}
	}

	private void CanCharge(){
		mCanCharge = true;
	}
}
