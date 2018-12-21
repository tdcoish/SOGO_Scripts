using UnityEngine;

public enum SCENARIO_OBJECTIVE : int {

	KILL_ALL,
	DEFEND_OBJ,
	COLLECT_ITEMS,
	DESTROY_TARGETS
}

// Object that stores the raw data for an individual wave. A LVL_SpawnMaster contains a bunch of these.
[CreateAssetMenu(fileName="LVL_Wave", menuName="SunsOutGunsOut/LVL/SpawnWave")]
public class SO_LVL_Wave : ScriptableObject {

	// Actually that was annoying, now you do actually specify which entities spawn in what order

	public GameObject[]		mEnemies;

	public GameObject 			ArrowPrefab;

	[Header("The number that can still be alive before the next wave spawns")]
	public int 				mNumLeftAlive = 0;

	public bool				mIsTimed = false;
	public float			mTime = 100f;

	public bool				mSpawnsCheckpoint = false;
	public GameEvent		mHitCheckpoint;
	public GameEvent		OnWaveDone;
	public GameEvent		OnWaveFullySpawned;

	public SCENARIO_OBJECTIVE		mObjective;

	public bool				mSpawnsArrowsNearEnd = true;
	public int 				mNumAliveWhenArrowsSpawn = 2;
}
