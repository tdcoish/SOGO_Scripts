using UnityEngine;

public class WP_TurGun : WP_Gun {

	[SerializeField]
	private float temp;

	[SerializeField]
	public WP_TUR_Gun		mTurretGunProperties;
	[HideInInspector]
	public float	 		mCurChargeAmt;

	[HideInInspector]
	public Transform			mVictim;

	[HideInInspector]
	public CANNON_STATE		mState;

	[SerializeField]
	private Transform		mFirePoint;

	private void Awake(){
		mState = CANNON_STATE.INACTIVE;
	}

	private void Update(){

		switch(mState){
			case CANNON_STATE.INACTIVE: RunInactiveState(); break;
			case CANNON_STATE.CHARGING: RunChargingState(); break;
		}
	}

	private void RunInactiveState(){
		mCurChargeAmt -= Time.deltaTime;
		if(mCurChargeAmt < 0f )
			mCurChargeAmt = 0f;
	}

	private void RunChargingState(){
		mCurChargeAmt += Time.deltaTime;
		if(mCurChargeAmt >= mTurretGunProperties.mMaxChargeUpTime){
			// fire, this will also change the state to firing.
			Fire();
		}
	}

	private void Fire(){

		mFireTimeStamp = Time.time;

		Vector3 dir = Vector3.Normalize(mVictim.position - transform.position);

		GameObject clone = Instantiate(mTurretGunProperties.mBullet, mFirePoint.position, Quaternion.LookRotation(dir, Vector3.up));
		clone.GetComponent<Bullet>().SetOwner(transform.parent);
		clone.GetComponent<Bullet>().SetTarget(mVictim);

		Instantiate(mGunProperties.mMuzzleBlast, mFirePoint.transform.position, transform.rotation);

        AUD_Manager.PostEvent(mGunProperties.mAudFireEvent, gameObject);

		mCurChargeAmt = 0f;
	}


	public override void TryToFireGun(Transform victim){
		mVictim = victim;
		mState = CANNON_STATE.CHARGING;
	}

	public override void StopTryingToFire(){
		mState = CANNON_STATE.INACTIVE;
	}

}
