using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : UIScreen
{

	[SerializeField]
	private BooleanVariable disableInput;
	[SerializeField]
	private GameEvent OnGameUnpaused;
	[SerializeField]
	private GameEvent OnGamePaused;
	[SerializeField]
	private SO_AD_Dialogue openPauseMenuDialog;
	[SerializeField]
	private PlayerState playerState;
	[SerializeField]
	private StringVariable		Player;

    protected override void UIScreenDisabled()
    {
		Time.timeScale = 0f;
		OnGamePaused.Raise(null);
		disableInput.Value = true;
    }

    protected override void UIScreenEnabled()
    {
		Time.timeScale = 1f;
		OnGameUnpaused.Raise(null);
		StartCoroutine(ActivateInput());
    }

	private void Update()
	{	
		if (input.startButtonPressed && playerState.isPlayerDead == false) {
			AUD_Manager.PlayerDialogue(openPauseMenuDialog, gameObject, Player.Value);
			UIManager.Instance.ShowScreen<PauseScreen>();
		}
	}

	public void ShowGameOverScreen(object data) {
		UIManager.Instance.ShowScreen<GameOverScreen>();
	}

	private IEnumerator ActivateInput() {
		yield return null;
		disableInput.Value = false;
		yield break;
	}

}
