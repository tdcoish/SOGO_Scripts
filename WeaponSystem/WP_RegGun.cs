using UnityEngine;

/*************************************************************************************************
Our basic gun, all weapons inherit from this. 

Weapons must:
1) Charge up before the first shot comes out
2) After the first shot, fire at a certain rate. 
3) After the weapon has fired, it needs to rechamber -> this determines fire rate.
4) When we run out of ammunition, then we need to reload
5) If we stop firing for any reason, after some time we start idling again, and must be recharged.

Example: - all numbers made up for example
Sniper Rifle takes 1.2 s to charge up
Each consecutive shot takes 0.55 s to chamber and fire
After 4 rounds we need to take 3.1 s to reload
If we fired 2 shots, but then didn't fire for 1.5 seconds, then we need to charge up again.

All weapons will spawn a projectile, handled by Carlos. At this point the AI's no longer care at 
all about what happens, except to the extent that this triggers events. An example of an event would 
be the FatBoy projectile hitting friendly troops. In that case we need to spawn a global event for 
the AI's to read, but we do not need to continuously check the projectiles.

UPDATE:
Guns now just charge up and fire. That's it. Basically we've gotten rid of everything except chambering, 
which has been renamed Charging.
*********************************************************************************************** */

public enum WeaponState : int {
	IDLE,
	CHARGING
}

public class WP_RegGun : WP_Gun {

	[SerializeField]
    public WP_REG_Gun               mRegGunProperties;

    // These build as the gun attempts to fire, when this is larger than the tested time, change state
    private float         curChargeAmt;

    // isActivated is true whenever the enemy is trying to fire.
    private bool          isActivated;
    private bool          mPlayedChargeSnd;

    private WeaponState   state;
    
    [SerializeField]
    private Transform  firePoint;
    [HideInInspector]
    public Vector3       target;

    [HideInInspector]
    public Transform       victimTrans;

    protected void Update(){
        ManageStates();

        if((curChargeAmt / mRegGunProperties.mMaxChargeTime) >= 0.7){
            mChargeParticles.Activate();
        }else{
            mChargeParticles.Deactivate();
        }
    }

    /************************************************************************************************
    Each individual weapon will define how it fires its projectile.
    By the time this has been called we are sure that we are firing the weapon. We have already checked 
    that we are loaded, charged, and nothing else is preventing us from firing. 

    Great opportunity to use Carlos's Event system.
    ************************************************************************************************/
    protected virtual void FireProjectile(Transform trans){

        mFireTimeStamp = Time.time;
        
        // our plasma ray gun is as basic as it gets, spawning a plasmoid and then trying to re-chamber.
        // The target has already been set, and the projectile now simply has to have force added in the direction
        Vector3 dir = Vector3.Normalize(trans.position - transform.position);
        // GameObject clone = PhotonNetwork.Instantiate(mGunProperties.bullet.name, transform.position, Quaternion.LookRotation(dir, Vector3.up));
        dir = CalculateRandomError(dir);
        GameObject clone = Instantiate(mRegGunProperties.bullet, firePoint.position, Quaternion.LookRotation(dir, Vector3.up));
        AI_Controller owner = UT_FindComponent.FindComponent<AI_Controller>(gameObject);
        if(owner!=null){
            clone.GetComponent<Bullet>().SetOwner(owner.transform);
        }else{
            clone.GetComponent<Bullet>().SetOwner(transform);
        }
        clone.GetComponent<Bullet>().SetTarget(trans);

        // Also, got to create those particles for the muzzle blast
        if (mGunProperties.mMuzzleBlast){
            var mBlast = Instantiate(mGunProperties.mMuzzleBlast, firePoint.transform.position, transform.rotation);
        }

        for(int i=0; i<mGunProperties.mSwitchGroups.Length; i++){
            AUD_Manager.SetSwitch(mGunProperties.mSwitchGroups[i], mGunProperties.mSwitchStates[i], gameObject);
        }
        AUD_Manager.PostEvent(mGunProperties.mAudFireEvent, gameObject);

        // play the dialogue for our character shooting.
        AUD_Manager.DynamicDialogueFromData(owner.GetBase().mShootLine, gameObject);

        // Now push the event that a weapon has fired.
        owner.GetBase().mEnemyFired.Raise(null);
    }

    private Vector3 CalculateRandomError(Vector3 dir){
        AI_Controller owner = UT_FindComponent.FindComponent<AI_Controller>(gameObject); 
        if(owner == null) return dir;

        SO_AI_Base bs = owner.GetBase();
        if(!bs.mFireInaccurately){
            return dir;
        }

        // now find a random spread up to the max spread.
        float spread = Random.Range(0, bs.mInacRange);
        if(Random.value > 0.5f){
            spread *= -1;
        }

        // now apply that spread to the bullets direction.
        Vector3 newDir = Quaternion.AngleAxis(spread, Vector3.up) * dir;
        return newDir;
    }

    public void ManageStates(){
        // If we're not firing, then go idle.
        if(!isActivated){
            state = WeaponState.IDLE;
        } 

        switch(state){
            case WeaponState.IDLE: HandleIdleState(); break;
            case WeaponState.CHARGING: HandleChargingState() ;break;
            default: Debug.Log("Some weird state"); break;
        }

        // Set true every frame that they are trying to fire.
        isActivated = false;
    }

    private void HandleIdleState(){
        // curChargeAmt = 0f;
        curChargeAmt -= Time.deltaTime;
        if(curChargeAmt < 0f) curChargeAmt = 0f;

    }

    private void HandleChargingState(){
        curChargeAmt += Time.deltaTime;

        if(curChargeAmt > mRegGunProperties.mMaxChargeTime*0.7f){
            if(!mPlayedChargeSnd){
                mPlayedChargeSnd=true;
                // AUD_Manager.PostEvent(mGunProperties.mAudPreFireEvent, gameObject);
                Invoke("RefreshChargedEvent", 3f);
            }
        }

        if(curChargeAmt > mRegGunProperties.mMaxChargeTime){
            // should start firing.
            FireProjectile(victimTrans);
            curChargeAmt = 0f;
            state = WeaponState.CHARGING;
        }
    }

    private void RefreshChargedEvent(){
        mPlayedChargeSnd = false;
    }

    // This will be called by the NPC that owns this weapon
    public override void TryToFireGun(Transform victim){
        if(!UT_FindComponent.FindComponent<AI_Controller>(gameObject).GetBase().mCanFireGun){
            mFireTimeStamp = Time.time;
            return;
        }

        target = victim.position;
        victimTrans = victim;
        // target = victimPos;
        isActivated = true;
        // Can't idle any longer, but don't change state as opposed to that.
        if(state == WeaponState.IDLE){
            state = WeaponState.CHARGING; 
        }
    }

    // returns the charge from 0-1, 1 being fullest.
    public override float GetNormalizedCharge(){
        float val = curChargeAmt;
        val/=mRegGunProperties.mMaxChargeTime;

        return val;
    }

}
