using UnityEngine;

[CreateAssetMenu(fileName="NewGunProperties", menuName="SunsOutGunsOut/AI/NewGunProperties")]
public class SO_WP_Gun : ScriptableObject
{
	// Should probably change these to private and read only.
	[Header("Maximum range to start firing")]
	public float			mRange;

	public string				mAudFireEvent = "WP_ST_PL";
	public string[]				mSwitchGroups;
	public string[]				mSwitchStates;
	
	[Tooltip("Specific name of Wwise event that plays pre-fire sound")]
	public string				mAudPreFireEvent = "NameOfChargeAudioEvent";

	[Header("Carries game object that is muzzle blast")]
    public PART_MuzzleBlast               mMuzzleBlast;
}

