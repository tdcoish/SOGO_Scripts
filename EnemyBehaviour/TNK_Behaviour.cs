using System.Collections.Generic;
using UnityEngine;

public enum TANK_GOALS : int {

	ATTACK_FROM_RANGE,
	CHARGE_PLAYER
};


public class TNK_Behaviour : AI_Controller {

	private TNK_Conditions				mConResults;

	private TANK_GOALS			mCurGoal;

	private AI_ChargeLogic		mChargeLogic;
	private AI_GrenadedLogic	mGrenadedLogic;
	private AI_AttackFromRange	mAttackFromRangeLogic;
	private AN_Enemies			mAnim;

	private void Start(){
		mGrenadedLogic = GetComponent<AI_GrenadedLogic>();
		mConResults = GetComponent<TNK_Conditions>();
		mChargeLogic = GetComponent<AI_ChargeLogic>();
		mAttackFromRangeLogic = GetComponent<AI_AttackFromRange>();
		mAnim = UT_FindComponent.FindComponent<AN_Enemies>(gameObject);
	}

	public override void RunUpdate(bool canGenNodePath){
		mMoveToGoal.mCanGenPathThisFrame = canGenNodePath;

		SmallUpdate();

		switch (mState){

			case STATE.GRENADED: mGrenadedLogic.Run(); break;
			case STATE.COMBAT: RunCombatState(); break;
			case STATE.IDLE: RunIdleState(); break;
			case STATE.DEAD: RunDeadState(); break;
		}

		mAnim.UpdateAnimations();
    }

	public void RunIdleState(){ }
	public void RunDeadState() {}

	public void RunCombatState(){
		
        rBody.constraints = RigidbodyConstraints.None;

        // Sets target to either player or objective.
        // SetTarget(doObj, obj);

        mConResults.CheckAndStoreConditions();

		// Here we find out what our goal is, to attack from a distance, or to attack with a charge.
        mCurGoal = FindCombatGoal();

		// Here we perform that goal, attacking from a distance or attacking with a charge
		DoCombatGoal(mCurGoal);

		// goal is updated with side effects
		mMoveToGoal.MoveToGoal();	
    }

	// check which goal the tank should be going for.
	private TANK_GOALS FindCombatGoal(){

		TANK_GOALS gls;

		if(mChargeLogic.mIsCharging){
			gls = TANK_GOALS.CHARGE_PLAYER;
		}else{
			if(mConResults.mDisToPlayer < mBase.mChargeRange.Value && mChargeLogic.mCanCharge){
				gls = TANK_GOALS.CHARGE_PLAYER;
			}else{
				gls = TANK_GOALS.ATTACK_FROM_RANGE;
			}
		}

		return gls;
	}

	private void DoCombatGoal(TANK_GOALS goal){
		switch (goal){
			case TANK_GOALS.ATTACK_FROM_RANGE: mAttackFromRangeLogic.Run(); break;
			case TANK_GOALS.CHARGE_PLAYER: mChargeLogic.Run(); break;
			default: break;
		}
	}

}












