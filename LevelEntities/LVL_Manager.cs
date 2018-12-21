using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManager{
	class SavedState{
		public int mSectionInd;
		public int mWaveInd;
		public Vector3			mPlayerPos;
		public Quaternion		mPlayerRot;
	}

	enum LEVEL_STATE : int{
		COUNTDOWN_TO_START,
		RUNNING,
		COUNTDOWN_TO_OUTRO
	}
}

/*******************************************************************************************
Levels consist of sections, at least for now. And the level needs to keep track of which sections
are active at any one time. Also, the triggers to make them active and the walls to contain 
everything.
*******************************************************************************************/
public class LVL_Manager : MonoBehaviour {

	[SerializeField]
	private LVL_Section[]		mSections;

	[SerializeField]
	private BooleanVariable		mDisableInput;

	[SerializeField]
	private int 				mActSection = 0;

	[SerializeField]
	private float 				mStartCountdown = 3f;
	private float				mLevelQuitCountdown = 5f;

	// checkpoint system. Has GameEventListener which calls SaveCheckpoint
	private LevelManager.SavedState		mCheckpointSave;

	private PlayerController		mPlayer;

	[SerializeField]
	private GameEvent		OnTutorialStarted;
	[SerializeField]
	private GameEvent			OnLevelStartCountdown;
	[SerializeField]
	private GameEvent			OnLevelActivated;		// after the cooldown.
	[SerializeField]
	private GameEvent			OnAllSectionsBeaten;		// after you win, before you get booted to main.

	[SerializeField]
	private VectorVariable		ActiveSectionPosition;
	[SerializeField]
	private IntegerVariable		NumEnemiesLeft;

	[SerializeField]
	private GameEvent			OnCheckpointRestarted;
	[SerializeField]
	private SO_LVL_Checkpoint		SavedCheckpoint;

	[SerializeField]
	private GameObject			mFinalSlide;
	
	// passed in based upon how 
	[SerializeField]
	private SO_LVL_LoadSectionEvents			LevelSectionEvents;

	private LevelManager.LEVEL_STATE 		mState = LevelManager.LEVEL_STATE.COUNTDOWN_TO_START;


	private void Start(){
		mSections[0].Prep();
		mCheckpointSave = new LevelManager.SavedState();
		mPlayer = FindObjectOfType<PlayerController>();

		// Set a checkpoint right away.
		SaveCheckpoint();

		OnLevelStartCountdown.Raise(null);

		LoadCheckpointFromData(SavedCheckpoint);

		// Pop off the passed in level event here.
		LevelSectionEvents.TriggerActiveEvent();
	}

	private void Update(){
		// Just after we're loaded, we need to countdown until we've actually started.
		// gotta update the UI stuffs.

		switch(mState){
			case LevelManager.LEVEL_STATE.COUNTDOWN_TO_START: RunIntroLogic(); break;
			case LevelManager.LEVEL_STATE.RUNNING: RunRunningLogic(); break;
			case LevelManager.LEVEL_STATE.COUNTDOWN_TO_OUTRO: RunOutroLogic(); break;
		}

	}

	private void RunIntroLogic(){
		mDisableInput.Value = true;
		mStartCountdown -= Time.deltaTime;
		if(mStartCountdown <= 0f){
			OnLevelActivated.Raise(null);
		}
	}

	private void RunRunningLogic(){

		if(mActSection >= mSections.Length) return;

		mSections[mActSection].RunSection();

		// previous code can have side effect, so we gotta deal with this here.
		if(mActSection >= mSections.Length){
			NumEnemiesLeft.Value = 0;
			return;
		}
		NumEnemiesLeft.Value = mSections[mActSection].GetEnemiesLeft();

	}

	private void RunOutroLogic(){
		mLevelQuitCountdown-=Time.deltaTime;
		//mUI.ShowFinishCountdown(mLevelQuitCountdown);
		if(mLevelQuitCountdown <= 0f){
			SceneManager.LoadScene("MN_TestMain");
		}
	}

	public void HandleLevelActivated(){
		mDisableInput.Value = false;
		mState = LevelManager.LEVEL_STATE.RUNNING;
	}

