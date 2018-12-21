using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************************
When an enemy is spawned, the hashtable stores a unique string->int key, along with a pointer
to the enemy. We can then just check that the pointer is still valid, and if not, the enemy has 
been destroyed.

Oh dear this is hideous code. Side effect central, plus constant checking for null pointers.
*******************************************************************************************/
public class LVL_Wave : MonoBehaviour {

	[SerializeField]
	public SO_LVL_Wave			mWave;

	[SerializeField]
	private LVL_Section		mMaster;

	private int 		mNumInWave = 0;
	private int 		mNumSpawned = 0;

	private bool					mFinishedSpawning;

	private int			mNumThatMustDie;
	private int			mNumThatHaveDied = 0;

	private bool		mSpawnedArrows = false;

	[HideInInspector]
	public float		mTimeLeft;

	// These are the unique hashes of the enemies made.
	[HideInInspector]
	public List<GameObject>			mEnemies;

	private void Awake(){

		mEnemies = new List<GameObject>();
		mNumInWave = mWave.mEnemies.Length;

		mNumThatMustDie = mNumInWave - mWave.mNumLeftAlive;

		mTimeLeft = mWave.mTime;
	}

	public void RunWave(){
		// while we haven't finished spawning the wave, try to spawn more.
		if(mNumSpawned < mNumInWave){
			if(mMaster.CanSpawnEnemy()){
				mEnemies.Add(mMaster.SpawnEnemy(mWave.mEnemies[mNumSpawned]));
				mNumSpawned++;
				if(mNumSpawned == mNumInWave){
					mWave.OnWaveFullySpawned.Raise(null);
					mFinishedSpawning = true;
				}
			}
		}

		CleanEnemiesList();
		HandleArrows();

		mTimeLeft -= Time.deltaTime;

		if(IsWaveDone()){
			Debug.Log("Wave Done");
			mWave.OnWaveDone.Raise(null);
		}
	}

	private void CleanEnemiesList(){
		for(int i=0; i<mEnemies.Count; i++){
			if(mEnemies[i] == null){
				mEnemies.RemoveAt(i);
				mNumThatHaveDied++;
			}
		}
	}

	private void HandleArrows(){
		if(!mWave.mSpawnsArrowsNearEnd) return;
		if(mSpawnedArrows) return;

		if(mNumThatHaveDied >= 1){
			if(mWave.mEnemies.Length - mNumThatHaveDied <= mWave.mNumAliveWhenArrowsSpawn){
				mSpawnedArrows = true;
				
				// now we spawn the arrows over the heads of the last x enemies.
				for(int i=0; i<mWave.mNumAliveWhenArrowsSpawn; i++){
					var clone = Instantiate(mWave.ArrowPrefab, mEnemies[i].transform.position, transform.rotation);
					clone.transform.SetParent(mEnemies[i].GetComponentInChildren<AI_Controller>().transform);
				}
			}
		}
	}

	private bool IsWaveDone(){
		if(mFinishedSpawning){
			if(mNumThatHaveDied == mNumThatMustDie){
				return true;
			}
		}

		// also check if the time has run out.
		if(mWave.mIsTimed){
			if(mTimeLeft <= 0f){
				return true;
			}
		}
		return false;
	}

	// ONLY CALL WHEN CHECKPOINT RESET.
	// Have side effect of killing all enemies that are still alive.
	public void ResetWave(){
		CleanEnemiesList();
		for(int i=0; i<mEnemies.Count; i++){
			AI_Controller mEnemyPointer = UT_FindComponent.FindComponent<AI_Controller>(mEnemies[i].gameObject);
			if(mEnemyPointer != null){
				mEnemyPointer.KillYourself();
			}
		}
		mEnemies.Clear();
		mNumSpawned = 0;
		mNumThatHaveDied = 0;
		mTimeLeft = mWave.mTime;
		mSpawnedArrows = false;
	}

}
