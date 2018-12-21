using UnityEngine;

/***************************************************************************************************
This is a sub-type of level. Basically, each level is going to have a bunch of different sections. Those 
sections will themselves have 1 or more waves. They manage the waves that are spawned, and move to the next one 
when they are finished.

Oh boy this code makes me cry.
************************************************************************************************** */

public class LVL_Section : MonoBehaviour {

	[SerializeField]
	private LVL_Wave[]				mWaves;
	public int GetNumWaves() {return mWaves.Length;}

	[SerializeField]
	private int 					mActWave = 0;
	public int GetActiveWaveIndex(){return mActWave;}

	public bool						mPlayerHasHit = false;

	[HideInInspector]
	public bool					mSectionBeingRun = false;

	[SerializeField]
	public AI_Spawner[]				mSpawners;

	[SerializeField]
	private LVL_Wall[]				mWallsToDeactivateOnFinish;

	[SerializeField]
	private LVL_Wall[]				mWallsToActivateOnStart;

	[SerializeField]
	private GameObject				mZoneFX;

	[SerializeField]
	public GameEvent				OnSectionHit;
	[SerializeField]
	private GameEvent				OnSectionDone;

	[SerializeField]
	private bool					mTriggersMajorLevelChange = false;
	[SerializeField]
	private GameEvent				OnMajorSectionOver;

	private void Awake(){
		mZoneFX.gameObject.SetActive(false);
		for(int i=0; i<mSpawners.Length; i++){
			mSpawners[i].enabled = false;
		}
	}
	
	public LVL_Wave GetWave(){
		return mWaves[mActWave];
	}

	// Basically we just keep spawning them in until there are none left to spawn
	public void RunSection(){

		mSectionBeingRun = true;

		// The player has to hit our trigger in order for us to be able to run.
		if(!mPlayerHasHit){
			return;
		}

		// run the active wave
		mWaves[mActWave].RunWave();

	}

	public void HandleWaveFullySpawned(){
		for(int i=0; i<mSpawners.Length; i++){
			mSpawners[i].enabled = false;
		}
	}

	public void HandleWaveFinished(){
		if(mSectionBeingRun){
			// Check if we get a checkpoint. 
			if(mWaves[mActWave].mWave.mSpawnsCheckpoint){
				mWaves[mActWave].mWave.mHitCheckpoint.Raise(null);
			}

			mActWave++;
			if(mActWave == mWaves.Length){
				mSectionBeingRun = false;
				mZoneFX.gameObject.SetActive(false);
				DeactivateWallsOnFinish();
				if(mTriggersMajorLevelChange) OnMajorSectionOver.Raise(null);
				OnSectionDone.Raise(null);
			}else{
				ActivateWallsAndArrowAndSpawnersOnStart();
			}
		}
	}

	// called by the LVL_Manager when we start. Activates our arrow.
	public void Prep(){
		mZoneFX.gameObject.SetActive(true);
	}

	public void ActivateWallsAndArrowAndSpawnersOnStart(){
		for(int i=0; i<mWallsToActivateOnStart.Length; i++){
			mWallsToActivateOnStart[i].gameObject.SetActive(true);
		}
		// mZoneFX.gameObject.SetActive(false);
		for(int i=0; i<mSpawners.Length; i++){
			mSpawners[i].enabled = true;
		}
		for(int i=0; i< mWaves[mActWave].mWave.mEnemies.Length; i++){
			if(i >= mSpawners.Length) break;
			// Get size of wave, just do this for all the spawners in the wave.
			if(mSpawners[i].mHasSpawnedFX == false){
				mSpawners[i].PlaySpawnFX();
			}
		}
		mZoneFX.gameObject.SetActive(false);
	}

	public void DeactivateWallsOnFinish(){
		for(int i=0; i<mWallsToDeactivateOnFinish.Length; i++){
			mWallsToDeactivateOnFinish[i].gameObject.SetActive(false);
		}
	}

	public bool CanSpawnEnemy(){
		for(int i=0; i<mSpawners.Length; i++){
			if(mSpawners[i].IsReadyToSpawn()){
				return true;
			}
		}
		return false;
	}

	public GameObject SpawnEnemy(GameObject enemy){

		for(int i=0; i<mSpawners.Length; i++){
			if(mSpawners[i].IsReadyToSpawn()){
				var clone = mSpawners[i].SpawnEnemy(enemy);

				return clone;
			}
		}

		return null;
	}

	public int GetEnemiesLeft(){
		return mWaves[mActWave].mEnemies.Count;
	}

	/*****************************************************************************************
	Resets all waves past the wave that has been completed. For instance, if I have 15 waves, and I 
	get a checkpoint after wave 7, then I reset all the waves past there. The LVL_Manager is smart 
	enough to call ResetSection on all sections that are after us, although we will probably have gotten
	a checkpoint if we're there anyways.
	*************************************************************************************** */
	public void ResetSection(int wavesDone){
		for(int i=wavesDone; i<mWaves.Length; i++){
			mWaves[i].ResetWave();
		}

		mSectionBeingRun = false;
		if(wavesDone == 0){
			mPlayerHasHit = false;
			mZoneFX.gameObject.SetActive(false);
			for(int i=0; i<mSpawners.Length; i++){
				mSpawners[i].enabled = false;
			}
		}else{
			mZoneFX.gameObject.SetActive(true);
		}

		mActWave = wavesDone;

		// Might have to re-activate, de-activate walls.
	}
}
