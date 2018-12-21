using UnityEngine;

/*****************************************************************************
The AI for the turret.
***************************************************************************** */

public class AI_Turret : MonoBehaviour, IDamageable {

	[HideInInspector]
	public float 		mHealth;

    // Array of player Vectors representing positions. 
    [SerializeField]
    public TransformVariable     	mPlayerTrans;

	[SerializeField]
	private TR_Model				mModel;

	[SerializeField]
	private GameObject				mDeathParticles;

	[HideInInspector]
	public WP_TurGun		mCannon;

	private Grounded		mGrounder;

	private void Awake(){
		mCannon = GetComponentInChildren<WP_TurGun>();
		mGrounder = GetComponent<Grounded>();

		mHealth = 100f;
	}

	// Every frame, raycast to the player.
	// If we can see him, then we rotate to him and shoot at him.
	private void Update(){

		CheckAndHandleDeath();

		// initially, always point to the player.
		bool canSeePlayer = CanSeePlayer(transform.position, mPlayerTrans.Value.position);
		if(AngleFromForward(mPlayerTrans.Value.position) > 0.6 && canSeePlayer){
			PointToPlayer();
		}else{
			SpinAimlessly();
		}

		// if we can see the player, try to fire the cannon.
		// Raycast to check the player.
		bool shouldFire = false;
		if(canSeePlayer && Vector3.Distance(transform.position, mPlayerTrans.Value.position) < mCannon.mGunProperties.mRange){
			if(AngleFromForward(mPlayerTrans.Value.position) > 0.95f){
				shouldFire = true;
			}
		}
		if(shouldFire){
			mCannon.TryToFireGun(mPlayerTrans.Value);
		}else{
			mCannon.StopTryingToFire();
		}

		mModel.SetRotation(transform.rotation);

		// keep feet on ground - have to put grounder a bit above feet.
        if(!mGrounder.IsGrounded()){
            Vector3 pos = transform.root.position;
			// kinda smooth in the slide to the ground.
            pos.y -=  mGrounder.DisFromGround(pos) * 0.1f;
            pos.y += mGrounder.GetSetDis();
            transform.root.position = pos;
        }
	}

	// Set our rotation to be in the direction of the player.
	// Note, we might start using the cross product ourselves for this function.
    private void PointToPlayer(){
        // transform.rotation = Quaternion.LookRotation(rigidBody.velocity);
		Vector3 dirToPlayer = transform.position - mPlayerTrans.Value.position;
		// transform.rotation = Quaternion.LookRotation(-dirToPlayer);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-dirToPlayer, Vector3.up), 30f * Time.deltaTime);

    }


	/*****************************************************************************
	Working as intended.
	***************************************************************************** */
	private float AngleFromForward(Vector3 target){
		// get vector from us to target.
		Vector3 dis = target - transform.position;
		dis = Vector3.Normalize(dis);

		// calculate angle from this vector, to our forward vector.
		float angle = Vector3.Dot(dis, transform.forward);

		return angle;
	}

	/*******************************************************************************
	Turret just spins around aimlessly if it can't see the player.
	******************************************************************************* */
	private void SpinAimlessly(){
		transform.RotateAround(transform.position, Vector3.up, 180 * Time.deltaTime);
	}

    private bool CanSeePlayer(Vector3 ourPos, Vector3 playerPos){

        // actually this gives us the direction from 2 -> 1
        Vector3 direction = (playerPos - ourPos).normalized;

        // Note, can always add a layer mask here if needed. 
		int layerMask = 1<<LayerMask.NameToLayer("LevelGeometry");
        layerMask |= 1<<LayerMask.NameToLayer("Player");

		Debug.DrawRay(ourPos, direction * 10f, Color.green, 0.1f);

        RaycastHit hit;
        if (Physics.Raycast(ourPos, direction, out hit, Mathf.Infinity, layerMask))
        {
			Debug.DrawRay(ourPos, direction, Color.blue, 0.1f);

            if(hit.transform.GetComponent<PlayerController>()){
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
		Debug.Log("Turret Getting hit");
        mHealth -= damage;
    }

	private void CheckAndHandleDeath(){
        if(mHealth <= 0f){
            Die();
        }
    }

	[ContextMenu("Die")]
    private void Die(){

        Instantiate(mDeathParticles, transform.position, transform.rotation);
        Destroy(transform.root.gameObject);
    }
}


public class LeftRightTest : MonoBehaviour {
	public Transform target;
	public float dirNum;
	

	void Update () {
		Vector3 heading = target.position - transform.position;
		dirNum = AngleDir(transform.forward, heading, transform.up);
	}
	
	
	float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		
		if (dir > 0f) {
			return 1f;
		} else if (dir < 0f) {
			return -1f;
		} else {
			return 0f;
		}
	}
	
}