	// Now sections spawn events when they are completed.
	public void HandleSectionCompleted(){
		Debug.Log("Section complete");
		mActSection++;
		if(mActSection >= mSections.Length){
			OnAllSectionsBeaten.Raise(null);
		}else{
			mSections[mActSection].Prep();
			LVL_SectionTrigger trig = mSections[mActSection].GetComponentInChildren<LVL_SectionTrigger>();
			ActiveSectionPosition.Value = trig.transform.position;
		}
	}

	public void HandleAllSectionsBeaten(){
		mFinalSlide.gameObject.SetActive(true);
	}
	public void HandleLevelBeaten(){
		mState = LevelManager.LEVEL_STATE.COUNTDOWN_TO_OUTRO;
	}

	// When this is called, we save the section we're on, and the wave we're on.
	public void SaveCheckpoint(){
		mCheckpointSave.mPlayerPos = mPlayer.transform.position;
		mCheckpointSave.mPlayerRot = mPlayer.transform.rotation;

		// Since it's the wave that just completed that raises the checkpoint, we gotta go to the next one.
		if(mSections[mActSection].GetActiveWaveIndex()+1 >= mSections[mActSection].GetNumWaves() && mSections[mActSection].mPlayerHasHit){
			mCheckpointSave.mSectionInd = mActSection+1;
			mCheckpointSave.mWaveInd = 0;
		}else{
			mCheckpointSave.mSectionInd = mActSection;
			// mCheckpointSave.mWaveInd = mSections[mActSection].GetActiveWaveIndex() + 1;
			mCheckpointSave.mWaveInd = mSections[mActSection].GetActiveWaveIndex();
		}
	}

	public void LoadCheckpoint(){
		// reset all sections past mSectionInd with 0
		// reset mSectionInd with mWaveInd
		for(int i=mCheckpointSave.mSectionInd+1; i<mSections.Length; i++){
			mSections[i].ResetSection(0);
		}
		// mSections[mCheckpointSave.mSectionInd+1].ResetSection(0);
		mSections[mCheckpointSave.mSectionInd].ResetSection(mCheckpointSave.mWaveInd);
		mSections[mCheckpointSave.mSectionInd].Prep();
		LVL_SectionTrigger trig = mSections[mCheckpointSave.mSectionInd].GetComponentInChildren<LVL_SectionTrigger>();
		ActiveSectionPosition.Value = trig.transform.position;

		// Need to also reset the player, health and position.
		mPlayer.MovePlayerTo(mCheckpointSave.mPlayerPos, mCheckpointSave.mPlayerRot);

		mActSection = mCheckpointSave.mSectionInd;

		// need to re-activate/de-activate all the walls that need to be up. Maybe we can just activate all walls, then deactivate the ones in 
		// deactivate on finish?
		if(mCheckpointSave.mSectionInd <= 0) return;
		mSections[mCheckpointSave.mSectionInd-1].DeactivateWallsOnFinish();
	}

	private void LoadCheckpointFromData(SO_LVL_Checkpoint data){

		for(int i=0; i<mSections.Length; i++){
			mSections[i].ResetSection(0);
		}

		if(data.mSectionInd!=0){
			mSections[data.mSectionInd-1].DeactivateWallsOnFinish();
			// such a goddamn hack.
			if(data.mSectionInd > 11){
				for(int i=0; i<11; i++){
					mSections[i].DeactivateWallsOnFinish();
				}
			}
		}

		// mSections[mCheckpointSave.mSectionInd+1].ResetSection(0);
		mSections[data.mSectionInd].Prep();
		LVL_SectionTrigger trig = mSections[data.mSectionInd].GetComponentInChildren<LVL_SectionTrigger>();
		ActiveSectionPosition.Value = trig.transform.position;

		// Need to also reset the player, health and position.
		mPlayer.MovePlayerTo(data.mPlayerPos, mPlayer.transform.rotation);

		mActSection = data.mSectionInd;
		SaveCheckpoint();
		OnCheckpointRestarted.Raise(null);

		// mActSection = data.mSectionInd;		

	}

}
