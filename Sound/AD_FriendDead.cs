using System.Collections;
using UnityEngine;

/**********************************************************************************************
This gets attached to every enemy, and is called by an event listener that listens for enemies 
dying. We call in to this object, and it figures out which line to play.
********************************************************************************************* */
public class AD_FriendDead: MonoBehaviour {

	[SerializeField]
	private FloatVariable 				mDis;

	[SerializeField]
	private FloatVariable				mDelay;

	[SerializeField]
	private SO_AD_SympathyLines		mSympathyLines;

	private EnemyTypes		mType;

	// Due to the method of attachment, the object does not update with the enemies movement.
	// SO we need to compare using this transform.
	private AI_Controller 	mCont;

	private void Awake(){
		if(UT_FindComponent.FindComponent<GRT_Behaviour>(gameObject) != null){
			mType = EnemyTypes.GRUNT;
		} else if(UT_FindComponent.FindComponent<LNK_Behaviour>(gameObject) != null){
			mType = EnemyTypes.LANKY;
		} else if(UT_FindComponent.FindComponent<TNK_Behaviour>(gameObject) != null){
			mType = EnemyTypes.TANK;
		}else{
			
		}

		mCont = UT_FindComponent.FindComponent<AI_Controller>(gameObject);
	}


	// Lul, apparently we can trigger this on ourselves. So I guess I'll need a minimum distance from which to NOT play the sound.
	public void HandleGruntDied(object data){
		Vector3 deathPos = (Vector3) data;

		float dis = Vector3.Distance(deathPos, mCont.transform.position);

		if(dis < mDis.Value/10f) return;

		if(dis < mDis.Value){
			switch(mType){
				case EnemyTypes.GRUNT: StartCoroutine(PlayDialogueDelayed(mSympathyLines.gruntToGrunt)); break;
				case EnemyTypes.LANKY: StartCoroutine(PlayDialogueDelayed(mSympathyLines.lankyToGrunt)); break;
				case EnemyTypes.TANK: StartCoroutine(PlayDialogueDelayed(mSympathyLines.tankToGrunt)); break;
			}
		}
	}

	public void HandleLankyDied(object data){
		Vector3 deathPos = (Vector3) data;

		float dis = Vector3.Distance(deathPos, mCont.transform.position);

		if(dis < mDis.Value/10f) return;		

		if(dis < mDis.Value){
			switch(mType){
				case EnemyTypes.GRUNT: StartCoroutine(PlayDialogueDelayed(mSympathyLines.gruntToLanky)); break;
				case EnemyTypes.LANKY: StartCoroutine(PlayDialogueDelayed(mSympathyLines.lankyToLanky)); break;
				case EnemyTypes.TANK: StartCoroutine(PlayDialogueDelayed(mSympathyLines.tankToLanky)); break;
			}
		}
	}

	public void HandleTankDied(object data){
		Vector3 deathPos = (Vector3) data;

		float dis = Vector3.Distance(deathPos, mCont.transform.position);

		if(dis < mDis.Value/10f) return;

		if(dis < mDis.Value){
			switch(mType){
				case EnemyTypes.GRUNT: StartCoroutine(PlayDialogueDelayed(mSympathyLines.gruntToTank)); break;
				case EnemyTypes.LANKY: StartCoroutine(PlayDialogueDelayed(mSympathyLines.lankyToTank)); break;
				case EnemyTypes.TANK: StartCoroutine(PlayDialogueDelayed(mSympathyLines.tankToTank)); break;
			}
		}
	}

	
	IEnumerator PlayDialogueDelayed(SO_AD_Dialogue data){
		yield return new WaitForSeconds(mDelay.Value);
		AUD_Manager.DynamicDialogueFromData(data, gameObject);
	}

}
