using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL_EnemyHashes : SingletonBehaviour<LVL_EnemyHashes> {

	private Dictionary<int, GameObject>			mDict;

	protected override void SingletonAwake(){
        mDict = new Dictionary<int, GameObject>();
    }

    // Alright, we add enemies to this guy.

	public void AddEnemy(int ID, GameObject obj){
		mDict.Add(ID, obj);
	}

	public void RemoveEnemy(int ID){
		mDict.Remove(ID);
	}

	// Call this to check if our value is still valid.
	public bool IsEnemyInDict(int ID){

		if(mDict.ContainsKey(ID)){
			return true;
		}

		return false;
	}

}
