using UnityEngine;
using UnityEngine.UI;

public class UI_GrenadeHit : MonoBehaviour {

	[SerializeField]
	private Image		mSlimeRecover;

	private AI_Controller		mEntity;
	private AI_GrenadedLogic	mGrenLogic;
	
	private void Awake(){
		mEntity = UT_FindComponent.FindComponent<AI_Controller>(gameObject);
		mGrenLogic = UT_FindComponent.FindComponent<AI_GrenadedLogic>(gameObject);
	}

	private void Update(){
		mSlimeRecover.fillAmount = CalcStunLeft();
	}

	private float CalcStunLeft(){
        // after a certain amount of time, switch states back to combat
        float timePassed = Time.time - mGrenLogic.mTimeHitGrenade;

        float normTimeLeft = 1f-(timePassed/mEntity.GetBase().mTimeInGrenadeStun);
        if(normTimeLeft < 0f) normTimeLeft = 0f;
        return normTimeLeft;
    }
}
