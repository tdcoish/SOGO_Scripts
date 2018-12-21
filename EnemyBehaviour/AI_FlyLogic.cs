using UnityEngine;

/********************************************************************************
Enemies go flying until they hit a wall or something.
This is where I wish I had some OnStateEnter() function.
****************************************************************************** */
public class AI_FlyLogic : MonoBehaviour {

	[SerializeField]
	private SO_AD_Dialogue		mHitsWall;

	[HideInInspector]
	public float		mTimeStartedFlying;

	[SerializeField]
	private VectorVariable		mRotSpeed;

	private Rigidbody 			mRigid;
	private AI_Controller		mEntity;
	private AN_Enemies			mAnim;
	private ObjectFollower		mModelFollower;

	private void Awake(){
		mRigid = GetComponent<Rigidbody>();
		mEntity = GetComponent<AI_Controller>();
		mAnim = UT_FindComponent.FindComponent<AN_Enemies>(gameObject);
		mModelFollower = UT_FindComponent.FindComponent<ObjectFollower>(gameObject);
	}

	public void Run(){


		mRigid.constraints = RigidbodyConstraints.None;
		// mAnim.mState = AnimState.FLYING;

        // after a certain amount of time, switch states back to combat
		// before that, let us handle collisions again.
        float timePassed = Time.time - mTimeStartedFlying;
		// If they have been flying for > 4 seconds, then just kill them.
		if(timePassed > 4f){
			mEntity.TakeDamage(100000000000f);
			Debug.Log("Killed because flying too long");
			return;
		}
		if(timePassed > 0.05f) mRigid.detectCollisions = true;

        if(timePassed > mEntity.GetBase().mTimeFly){
            mEntity.mStateChange = STATE.COMBAT;
        }

		// make them tumble in the air.
		mModelFollower.rotateSpeed = 100000000000f;
		Vector3 rot = transform.rotation.eulerAngles;
		rot.x += mRotSpeed.Value.x; rot.y += mRotSpeed.Value.y; rot.z += mRotSpeed.Value.z;
		transform.rotation = Quaternion.Euler(rot);
	}

	private void OnCollisionEnter(Collision other){
		if(mEntity.mState == STATE.FLYING){
			AUD_Manager.DynamicDialogueFromData(mHitsWall, gameObject);
			mEntity.mStateChange = STATE.COMBAT;
			mEntity.TakeDamage(60f);
		}
	}
}
