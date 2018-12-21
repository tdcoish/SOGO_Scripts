using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HACK_ReloadCheckpoint : MonoBehaviour {

	[SerializeField]
	private InputState	input;

	[SerializeField]
	private GameEvent mCheckpointRestarted;

	
	// Update is called once per frame
	void Update () {
		if(input.yButtonPressed){
			Debug.Log("Restart Checkpoint");

			// We could get a reference to the LVL_Manager, then build the player data into this. But maybe not.
			mCheckpointRestarted.Raise(null);
		}
	}
}
