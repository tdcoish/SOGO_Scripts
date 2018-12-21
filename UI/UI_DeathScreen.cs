using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Update, now the death screen just restarts you from the checkpoint after 2 seconds or so.
public class UI_DeathScreen : MonoBehaviour {

	[SerializeField]
	private Text 		tRespawn;

	[SerializeField]
	private Text 		tMainMenu; 

	[SerializeField]
	private TransformVariable 	mPlayerTrans;

	[SerializeField]
	private InputState		input;

	[SerializeField]
	private GameEvent		mCheckpointRestarted;

	[SerializeField]
	private float 			mRespawnEnableTime = 2.0f;

	[SerializeField]
	private float 			mRespawnTime;
	private float 			mRespawnCountdown;

	private bool 			mCanRespawn;

	private void Update(){

		mRespawnCountdown -= Time.deltaTime;

		// if(mRespawnCountdown <= 0f){
		// 	mCheckpointRestarted.Raise(null);
		// 	gameObject.SetActive(false);
		// }

		// Don't even show the 
		if(mCanRespawn){
			
			if(input.aButtonPressed){
				string thisScene = SceneManager.GetActiveScene().name;
				SceneManager.LoadScene(thisScene);
			}
			if(input.bButtonPressed){
				SceneManager.LoadScene("MN_TestMain");			
			}
			if(input.yButtonPressed){

				// We could get a reference to the LVL_Manager, then build the player data into this. But maybe not.
				mCheckpointRestarted.Raise(null);
				gameObject.SetActive(false);
			}
		}

	}

	private void OnEnable()
	{		
		mRespawnCountdown = mRespawnTime;
		
		// give a little delay here.
		tRespawn.enabled = false;
		tMainMenu.enabled = false;
		mCanRespawn = false;
		Invoke("EnableRespawn", mRespawnEnableTime);
	}

	private void EnableRespawn(){
		mCanRespawn = true;
		tRespawn.enabled = true;
		tMainMenu.enabled = true;
	}
}
