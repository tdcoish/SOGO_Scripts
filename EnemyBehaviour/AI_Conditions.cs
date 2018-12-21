using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************************************
This is what we utilize for checking various conditions, used for the AI Utility calculations. For instance, we pass
in a reference to the player and to the us and we see if we can raycast between the two of us, if true, then in our
enemy we will store that we can in fact see the player. All the various AI subsytems should only need the information
stored in conditions.

For now it's just a basic data field, and is attached directly to the enemy prefab.
*************************************************************************************************************** */


// For right now the idea is that we're breaking this off to a child component, and shoving it into each enemy.
// However, I'm actually just shoving all this into the AI_Controller for now.
// Actually, Instead I'm shoving this into its own object and putting it in the level.
public class AI_Conditions : MonoBehaviour{

   // here we store all the info we need to make decisions.
    public bool         mCanSeePlayer;
    public bool         mFeetCanSeePlayer;
    public float        mDisToPlayer;
    public float        mTimeSinceSawPlayer;
    public float        mTimeSinceStoppedSeeingPlayer;
    public float        mDisToTarget;
    public bool         mInPlayersSights;           // use the rotation of the player to basically not shoot them in the back.
    public Vector3      mCenterOfMass;              // center of mass for enemies around us.
    public bool         mClearOfCover;              // can see player easily from cover, basically, do we have some space to either side
    public bool         mWithinGunRange;

    protected AI_Controller       mCont;
    protected WP_Gun                mGun;
    protected AI_EyeCaster          mEyeCaster;
    protected AI_FeetCaster         mFootCaster;

    private float       mSawPlayerTimeStamp;
    private float       mNotSawPlayerTimeStamp;

    private void Awake(){
        mCont = GetComponent<AI_Controller>();
        mGun = GetComponentInChildren<WP_Gun>();
        mEyeCaster = GetComponentInChildren<AI_EyeCaster>();
        mFootCaster = GetComponentInChildren<AI_FeetCaster>();
    }

    // Runs every check and stores the result.
    public void CheckAndStoreConditions(){
        
        mCanSeePlayer = CanSeePlayer(mCont.mPlayerTrans.Value.position, mEyeCaster.transform.position);
        mFeetCanSeePlayer = CanSeePlayer(mCont.mPlayerTrans.Value.position, transform.position);
        mTimeSinceSawPlayer = TimeSinceSawPlayer();
        mTimeSinceStoppedSeeingPlayer = TimeSinceStoppedSeeingPlayer();
        mDisToPlayer = Vector3.Distance(mCont.mPlayerTrans.Value.position, transform.position);
        mDisToTarget = DistanceToTarget();
        mInPlayersSights = InPlayersSights();
        mCenterOfMass = FindCenterOfMass();  
        mClearOfCover = CheckWellClearOfCover();
        mWithinGunRange = CheckWithinGunRange();
    }

    private bool CheckWithinGunRange(){
        float dis = Vector3.Distance(mCont.mPlayerTrans.Value.position, transform.position);
        if(mGun.mGunProperties == null){
            Debug.Log("Missing properties");
        }
        if(dis < mGun.mGunProperties.mRange){
            return true;
        }
        return false;
    }

    private bool InPlayersSights(){
        // basically use the dot product of the look vector + vector from the player to us.
        Vector3 dir = transform.position - mCont.mPlayerTrans.Value.position;
        dir = Vector3.Normalize(dir);

        Camera playerCam = mCont.mPlayersCam;
        Vector3 camLook = playerCam.gameObject.transform.forward;
        camLook = Vector3.Normalize(camLook);

        float dot = Vector3.Dot(dir, camLook);

        if(dot > 0.5f){
            return true;
        }

        return false;
    }

    private float DistanceToTarget(){
        return Vector3.Distance(transform.position, mCont.mPosTarget);
    }

    public float TimeSinceStoppedSeeingPlayer(){
        return Time.time - mNotSawPlayerTimeStamp;
    }

