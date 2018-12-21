using UnityEngine;

public class AI_Strafer : MonoBehaviour {

	private float 		mTimeStartedStrafe;

	private AI_Controller	mOwner;

	private void Awake(){

	}

	public bool NeedsNewStrafeSpot(){
		float timeSinceLast = Time.time - mTimeStartedStrafe;
        if(timeSinceLast < 1.5f){
            return false;
        }

		return true;
	}

	/*************************************************************************************************
    Strafes circularly around the player/goal.

    Alternate method. Make them choose totally randomly which direction to go in, but then store the time 
    they made that decision. Then, after a small amount of time, make them choose differently. Then I can
    just straight up use right/left.

	All this does is set the destination. It doesn't do anything else.
    *********************************************************************************************** */
	public Vector3 FindStrafeSpot(){

        mTimeStartedStrafe = Time.time;

        // Here we do in fact need the AI_Conditions, but only for figuring our strafing left or right.
        Vector3 dirToPlayer = GetComponent<AI_Controller>().mPlayerTrans.Value.position - transform.position;

        // Crossing straight up with the direction to the player will give us to the left.
        Vector3 strafeLeft = Vector3.Cross(transform.up, dirToPlayer);
        strafeLeft.y = 0;       // assume 2D plane.
        strafeLeft = Vector3.Normalize(strafeLeft);

        // eventually, store the time, then recalculate if the time is too stale
        bool left = (Random.value > 0.5f);

        Vector3 strafeDir = strafeLeft;
        if(!left){
            strafeDir *= -1;
        }

        Vector3 destSpot = transform.position + (strafeDir*3f);

        // This is our new goal. Since it updates every frame, there's no reason to worry that we'll be moving too far away.
		return destSpot;
	}
}
