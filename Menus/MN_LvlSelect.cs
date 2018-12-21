using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*************************************************************************************
This screen has a bunch of levels listed as strings, that correspond to the levels you 
can select.
************************************************************************************ */
public class MN_LvlSelect : MN_Screen
{

	[SerializeField]
	private SO_LVL_Checkpoint		LevelStartingCheckpoint;

	[SerializeField]
	private SO_LVL_LoadSectionEvents		SectionLoadEvents;

    protected override void MNScreenDisabled()
    {
    }

    protected override void MNScreenEnabled()
    {
    }

	private void Update(){
		base.Update();

		// now we also manually handle when a button is pressed.
		if(input.aButtonPressed){
			MN_CheckpointButton btn = mButtons[mActive].gameObject.GetComponent<MN_CheckpointButton>();
			if(btn != null){
				LevelStartingCheckpoint.mPlayerPos = btn.CheckpointToLoadFrom.mPlayerPos;
				LevelStartingCheckpoint.mPlayerRot = btn.CheckpointToLoadFrom.mPlayerRot;
				LevelStartingCheckpoint.mSectionInd = btn.CheckpointToLoadFrom.mSectionInd;
				LevelStartingCheckpoint.mWaveInd = btn.CheckpointToLoadFrom.mWaveInd;

				// We also need to correctly pass in the game event to shoot off once the level is started.
				SectionLoadEvents.indice = mButtons[mActive].GetComponent<MN_CheckpointButton>().EventIndice;

				Debug.Log("Pos: " + LevelStartingCheckpoint.mPlayerPos);

				Events.OnButtonPress.Raise(null);

				MN_Manager.Instance.mSceneToLoad = "LVL_BeachWorkingArena";
				// Note: recommended to use SceneManager.LoadSceneAsync(...), then maybe switch to a splash screen.
				MN_Manager.Instance.ShowScreen<MN_LoadScreen>();
			}
		}

		// If they press b, go back to the main menu.
		if(input.bButtonPressed){
			Events.OnBackScreen.Raise(null);
			MN_Manager.Instance.ShowScreen<MN_DiffSelect>();
		}
	}

}
