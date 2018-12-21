using UnityEngine;

public enum TerrainType : int {
	GRASS,
	SAND,
	CONCRETE,
	ERROR
}

public class LVL_Terrain : MonoBehaviour {

	public TerrainType		mType = TerrainType.GRASS;
}
