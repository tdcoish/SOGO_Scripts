using UnityEngine;

public class LVL_Checkpoint : MonoBehaviour {

	// you can't hit a checkpoint twice.
	private bool HasBeenHit;

	[SerializeField]
	private GameEvent		OnThisCheckpointHit;

	private void OnTriggerStay(Collider other){
		if(HasBeenHit) return;

		if(other.GetComponent<PlayerController>()){
			Debug.Log("Save Checkpoint");
			HasBeenHit = true;

			OnThisCheckpointHit.Raise(null);
		}
	}

}
