using UnityEngine;

/******************************************************************************************************************
This is the entity spawned by the head arrow when it dies. It bounces up and down a few times, then it spawns that actual
throwable arrow.
**************************************************************************************************************** */
public class PRP_BouncyArrow : MonoBehaviour {
    
	[SerializeField]
	private GameObject		ThrowableArrow;

	[SerializeField]
	private GameObject		DeathParticles;

	private Rigidbody		mRigid;

	private bool 			mAllowDeath = false;

	private void Awake(){
		mRigid = GetComponent<Rigidbody>();
	}
	
	// on start, we make it roughly at the ground.
    private void Start(){

        Vector3 rot = transform.rotation.eulerAngles;
        rot.x = 90f;
        transform.rotation = Quaternion.Euler(rot);

        Vector3 pos = transform.position;

        int layerMask = 1<<LayerMask.NameToLayer("LevelGeometry") | 1<<LayerMask.NameToLayer("FLOOR");
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(downRay, out hit, Mathf.Infinity, layerMask)){
            pos.y -= hit.distance;
            pos.y += 2f;
        }

        transform.position = pos;

		mRigid.velocity = new Vector3(0f, 20f, 0f);

		// just in case we don't trigger this.
		Invoke("AllowDeath", 0.5f);
		Invoke("Die", 2.5f);
    }

	private void Update(){
		if(!mAllowDeath) return;

		int layerMask = 1<<LayerMask.NameToLayer("LevelGeometry");
		layerMask |= 1<<LayerMask.NameToLayer("FLOOR");		
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(downRay, out hit, Mathf.Infinity, layerMask)){
            if(hit.distance < 2.5f){
				Die();
			}
        }
	}

	public void AllowDeath(){
		mAllowDeath = true;
	}

	public void Die(){
		AUD_Manager.PostEvent("AM_ShieldBarrierImpact_ST", gameObject);
		Instantiate(ThrowableArrow, transform.position, transform.rotation);
		Instantiate(DeathParticles, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
