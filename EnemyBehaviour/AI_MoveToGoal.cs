using System.Collections.Generic;
using UnityEngine;

public class AI_MoveToGoal : MonoBehaviour {

	// Generated whenever we do pathfinding.
    public List<int>               	mStoredPath;
	
    [HideInInspector]
    public AI_WallCheck         	mWallChecker;

	// Empty game object attached to us that we use to keep ourselves grounded. Placed at feet.
    [HideInInspector]
    public Grounded            mGrounder;
    private AI_FeetCaster       mFootCaster;

    [HideInInspector]
    public bool                 mCanGenPathThisFrame = true;

    [HideInInspector]
    public bool                 mCanUsePath = true;

    private AI_Controller       mEntity;

	private void Awake(){
        mStoredPath = new List<int>();
        mStoredPath.Clear();

        mEntity = GetComponent<AI_Controller>();
        mWallChecker = GetComponent<AI_WallCheck>();
        mGrounder = GetComponentInChildren<Grounded>();
        mFootCaster = GetComponentInChildren<AI_FeetCaster>();
	}

    /**********************************************************************************************
    We have already found the goal destination. We are now deciding whether or not to use A* or 
    Force based movement to get there. If A*, then we set the target to the next node. If Force, then 
    the target is the goal.
    ********************************************************************************************** */
    public void MoveToGoal(){
        // Kind of a hack, but if our state has changed, then we don't do this.
        if(mEntity.mState != mEntity.mStateChange) return;
        
        // If the level geometry blocks, then move to a different 
        bool usingPath = false;
        if(mEntity.mMaster.mPather.isActive && mCanUsePath){
            if(mWallChecker.IsWallBlockingWide(mFootCaster.transform.position, mEntity.mGoalPos)){
                usingPath = true;
                if(mCanGenPathThisFrame){
                    GenerateNodePath();
                }
                if(mStoredPath.Count == 0) {
                    mEntity.mPosTarget = mEntity.mGoalPos;
                }else{
                    mEntity.mPosTarget = mEntity.mMaster.mPather.GetNodeFromIndex(mStoredPath[0]).transform.position;
                }
            }
        }
        if(!usingPath){
            mEntity.mPosTarget = mEntity.mGoalPos;
        }

        MoveToTarget(mEntity.mPosTarget);
    }

    /*************************************************************************************************************************
    The rules for generating a new path should be fairly simple. If we can make it to the second node in the list, then drop 
    the first node, and just use the second. The definition of making it to the second node is, for now, just whether or not we 
    can see the second node. 
    A second rule for generating a new node is whether or not the last node in the list can see the player. If not 
    *********************************************************************************************************************** */

    // If we don't have a path, make one. Every time we stop using A*, we clear the path.
    private void GenerateNodePath(){
        mStoredPath.Clear();

        // Have to make sure that we can actually get to the starting node that we pick. Easy way is raycasting.
        // What we could do is sort the entire list by positions.
        bool success = true;
        int stIndex = mEntity.mMaster.mPather.FindNearestVisibleNode(mGrounder.transform.position);
        if(stIndex == -1){
            Debug.Log("No valid start");
            success = false;
        } 
        int glIndex = mEntity.mMaster.mPather.FindNearestVisibleNode(mEntity.mGoalPos);
        if(glIndex == -1) {
            Debug.Log("No valid goal");
            success = false;
        }

        if(success){
            mStoredPath = mEntity.mMaster.mPather.GenerateNodePath(stIndex, glIndex, out success);
        }
        if(!success){
            if(mStoredPath.Count <= 0){
                mStoredPath.Clear();
                mStoredPath.Add(mEntity.mMaster.mPather.FindNearestVisibleNode(transform.position));
            }
            return;
        }

        // now we cull the beginning nodes until we reach one that we find one we can't get to.
        while(true){
            if(mStoredPath.Count < 2){
                return;
            }
            // This line is never true. I can't figure out why.
            if(!mWallChecker.IsWallBetween(mEntity.mMaster.mPather.GetNodeFromIndex(mStoredPath[1]).transform.position, mFootCaster.transform.position)){
                mStoredPath.RemoveAt(0);
            }else{
                return;
            }
        }
    }

    /**************************************************************************************************
    Moves to a target directly. This target could be a pathfinding node, the player, or anything else.
    If we are within a very small range to the target, the velocity given becomes smaller and smaller.

    We have a number of different forces. We get those forces to apply themselves to our final velocity, 
    with normalization done at the very end to get our heading. We then multiply by max speed.
    ************************************************************************************************* */
    public void MoveToTarget(Vector3 target){

        // rBody.velocity = mMover.SetVelocity(target, others, mBase.mMaxSpd);
        mEntity.rBody.velocity = mEntity.mMover.SetVelocity(target, mEntity.mCurMaxVel);

        // keep feet on ground - have to put grounder a bit above feet.
        if(!mGrounder.IsGrounded()){
            Vector3 pos = transform.position;
            pos.y -=  mGrounder.DisFromGround(pos);
            pos.y += mGrounder.GetSetDis();
            transform.position = pos;
        }
        
    }
}
