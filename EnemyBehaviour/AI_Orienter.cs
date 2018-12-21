using UnityEngine;

/*************************************************************************************************
Where should the AI be looking? Ultimately this will be a complicated system that requires heavy 
integration with animations. However, for now we just rotate the entire rigidbody in a certain direction. 
This won't have any impact on movement.

If you're firing at the player, then you should probably be looking at him.
If you can see the player, then you should probably be looking at him.
If you can see the player, but you are moving at an angle greater than 45* from him, and also running really hard, maybe look where you're going.
If you can't see the enemy, then don't even try to look at him.
If the player is not even close to within gun range, then don't look at him.

Planned ---
If you are fleeing from the player, look where you're going.
If you're captivated by the tanning lotion grenade, possibly look at that.
---
************************************************************************************************* */
public class AI_Orienter : MonoBehaviour{

    // Basically there are two options for now, look where you're going, or look at the player.
    // This function has no side effects. And should never have side effects. We simply return a quaternion that we think the enemy
    // should have as it's orientation. Nothing more.
    public Quaternion OrientModel(AI_Conditions conResults, Vector3 vel, Vector3 ourPos, Vector3 goalPos){

        Vector3 mvDir;
        Vector3 plyDir;

        // Get rid of any y rotation, for now.
        mvDir = vel;
        mvDir.y = 0;
        mvDir = Vector3.Normalize(mvDir);
        plyDir = goalPos - ourPos;
        plyDir.y = 0;
        plyDir = Vector3.Normalize(plyDir);

        Quaternion faceVel = Quaternion.LookRotation(mvDir);
        Quaternion facePly = Quaternion.LookRotation(plyDir);

        // If we can't see the player, face where we're going 
        if(!conResults.mCanSeePlayer){
            return faceVel;
        }

        if(conResults.mWithinGunRange){
            return facePly;
        }

        return faceVel;
    }

    // looks at a spot you pass in.
    public Quaternion OrientToSpot(Vector3 spot){
        Vector3 spotDir;

        spotDir = spot - transform.position;
        spotDir.y = 0;
        spotDir = Vector3.Normalize(spotDir);

        return Quaternion.LookRotation(spotDir);
    }

    public Quaternion OrientToDirection(Vector3 vel){
        vel.y = 0f;
        vel = Vector3.Normalize(vel);

        return Quaternion.LookRotation(vel);
    }
}