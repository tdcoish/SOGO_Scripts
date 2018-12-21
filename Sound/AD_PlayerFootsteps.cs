using UnityEngine;

/******************************************************
Unity calls the correct functions when the animation triggers
the event.
***************************************************** */
public class AD_PlayerFootsteps : MonoBehaviour {

	// We can actually just hardcode all these since we know what they are and they never change.
	private string mRunStepEventName = "PC_FS_ST";

	// Called by the run animation at the correct frame.
	private void RunStep(){

		// Now we actually have to raycast downwards and check what type of terrain we're on.
		TerrainType type = FindTerrainStandingOn();
		switch(type){
			case TerrainType.GRASS: AUD_Manager.SetSwitch("Surface_Type", "Grass", gameObject); break;
			case TerrainType.SAND: AUD_Manager.SetSwitch("Surface_Type", "Sand", gameObject); break;
			case TerrainType.CONCRETE: AUD_Manager.SetSwitch("Surface_Type", "Concrete", gameObject); break;
			case TerrainType.ERROR: break;
		}

		AUD_Manager.PostEvent(mRunStepEventName, gameObject);
	}

	private TerrainType FindTerrainStandingOn(){
		TerrainType type = TerrainType.ERROR;

		Vector3 dir = Vector3.down;
		int layerMask = 1<<LayerMask.NameToLayer("LevelGeometry");
		RaycastHit hit;

		if(Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, layerMask)){
			LVL_Terrain tag = hit.transform.GetComponent<LVL_Terrain>();
			if(tag != null){
				type = tag.mType;
			}
		}

		return type;
	}

}
