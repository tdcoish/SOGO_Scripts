using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************************************************
This script is attached to an AI. It basically checks if it can see it's target, or if there's a wall
in front. If there's a wall in front, then it will return false, which makes the AI pathfind using A*.
If not, then we just use forces.
************************************************************************************************** */
public class AI_WallCheck : MonoBehaviour {

	// Get the distance to our goal. If anything along the way is blocking (wall, floor), then pathfind.
	public bool IsWallBlocking(Vector3 goalPos){

		Vector3 direction = (goalPos - transform.position).normalized;

		int layerMask = 1<<LayerMask.NameToLayer("WALL");

		RaycastHit hit;

		float disToGoal = Vector3.Distance(goalPos, transform.position);
		if(Physics.Raycast(transform.position, direction, out hit, disToGoal, layerMask)){
			float disToWall = Vector3.Distance(transform.position, hit.point);
			Debug.DrawLine(transform.position, hit.point, Color.magenta, 0.1f);
			if(disToWall < disToGoal){
				return true;
			}
		}
		return false;
	}

	// Checks that we're well clear of any cover. Just two spots directly perpendicular to the destination on both sides.
	public bool IsWallBlockingWide(Vector3 castPos, Vector3 goalPos){

		Vector3 dirToPlayer = goalPos - castPos;
		dirToPlayer = Vector3.Normalize(dirToPlayer);
        
		Vector3 leftDir = Vector3.Cross(dirToPlayer, transform.up);
        Vector3 rightDir = Vector3.Cross(transform.up, dirToPlayer);

        Vector3 leftRaySpot = castPos;
        Vector3 rightRaySpot = castPos;

        leftRaySpot += leftDir*1f;
        rightRaySpot += rightDir*1f;

		if(IsWallBetween(leftRaySpot, goalPos)){
			return true;
		}
		if(IsWallBetween(rightRaySpot, goalPos)){
			return true;
		}
		if(IsWallBetween(castPos, goalPos)){
			return true;
		}

		return false;
	}

	public bool IsWallBetween(Vector3 pos, Vector3 goalPos){
		Vector3 direction = (goalPos - pos).normalized;

		int layerMask = (1<<LayerMask.NameToLayer("WALL"));
		// layerMask |= LayerMask.NameToLayer("LevelGeometry");
		// layerMask &= LayerMask.NameToLayer("INVISIBLE_WALL");

		RaycastHit hit;

		float disToGoal = Vector3.Distance(goalPos, pos);
		if(Physics.Raycast(pos, direction, out hit, disToGoal, layerMask)){
			float disToWall = Vector3.Distance(pos, hit.point);
			if(disToWall < disToGoal){
				// Debug.DrawLine(pos, hit.point, Color.green, 0.1f);
				return true;
			}
		}
		return false;
	}


}
