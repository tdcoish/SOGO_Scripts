using System.Collections.Generic;
using UnityEngine;

public class DB_AD_CycleMus : MonoBehaviour {

	[SerializeField]
	private string[]		mProgressState = new string[4];
	private int 		index = 0;

	private void Update(){
		if(Input.GetMouseButtonDown(0)){
			Debug.Log("Setting progress music switch: " + mProgressState[index]);
			AUD_Manager.SetSwitch("ObjectiveProgress", mProgressState[index], gameObject);
			index++;
			if(index >= mProgressState.Length){
				index = 0;
			}
		}
	}
}
