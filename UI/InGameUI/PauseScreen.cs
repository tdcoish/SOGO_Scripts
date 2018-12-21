using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : UIScreen {

	[SerializeField]
	private BooleanVariable disableInput;
	[SerializeField]
	private GameEvent OnCheckpointRestart;
	[SerializeField]
	private GameEvent OnLevelQuit;
	[SerializeField]
	private string mainMenuSceneName;
	[SerializeField]
	private SO_AD_Dialogue closePauseMenuDialog;
	[SerializeField]
	private StringVariable		Player;

    protected override void UIScreenDisabled()
    {
		
    }

    protected override void UIScreenEnabled()
    {
		StartCoroutine(SetPreselectedObject());
		AUD_Manager.PostEvent("IG_UI_MenuOpen_ST", gameObject);
		AUD_Manager.SetState("PauseMenu", "PauseGame");
	}

	private void Update() {
		if (input.startButtonPressed || input.bButtonPressed) {
			OnResumeButtonClicked();
		}
	}

	public void OnResumeButtonClicked() {
		AUD_Manager.PlayerDialogue(closePauseMenuDialog, gameObject, Player.Value);
		UIManager.Instance.ShowScreen<GameScreen>();
		AUD_Manager.PostEvent("IG_UI_MenuClose_ST", gameObject);
		AUD_Manager.SetState("PauseMenu", "ResumeGame");
	}

	public void OnSettingsButtonClicked() {
		UIManager.Instance.ShowScreen<SettingsScreen>();
	}
	
	public void OnRestartButtonClicked() {
		OnCheckpointRestart.Raise(null);
		AUD_Manager.SetState("PauseMenu", "ResumeGame");		
		UIManager.Instance.ShowScreen<GameScreen>();
	}
	
	public void OnMenuButtonClicked() {
		Debug.Log("PauseScreen unpaused time");
		Time.timeScale = 1f;
		OnLevelQuit.Raise(null);
		SceneManager.LoadScene(mainMenuSceneName);			
	}
	
	public void OnWorkoutTipsButtonClicked() {
		UIManager.Instance.ShowScreen<WorkoutTipsScreen>();
	}
	
	public void OnControlsButtonClicked() {
		UIManager.Instance.ShowScreen<ControlsScreen>();
	}
}
