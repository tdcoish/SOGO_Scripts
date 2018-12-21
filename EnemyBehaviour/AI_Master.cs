using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/*******************************************************************************
AI_Master is the controller for the NPC's. This stores references to all the current
NPC's, and fishes through those references to find the events/behaviours that we
care about.
NPC's add themselves to this list upon Awake(). This is necessary because the 
list will grow/shrink based upon Entity spawns/deaths. 

AI_Master is responsible for telling which entities to update themselves and how.
******************************************************************************* */

// Make this class now spawn enemies.
public class AI_Master : MonoBehaviour {

    [SerializeField]
    private FloatVariable           mMaxSpawned;

    // NPC's add themselves to this list. This might not be the best way of doing this.
    public List<AI_Controller>      mNPCList;
    // Also need a list of NPC events.
    [HideInInspector]
    public List<AI_Controller>      mEnemiesTryingToFire;

    public bool                     mPincerNow;

    [HideInInspector]
    public AI_Pathfinder            mPather;

    private LVL_Objective           mObjective;

    private bool                    mDoObj;

    [SerializeField]
    private GameEvent               mWeaponFired;

    [Header("Space shots out between enemies")]
    [SerializeField]
    private FloatVariable           mShotSpacing;
    private float                   mLastFireTimeStamp;

    [SerializeField]
    private IntegerVariable           mMaxNumberFiring;

    [SerializeField]
    private GameEvent           mGruntDied;

    [SerializeField]
    private GameEvent           mLankyDied;

    [SerializeField]
    private GameEvent           mTankDied;

    private int                 mIndexCanGenPath = 0;
    private int                 mCountEnemiesGenPath = 0;


    private void Awake(){
        mPather = GetComponent<AI_Pathfinder>();

        mObjective = FindObjectOfType<LVL_Objective>();
        mEnemiesTryingToFire = new List<AI_Controller>();
    }

    private void RunEnemyUpdates(){
        // Clear the list of enemies trying to fire.
        mEnemiesTryingToFire.Clear();
        
        // Iterate through our NPC's, getting them to perform their updates.
        for(int i=0; i<mNPCList.Count; i++){
            
            if(mIndexCanGenPath == i){
                mNPCList[i].RunUpdate(true);
            }else{
                mNPCList[i].RunUpdate(false);                
            }
        }
        mIndexCanGenPath++;
        if(mIndexCanGenPath > mNPCList.Count) 
            mIndexCanGenPath = 0;
        mCountEnemiesGenPath = 0;

        // Here, we've had our list of enemies that want to fire added to. Now we pick the ones that have fired least recently, and priveledge them to fire.
        LetSomeEnemiesFire();

        // Debug.Log("Memory on heap: " + )Profiler.
    }

    public void HandleDeadNPC(AI_Controller npc){
        mNPCList.Remove(npc);

        // here we also need to spawn the event, with the location of their death.
        if(npc.GetComponent<GRT_Behaviour>()){
            mGruntDied.Raise(npc.transform.position);
        }
        if(npc.GetComponent<LNK_Behaviour>()){
            mLankyDied.Raise(npc.transform.position);
        }
        if(npc.GetComponent<TNK_Behaviour>()){
            mTankDied.Raise(npc.transform.position);
        }
    }


    private void FixedUpdate(){

        RunEnemyUpdates();
    }

    public bool CanFireNow(){
        if(Time.time - mLastFireTimeStamp > mShotSpacing.Value){
            return true;
        }
        return false;
    }
    
    public void HandleEnemyFired(){
        mLastFireTimeStamp = Time.time;
    }

    // Here we add ourselves to the list of enemies that want to fire.
    public void ThisEnemyWantsToFire(AI_Controller enemy){
        if(mEnemiesTryingToFire.Contains(enemy)){
            return;
        }
        mEnemiesTryingToFire.Add(enemy);
    }

    // Turn them into a sorted array with the ones that have fired least recently at 0 -> end.
    private void LetSomeEnemiesFire(){

        int numThatShouldFire = mMaxNumberFiring.Value;
        numThatShouldFire += (mNPCList.Count - mMaxNumberFiring.Value) / 2;

        for(int j=0; j<numThatShouldFire; j++){

            int index = -1;
            float closestTime = 0f;
            for(int i=0; i<mEnemiesTryingToFire.Count; i++){
                float tmSinceFiring = Time.time - mEnemiesTryingToFire[i].mGun.mFireTimeStamp;
                if(tmSinceFiring > closestTime){
                    closestTime = tmSinceFiring;
                    index = i;
                }
            }

            // now let that enemy fire.
            if(index != -1){
                mEnemiesTryingToFire[index].MasterLetsMeFire();
                mEnemiesTryingToFire.RemoveAt(index);
            } 
        }
    }

    // Called by bullets when the enemy that shot us is dead.
    public Transform GetClosestEnemy(Vector3 pos){
        // returns nearest enemy
        // if no enemies alive, then returns null.
        float dis = 10000000f;
        int index = -1;
        for(int i=0; i<mNPCList.Count; i++){
            float tempDis = Vector3.Distance(pos, mNPCList[i].transform.position);
            if(tempDis < dis){
                dis = tempDis;
                index = i;
            }
        }

        if(index == -1){
            return null;
        }

        return mNPCList[index].transform;
    }

}
