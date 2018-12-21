using UnityEngine;

/***************************************************************************
I don't want the irritation of these spawners having to keep track of all their 
visual effects, so this listens for when the wave has stopped spawning, and destroy themselves.
Also 
************************************************************************* */
public class VFX_AlienSpawner : MonoBehaviour {

	// We get instantiated when OnSEctionHit event is raised.

	private void Awake(){
		transform.parent = null;
	}

	// have to detach the particles, so they die gracefully.
	public void OnWaveStoppedSpawning(){
		transform.parent = null;
	}
}
