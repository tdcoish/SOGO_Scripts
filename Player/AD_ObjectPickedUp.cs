using UnityEngine;

public class AD_ObjectPickedUp : MonoBehaviour {

	[SerializeField]
	private StringVariable		Player;

	private string mEvent = "VO_ObjectInteraction_PL";
	private string[] mArgs;

	private void Awake(){
		mArgs = new string[2];
		mArgs[0] = Player.Value;
	}

	public void OnObjectPickedUp(object data){
		ThrowableObject obj = (ThrowableObject) data;

		if(obj!=null){
			// now here we find out if it's small, medium, or large
			switch(obj.properties.objSize){
				case "Small": mArgs[1] = "PickupSmall"; break;
				case "Medium": mArgs[1] = "PickupMedium"; break;
				case "Large": mArgs[1] = "PickupLarge"; break;
			}

			AUD_Manager.DynamicDialogue(mEvent, mArgs, gameObject);
		}
	}
}
