using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*****************************************************************************
Mover, uses flocking rules to figure out the correct velocity for an object.
**************************************************************************** */
public class AI_Mover : MonoBehaviour {

	[SerializeField]
    private SO_AI_Profile       mProfile;

    public SO_AI_Profile GetProfile(){return mProfile;}

	// Get rid of this ASAP
    private LVL_PushObj[]           mPushObjs;

    private AI_Master               mMaster;

	private void Awake(){
		mPushObjs = FindObjectsOfType<LVL_PushObj>();
        mMaster = GetComponent<AI_Controller>().mMaster;
	}

	public Vector3 SetVelocity(Vector3 target, float MAX_SPD){   
             
        // By now we know what we're moving towards
        Vector3 vel = new Vector3();
        
        // Start with heading directly to the target.
        Vector3 tgPush = target - transform.position;
        vel += Vector3.Normalize(tgPush) * mProfile.mTargetForceWeight;
        // Push entity away from objects in the level that push.
        Vector3 lvlPush = LvlObjPushForces();
        vel += lvlPush * mProfile.mLvlForceWeight;

        // Push away from other entities, if they're really close.
        Vector3 npcPush = NPCPushForces();
        vel += npcPush * mProfile.mSepForceWeight;

        // pull towards allies.
        Vector3 npcPull = NPCGroupForces();
        vel+=npcPull * mProfile.mGroupForceWeight;

        // Now normalize our velocity to get our heading, and multiply by maxSpd to get new vel
        vel = Vector3.Normalize(vel);
        vel *= MAX_SPD;

        // if they're really close, make them move slower and slower.
        float dis = Vector3.Distance(target, transform.position);
        if(dis < mProfile.mSlowDis){
            // normalize the distance to 0-1.
            float mult = dis/mProfile.mSlowDis;
            // square the decrease
            mult*=mult;
            vel *= mult;
        }
        
        return vel;
	}

	 // Now works on raw data. Not normalized.
    private Vector3 LvlObjPushForces(){
        Vector3 pushForce = new Vector3();
        for(int i=0; i<mPushObjs.Length; i++){
            if(mPushObjs[i] == null) continue;
            if(Vector3.Distance(transform.position, mPushObjs[i].transform.position) < 30f){
                // Debug.Log("Affected by: " + mObjs[i].gameObject);
                // Debug.DrawLine(mPushObjs[i].transform.position, transform.position, Color.green, 0.1f);
                // make it normalized, then fall off to next to nothing the further away the entity is.
                Vector3 strength = transform.position - mPushObjs[i].transform.position;
                strength.y = 0f;
                float dis = Vector3.Distance(transform.position, mPushObjs[i].transform.position);
                strength = Vector3.Normalize(strength);
                strength/=(dis*dis*dis);
                pushForce += strength;
            }
        }
        return pushForce;
    }

    // Precursor to flocking. Here they move away from each other when they get really close.
    private Vector3 NPCPushForces(){
        List<AI_Controller> superCloseNPCs = new List<AI_Controller>();
		for(int i=0; i<mMaster.mNPCList.Count; i++){
            float dis = Vector3.Distance(transform.position, mMaster.mNPCList[i].transform.position);
			if(dis < mProfile.mSepDis && dis > 0.1f){
				superCloseNPCs.Add(mMaster.mNPCList[i]);
			}
		}
		// Need to keep all, because we need to apply force from them constantly.
		// The closer they are, the exponentially more they are repulsed from each other.
		Vector3 sepForce = new Vector3();
		for(int i=0; i<superCloseNPCs.Count; i++){
			Vector3 strength = transform.position - superCloseNPCs[i].transform.position;
            strength.y = 0f;

            float dis = Vector3.Distance(transform.position, superCloseNPCs[i].transform.position);
            strength = Vector3.Normalize(strength);
            strength/=(dis*dis*dis);
            sepForce += strength;
		}
		return sepForce;
    }

    private Vector3 NPCGroupForces(){
        Vector3 groupForce = new Vector3();
        
        // get all entities within range 
        // Find center of mass of entities
        Vector3 center = new Vector3();
        int num = 0;

        for(int i=0; i<mMaster.mNPCList.Count; i++){
            float dis = Vector3.Distance(transform.position, mMaster.mNPCList[i].transform.position);
            if(dis < mProfile.mGrpDis && dis > 0.1f){
                center += mMaster.mNPCList[i].transform.position;
                num++;
            }
        }
        
        center/=(float)num;

        // What to do with that force? 
        // make it constant for now - make sure to check that it's not 0 when we receive this.
        groupForce = center - transform.position;
        return Vector3.Normalize(groupForce);
    }
}
