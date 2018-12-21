using UnityEngine;


/*******************************************************************************************
Should be a positional sound, which means it needs to be attached to every "spaceship" spawner 
in the game.
***************************************************************************************** */
public class AD_EnemySpawn : MonoBehaviour {

	private string 		eName = "AM_GravListTeleport_ST";

	public void EnemySpawnLiftSound(){
		AUD_Manager.PostEvent(eName, gameObject);
	}
}
