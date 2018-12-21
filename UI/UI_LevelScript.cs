using UnityEngine;
using UnityEngine.UI;

public class UI_LevelScript : MonoBehaviour {

	[SerializeField]
	private Text		mEnemiesLeft;

	[SerializeField]
	private Text 		mMoveMessage;

	[SerializeField]
	private Text		mStartCountdown;

	[SerializeField]
	private Text 		mFinishCountdown;

	[SerializeField]
	private Text 		mWaveTimer;

	public void ShowWaveTimer(float tmLeft, bool display){
		SetVisibilityOfText(mWaveTimer, display);
		mWaveTimer.text = "Next Wave In: " + tmLeft;
	}

	public void ShowStartCountdown(float tmLeft){
		SetVisibilityOfText(mEnemiesLeft, false);
		SetVisibilityOfText(mMoveMessage, false);
		SetVisibilityOfText(mStartCountdown, true);
		SetVisibilityOfText(mFinishCountdown, false);

		mStartCountdown.text = "Starting In: " + (int)(tmLeft+1f);
	}

	public void ShowFinishCountdown(float tmLeft){
		SetVisibilityOfText(mEnemiesLeft, false);
		SetVisibilityOfText(mMoveMessage, false);
		SetVisibilityOfText(mStartCountdown, false);
		SetVisibilityOfText(mFinishCountdown, true);
		// maybe do this differently.
		mFinishCountdown.text = "You WON! " + (int)(tmLeft+1f);
	}

	public void DisplayEnemiesLeft(int numEnemies){
		
		SetVisibilityOfText(mEnemiesLeft, true);
		SetVisibilityOfText(mMoveMessage, false);
		SetVisibilityOfText(mStartCountdown, false);
		SetVisibilityOfText(mFinishCountdown, false);
		mEnemiesLeft.text = "Enemies Left: " + numEnemies;

	}

	public void DisplayMoveMessage(){
		SetVisibilityOfText(mEnemiesLeft, false);
		SetVisibilityOfText(mMoveMessage, true);
		SetVisibilityOfText(mStartCountdown, false);
		SetVisibilityOfText(mFinishCountdown, false);
		mMoveMessage.text = "Move to next area";
	}

	private void SetVisibilityOfText(Text txt, bool makeVisible){

		Color tempColor = txt.color;
		if(makeVisible){
			tempColor.a = 1f;
		}else{
			tempColor.a = 0f;
		}

		txt.color = tempColor;
	}

}
