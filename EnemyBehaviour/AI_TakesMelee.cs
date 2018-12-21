using UnityEngine;

// You can get hit with a melee attack.
[RequireComponent(typeof(AI_Controller))]
public class AI_TakesMelee : MonoBehaviour {

	[SerializeField]
	private FloatVariable 			mMeleeForce;

	[SerializeField]
	private GameObject				hitParticles;

	private AI_Controller owner;

	private void Awake(){
		owner = UT_FindComponent.FindComponent<AI_Controller>(gameObject);
	}

	public virtual void GetHitByMelee(){
		AUD_Manager.PostEvent("EN_Hit_ST", gameObject);
		
		// here we check if we have a shield, and just return it if true.
		if(GetComponentInChildren<EnemyForceField>() != null){
			return;
		}

		Instantiate(hitParticles, transform.position, transform.rotation);
        owner.TakeDamage(50f);

		if(owner.GetHealth() > 0f && GetComponent<AI_FlyLogic>()!=null){
			owner.mStateChange = STATE.FLYING;
			// now add a force slightly up and away from the player.
			Vector3 dirFromPlayer = transform.position - owner.mPlayerTrans.Value.position;
			dirFromPlayer.y = 0f;
			// need a small percentage in the y dir though.
			float strength = Vector3.Magnitude(dirFromPlayer);
			dirFromPlayer.y = strength*0.3f;
			dirFromPlayer = Vector3.Normalize(dirFromPlayer); 
			// dirFromPlayer.y+=0.3f;		// gotta make them fly up.
			GetComponent<Rigidbody>().AddForce(dirFromPlayer*mMeleeForce.Value);
			GetComponent<AI_FlyLogic>().mTimeStartedFlying = Time.time;

			// we also gotta "cheat" a little bit by pushing them up a little bit, to ensure no early collisions with the ground.
			Vector3 newPos = transform.position;
			newPos.y += 0.5f;
			transform.position = newPos;

			AUD_Manager.PostEvent("PC_MeleeImpact_ST_LP", gameObject);
		} 
	}
}
