using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LVL_Tutorial : MonoBehaviour {

	// Just in case we get rumbling or something like that.
	[SerializeField]
	private GameEvent OnLevelQuit;
	
	public void OnTutorialOver(){
		OnLevelQuit.Raise(null);
		Invoke("TutorialOver", 0.1f);
	}

	private void TutorialOver(){
		Time.timeScale = 1f;			// WTF? WHY?		
		SceneManager.LoadScene("MN_TestMain");
	}

}
