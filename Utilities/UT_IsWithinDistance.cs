using UnityEngine;

public static class UT_IsWithinDistance {

	public static bool IsWithinDistance(Vector3 pos1, Vector3 pos2, float dis){
		if(Vector3.Distance(pos1, pos2) < dis){
			return true;
		}

		return false;
	}
}