    public float TimeSinceSawPlayer(){
        return Time.time - mSawPlayerTimeStamp;
    }

    public bool DidAlliesFireAtMe(){
        // check a global controller to see if there was an event possibly?
        // alternatively, look through all the spawned objects, then calculate if one is coming to you.
        // Alternatively, just trigger an event when we take damage from anything, and check what type it is.
        return false;
    }

    public bool CoverBlockMeFromPlayer(Vector3 pos){
        // maybe raycast from behind the cover to the player.
        return CanSeePlayer(mCont.mPlayerTrans.Value.position, pos);
    }

    // Helper function, used for when we get onto the pathfinding
    private List<Vector3> GetClosestNodes(List<Vector3> copy, int numDesired){

        while(copy.Count > numDesired){
            // iterate through the list, getting the furthest away element.
            float dis = 0f;
            float testDis = 0f;
            int longestIndex = -1;
            for(int i=0; i<copy.Count; i++){
                testDis = Vector3.Distance(copy[i], transform.position);
                
                if(testDis > dis){
                    dis = testDis;
                    longestIndex = i;
                }
            }

            // purge this element.
            copy.RemoveAt(longestIndex);
        }

        return copy;
    }

    /************************************************************************************
    Benchmarked, about 10-13 us. Debug.DrawLine actually does not take any more time.
    Has side effect of setting time stamps for when we last saw the player/when we last 
    didn't see the player.

    Actually, since the entity has some width, we should be checking from the right and left
    of the object as well.
    ************************************************************************************/
    public bool CanSeePlayer(Vector3 playerPos, Vector3 enemyPos){

        // actually this gives us the direction from 2 -> 1
        Vector3 direction = (playerPos - enemyPos).normalized;

        // Note, can always add a layer mask here if needed.
        // care about floor, player, level objectives.
		int layerMask = 1<<LayerMask.NameToLayer("WALL") | 1<<LayerMask.NameToLayer("FLOOR");

        float dis = Vector3.Distance(playerPos, enemyPos);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(enemyPos, direction, out hit, dis, layerMask)){

            // Here make a linerenderer instead.
            Debug.DrawRay(enemyPos, direction * hit.distance, Color.yellow, 0.01f);

            // We hit the physics component, but the script is in the parent. Should probably be using tags.
            if(hit.transform.GetComponentInParent<PlayerController>() == null){
                mSawPlayerTimeStamp = Time.time;
                return false;
            }
        }

        mNotSawPlayerTimeStamp = Time.time;
        return true;

    }

    private Vector3 FindCenterOfMass(){

        // get all the entities within our grouping distance.
        float rad = 0;
        rad = GetComponent<AI_Mover>().GetProfile().mGrpDis;

        int num = 0;
        Vector3 centerPos = new Vector3();
        

        for(int i=0; i<mCont.mMaster.mNPCList.Count; i++){
            if(Vector3.Distance(mCont.mMaster.mNPCList[i].transform.position, transform.position) < rad){
                centerPos+=mCont.mMaster.mNPCList[i].transform.position;
                num++;
            }
        }

        if(num != 0){
            centerPos/=(float)num;
        }

        return centerPos;
    }

    // cast from spots to the right and left of the character to see if we can see behind cover.
    private bool CheckWellClearOfCover(){
        
        Vector3 leftDir = transform.right*-1f;
        Vector3 rightDir = transform.right;

        Vector3 leftRaySpot = transform.position;
        Vector3 rightRaySpot = transform.position;

        leftRaySpot += leftDir*5f;
        rightRaySpot += rightDir*5f;

        if(CanSeePlayer(mCont.mPlayerTrans.Value.position, leftRaySpot)){
            return false;
        }

        if(CanSeePlayer(mCont.mPlayerTrans.Value.position, rightRaySpot)){
            return false;
        }

        return true;
    }

}

