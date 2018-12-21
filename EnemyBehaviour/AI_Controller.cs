using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

/**************************************************************************************************************************
Base class for AI entities. Also, does a few basic things.
************************************************************************************************************************** */

public enum STATE : int {
    INACTIVE,
    IDLE,
    GRENADED,
    COMBAT,
    FLYING,
    DEAD
}

[RequireComponent(typeof(Rigidbody))]
public class AI_Controller : MonoBehaviour, IDamageable{

    private float               mHealth;
    public float GetHealth(){return mHealth;}

    // Array of player Vectors representing positions. 
    [SerializeField] 
    public TransformVariable        mPlayerTrans;

    [HideInInspector]
    public Rigidbody                rBody;

    protected AI_Orienter             mOrienter;

    // Gets its own controller. Gonna eventually make this a global singleton
    [HideInInspector]
    public AI_Master               mMaster;

    public WP_Gun                   mGun;

    // Short term and long term desired destinations.
    public Vector3              mPosTarget;
    public Vector3              mGoalPos;
    
    // Script takes in our target and moves their for us applying forces.
    [HideInInspector]
    public AI_Mover            mMover;

    // This is the script that finds the target in the first place
    protected AI_MoveToGoal       mMoveToGoal;

    public STATE                mState;
    public STATE                mStateChange;       // gets read every frame, then has the state changed at the start of the next frame.

    [SerializeField]
    protected SO_AI_Base          mBase;
    public SO_AI_Base GetBase(){return mBase;}

    // Need camera, since we need to know if the character is looking at us, so we don't fire at him
    [HideInInspector]
    public Camera               mPlayersCam;

    [HideInInspector]
    public float                mCurMaxVel;

    // Our index in the list of enemies.
    [HideInInspector]
    public int                  mElInList;

    // Will be true if we're red.
    [SerializeField]
    public bool                 mRed = true;

    private void Awake(){

        mHealth = mBase.mMaxHealth;

        rBody = GetComponent<Rigidbody>();

        mGun = GetComponentInChildren<WP_Gun>();

        mMaster = FindObjectOfType<AI_Master>();
        mMaster.mNPCList.Add(this);

        mMover = GetComponent<AI_Mover>();
        
        mState = STATE.COMBAT;
        mStateChange = STATE.COMBAT;

        mOrienter = GetComponent<AI_Orienter>();

        mPlayersCam = Camera.main;

        mMoveToGoal = GetComponent<AI_MoveToGoal>();
    }

    public virtual void RunUpdate(bool canGenNodePath){}

    public void SmallUpdate(){
        mState = mStateChange;

        // ALso, we can still die.
        CheckAndHandleDeath();
    }

    public void TakeDamage(float damage)
    {
        if(GetComponentInChildren<EnemyForceField>()){
            return;
        }
        mHealth -= damage;

        // If we haven't died, play our damage sound
        if(mHealth > 0f){
            AUD_Manager.DynamicDialogueFromData(mBase.mGeneralDamage, gameObject);
        }
    }

    protected void CheckAndHandleDeath(){
        if(mHealth <= 0f && mState != STATE.DEAD){
            Die();
        }
        // manually build kill z here.
        if(transform.position.z > 1000f || transform.position.z < -100f){
            Debug.Log("Enemy Killed by kill Z");
            Die();
        }
    }

    [ContextMenu("Die")]
    private void Die(){
        // For now, not using object pooling. In the future, potentially just set GEtComponent<Renderer>().enabled = false;
        mState = STATE.DEAD;
        if(mRed){
            Instantiate(mBase.mRedDeathParticles, transform.position, transform.rotation);
        }else{
            Instantiate(mBase.mBlueDeathParticles, transform.position, transform.rotation);
        }
        if(mRed){
            Instantiate(mBase.mRedDeathMark, transform.position, transform.rotation);
        }else{
            Instantiate(mBase.mBlueDeathMark, transform.position, transform.rotation);
        }
        Instantiate(mBase.mDeathScream, transform.position, transform.rotation);

        mMaster.HandleDeadNPC(this);

        AUD_Manager.DynamicDialogueFromData(mBase.mDeathDialogue, gameObject);
        Destroy(transform.parent.gameObject);
    }

    public void KillYourself(){
        Die();
    }

    // Called by AI_Master
    public void MasterLetsMeFire(){
        mGun.TryToFireGun(mPlayerTrans.Value);
    }
}
