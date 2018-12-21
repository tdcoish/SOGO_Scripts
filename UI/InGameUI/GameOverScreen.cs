using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : UIScreen
{

    [SerializeField]
    private GameEvent OnCheckpointRestart;
    [SerializeField]
    private GameEvent OnLevelQuit;
    [SerializeField]
    private string mainMenuSceneName;

    protected override void UIScreenDisabled()
    {

    }

    protected override void UIScreenEnabled()
    {
        StartCoroutine(SetPreselectedObject());
    }

    public void OnRestartButtonClicked()
    {
        OnCheckpointRestart.Raise(null);
        UIManager.Instance.ShowScreen<GameScreen>();
    }

    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1f;
        OnLevelQuit.Raise(null);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
