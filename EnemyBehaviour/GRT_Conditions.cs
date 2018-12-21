using System.Collections.Generic;
using UnityEngine;

/**************************************************************************************
Specific conditions for the grunts only.
************************************************************************************ */

public class GRT_Conditions : AI_Conditions {

    public bool mWithinFleeDistance;
    public int  mNumAlliesWithinRadius;

	// Runs every check and stores the result.
    public void CheckAndStoreConditions(){

		base.CheckAndStoreConditions();
        
        mWithinFleeDistance = CheckWithinFleeDistance();
        mNumAlliesWithinRadius = CheckAlliedWithinRadius();
    }

	private bool CheckWithinFleeDistance(){
        if(mDisToTarget < (mCont.GetBase().mFleeRange.Value)){
            return true;
        }
        return false;
    }

    private int CheckAlliedWithinRadius(){
        int num = 0;
        for(int i=0; i<mCont.mMaster.mNPCList.Count; i++){
            float dis = Vector3.Distance(transform.position, mCont.mMaster.mNPCList[i].transform.position);
            if(dis < mCont.GetBase().mFleeAlliesCalmingRange && dis > 0.3f)
                num++;
            
        }

        return num;
    }

}
