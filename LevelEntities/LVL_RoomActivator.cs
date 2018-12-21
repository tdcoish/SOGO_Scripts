using UnityEngine;

/*******************************************************************************************
This script is mostly blank, although it is deactivated after being collided with once.
Currently, the enemies have a reference to one of these, and when it stops being active, they start activating.
***************************************************************************************** */
[RequireComponent(typeof(SphereCollider))]
public class LVL_RoomActivator : MonoBehaviour {

	public bool			mIsTriggered = false;

	private void OnTriggerEnter(Collider other)
    {
		// if(other.GetComponent<Fake_PlayerController>()){
		// 	mIsTriggered = true;

		// 	Debug.Log("Initializing some players");
		// }

		// Now we're using actual buff buddies.
		// TODO: Check network validation.
        if(other.GetComponent<PlayerController>()){
			mIsTriggered = true;
		}
    }
}
