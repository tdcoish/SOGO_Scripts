using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

// Also want to output the progress to text of course.
public class MN_LoadScreen : MN_Screen {

	[SerializeField]
	private Image 			mOutlineLoading;
	[SerializeField]
	private Image 			mProgress;
	[SerializeField]
	private Image 			mAIcon;

	private Image			mTipImage;


	[SerializeField]
	private Sprite[]		mTipSprites;
	private int 			mIndice = 0;

	[SerializeField]
	private float 			mChangeTime = 3f;
	private float 			mLastChangeTimeStamp;

	private void Awake(){
		mTipImage = GetComponent<Image>();
		mIndice = Random.Range(0, mTipSprites.Length-1);
		mTipImage.sprite = mTipSprites[mIndice++];
	}

	private void Update(){
		base.Update();
		if(Time.time - mLastChangeTimeStamp > mChangeTime){
			if(mIndice > mTipSprites.Length) mIndice = 0;

			mTipImage.sprite = mTipSprites[mIndice++];
			mLastChangeTimeStamp = Time.time;
		}
	}

	IEnumerator LoadScene(){
		yield return null;

		AsyncOperation asyncOp = SceneManager.LoadSceneAsync(MN_Manager.Instance.mSceneToLoad);
		asyncOp.allowSceneActivation = false;

		while(!asyncOp.isDone){
			// Huh. Apparently this might only work in the build.
			mProgress.fillAmount = asyncOp.progress * 100f;

			if(asyncOp.progress >= 0.9f){
				// change text to let them start.
				mAIcon.gameObject.SetActive(true);

				if(input.aButtonPressed){
					asyncOp.allowSceneActivation = true;
					
					// ALso, got to stop playing menu music.
					AkSoundEngine.StopAll();
				}
			}

			yield return null;
		}
	}

    protected override void MNScreenEnabled()
    {
        StartCoroutine(LoadScene());
    }

    protected override void MNScreenDisabled()
    {
    }
}
