using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PosAndRot{
	[HideInInspector]
	public Vector3 pos;
	[HideInInspector]
	public Vector3 rot;
	public Button btn;
}

/******************************************************************************************************
The main menu works on a system where you have a bunch of items, like Credits Screen, Model Select, whatever. 
You then Move the camera to certain prebuilt locations depending on which menu item is hovered over.
**************************************************************************************************** */
public class MN_SnapCamera : MonoBehaviour {

	[SerializeField]
	private MN_MainMenu		mMainMenu;

	[SerializeField]
	private float 		mMoveSpeed = 0.25f;

	[SerializeField]
	private float 		mRotSpeed = 1000f;

	// No idea why this isn't working. Sort of irritating since we need to now manually make both in the editor.
	public List<PosAndRot>		mPosAndRots;

	private int ind = 0;

	// For now, cycle through the various positions every 0.5 seconds or whatever.
	private float lastUpdate = 0f;
	private void Update(){
		NewUpdate();
	}

	private int FindValidPosAndRotIndiceUsingButton(){
		for(int i=0; i<mPosAndRots.Count; i++){
			if(i == MN_Manager.Instance.mActiveMainMenuButton){
				return i;
			}
		}

		return -1;
	}

	private void NewUpdate(){

		ind = FindValidPosAndRotIndiceUsingButton();
		if(ind == -1){
			ind = 0;
		}

		transform.position = transform.position.Hermite(mPosAndRots[ind].pos, mMoveSpeed);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(mPosAndRots[ind].rot), mRotSpeed * Time.deltaTime);
	}

	private void OldUpdate(){
		if(Time.time - lastUpdate > 0.5f){
			lastUpdate = Time.time;
			transform.position = mPosAndRots[ind].pos;
			transform.rotation = Quaternion.Euler(mPosAndRots[ind].rot);
			
			ind++;
			if(ind >= mPosAndRots.Count)
				ind = 0;
		}
	}
}
