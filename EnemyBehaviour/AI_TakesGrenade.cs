using UnityEngine;

[RequireComponent(typeof(AI_Controller))]
public class AI_TakesGrenade : MonoBehaviour {

	private AI_Controller mOwner;
	private AI_GrenadedLogic	mGrenadedLogic;
	private StunnedParticles		mStunParticles;

	private string 		sType;

	private void Awake(){
		mOwner = GetComponent<AI_Controller>();
		mGrenadedLogic = GetComponent<AI_GrenadedLogic>();
		mStunParticles = GetComponentInChildren<StunnedParticles>();
		mStunParticles.Deactivate();

		switch(mOwner.GetBase().mType){
			case EnemyTypes.GRUNT: sType = "Grunt"; break;
			case EnemyTypes.LANKY: sType = "Lanky"; break;
			case EnemyTypes.TANK: sType = "Tank"; break;
		}
	}

	// of course, we actually need to play the specific whine for this particular enemy.
	public void GetHitByGrenade(){
		// here we check if we have a shield, and just return it if true.
		if(GetComponentInChildren<EnemyForceField>() != null){
			Debug.Log("Shield in way");
			return;
		}

        mGrenadedLogic.mTimeHitGrenade = Time.time;   
        mOwner.mStateChange = STATE.GRENADED;

		string[] args = new string[]{sType, "GrossOut"};
        AUD_Manager.DynamicDialogue("VO_Negatives_E", args, gameObject);
		mStunParticles.Activate();
    }
}
