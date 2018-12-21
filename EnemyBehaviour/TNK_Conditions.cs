using System.Collections.Generic;
using UnityEngine;

public class TNK_Conditions : AI_Conditions {

	public bool         mWithinChargeDistance;

	// Runs every check and stores the result.
    public void CheckAndStoreConditions(){

		base.CheckAndStoreConditions();
        
		mWithinChargeDistance = CheckWithinChargeDistance();
    }

	private bool CheckWithinChargeDistance(){
        if(mDisToTarget < (mCont.GetBase().mChargeRange.Value)){
            return true;
        }
        return false;
    }

}
