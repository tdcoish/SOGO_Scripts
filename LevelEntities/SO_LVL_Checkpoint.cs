using UnityEngine;

/*********************************************************************************
Create these, then you can optionally start from them instead of the beginning.
******************************************************************************* */
[CreateAssetMenu(fileName="LVL_Checkpoint", menuName="SunsOutGunsOut/LVL/CheckpointData")]
public class SO_LVL_Checkpoint : ScriptableObject {

	public int mSectionInd;
	public int mWaveInd;
	public Vector3			mPlayerPos;
	public Quaternion		mPlayerRot;
}
