using UnityEngine;

// It just moves back and forth.
public class TU_Pedestal : MonoBehaviour {

	[SerializeField]
	private Vector3[] Destinations;
	int el = 0;

	[SerializeField]
	private float 		mSpd = 1f;
	private Rigidbody 	mRigid;
	
	private void Awake(){
		mRigid = GetComponent<Rigidbody>();
		MoveToDest();
	}

	// Move towards the destination.
	private void FixedUpdate(){
		float dis = Vector3.Distance(transform.position, Destinations[el]);
		if(dis < 0.3f){
			el++;
			if(el > 1){
				el = 0;
			} 
		}
		MoveToDest();
	}

	private void MoveToDest(){
		Vector3 dir = Destinations[el] - transform.position;
		dir = Vector3.Normalize(dir);
		mRigid.velocity = dir * mSpd;
	}
}
