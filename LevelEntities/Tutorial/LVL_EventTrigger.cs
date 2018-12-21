using UnityEngine;

// Tag 
public class LVL_EventTrigger : MonoBehaviour {
	
    [SerializeField]
    private GameEvent       OnTriggerEntered;

    private void OnTriggerEnter(Collider other){
        if(other.GetComponent<PlayerController>()){
            OnTriggerEntered.Raise(null);
        }
    }
}