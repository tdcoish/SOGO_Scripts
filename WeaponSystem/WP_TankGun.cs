using UnityEngine;

// CHARGING == RELOADING for all intents and purposes.
public enum CANNON_STATE : int {
	INACTIVE,
	CHARGING,
	PREFIRE
}

/***********************************************************************************************************
Tanks cannon charges up, then fires a bunch in a volley, then needs to start charging up from the start again.
It's up to the tank to control the state of his gun, and he should be doing this every frame.
********************************************************************************************************** */
public class WP_TankGun : WP_Gun {

	// Needed to write this system because the old WP_Gun wasn't really cutting it.
	[SerializeField]
	private WP_TNK_Gun		mCannonProperties;

	private float	 		mCurChargeAmt;
	private float 			mCurChamberAmt;
	private int 			mNumFiredThisVolley = 0;

	[HideInInspector]
	public CANNON_STATE		mState;

	[SerializeField]
	private Transform		mFirePoint;
	[HideInInspector]
	public Transform			mVictim;

	private AI_Controller		mEntity;

	private void Awake(){
		mState = CANNON_STATE.INACTIVE;
		mEntity = GetComponentInParent<AI_Controller>();
	}

	private void Update(){

		switch(mState){
			case CANNON_STATE.INACTIVE: RunInactiveState(); break;
			case CANNON_STATE.CHARGING: RunChargingState(); break;
			case CANNON_STATE.PREFIRE: 	RunPreFireState(); 	break;
		}

		if((mCurChargeAmt / mCannonProperties.mMaxChargeUpTime) >= 0.7){
            mChargeParticles.Activate();
        }else{
            mChargeParticles.Deactivate();
        }
	}

	private void RunInactiveState(){
		mCurChargeAmt -= Time.deltaTime;
		if(mCurChargeAmt < 0f )
			mCurChargeAmt = 0f;
	}

	private void RunChargingState(){
		mCurChargeAmt += Time.deltaTime;
		if(mCurChargeAmt >= mCannonProperties.mMaxChargeUpTime){
			// fire, this will also change the state to firing.
			Fire();
		}
	}

	private void RunPreFireState(){
		mCurChamberAmt += Time.deltaTime;
		if(mCurChamberAmt >= mCannonProperties.mTimeBetweenShots){
			Fire();
		}
	}

	private void Fire(){

		mFireTimeStamp = Time.time;

		Vector3 dir = Vector3.Normalize(mVictim.position - transform.position);

		GameObject clone;
		bool red = (Random.value < 0.5);
		if(red){
			clone = Instantiate(mCannonProperties.mRedBullet, mFirePoint.position, Quaternion.LookRotation(dir, Vector3.up));
		}else{
			clone = Instantiate(mCannonProperties.mBlueBullet, mFirePoint.position, Quaternion.LookRotation(dir, Vector3.up));
		}

		clone.GetComponent<Bullet>().SetOwner(mEntity.transform);
		clone.GetComponent<Bullet>().SetTarget(mVictim);

		Instantiate(mGunProperties.mMuzzleBlast, mFirePoint.transform.position, transform.rotation);

        AUD_Manager.PostEvent(mGunProperties.mAudFireEvent, gameObject);
		AUD_Manager.DynamicDialogueFromData(mEntity.GetBase().mShootLine, gameObject);

		mEntity.GetBase().mEnemyFired.Raise(null);

		// Tank cannon specific stuff.
		mNumFiredThisVolley++;
		if(mNumFiredThisVolley == mCannonProperties.mNumInVolley){
			mState = CANNON_STATE.CHARGING;
			mCurChargeAmt = 0f;
			mNumFiredThisVolley = 0;
		}else{
			mState = CANNON_STATE.PREFIRE;
			mCurChamberAmt = 0f;
		}
	}

	public override void TryToFireGun(Transform victim){
		mVictim = victim;

		if(mState == CANNON_STATE.INACTIVE){
			mState = CANNON_STATE.CHARGING;
			// AUD_Manager.PostEvent(mGunProperties.mAudPreFireEvent, gameObject);
		}
	}

	public override void StopTryingToFire(){
		mState = CANNON_STATE.INACTIVE;
	}

	// returns the charge from 0-1, 1 being fullest.
    public override float GetNormalizedCharge(){
        float val = mCurChargeAmt;
        val/=mCannonProperties.mMaxChargeUpTime;

        return val;
    }

}
