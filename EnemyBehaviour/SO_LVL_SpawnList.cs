using UnityEngine;

// Just the NPC's to spawn.
[CreateAssetMenu(fileName="LVL_SpawnList", menuName="SunsOutGunsOut/LVL/EnemyList")]
public class SO_LVL_SpawnList : ScriptableObject {

	public GameObject			mRedGrunt;
	public GameObject			mBlueGrunt;
	public GameObject			mRedLanky;
	public GameObject			mBlueLanky;
	public GameObject			mRedTank;
	public GameObject			mBlueTank;
}
