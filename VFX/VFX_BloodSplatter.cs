using UnityEngine;
using UnityEngine.UI;

/****************************************************************************************
Just sets itself to the ground. If it's too far from the ground, then it instantly destroys
itself.
************************************************************************************* */
public class VFX_BloodSplatter : MonoBehaviour {

	[SerializeField]
	private FloatVariable 			mAmountAboveGround;
	[SerializeField]
	private FloatVariable			mMaxAmountAboveGround;

	private MeshRenderer			mRenderer;

	void Awake(){
		mRenderer = GetComponentInChildren<MeshRenderer>();
	}

	// Use this for initialization
	void Start () {

		transform.rotation = Quaternion.Euler(Vector3.right);

		Vector3 pos = transform.position;

        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(downRay, out hit, Mathf.Infinity)){
			// if the enemy is too far above the ground, then don't have a blood splatter.
			if(hit.distance > mMaxAmountAboveGround.Value){
				Destroy(gameObject);
				return;
			}
            pos.y -= hit.distance;
            pos.y += mAmountAboveGround.Value;
        }

        transform.position = pos;

		Destroy(gameObject, 2f);	
	}

	// we make our image slowly fade out.
	private void Update(){
		Color col = mRenderer.material.color;
		col.a -= Time.deltaTime * 0.5f;
		mRenderer.material.color = col;
	}
}
