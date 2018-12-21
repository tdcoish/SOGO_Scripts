using UnityEngine;

[CreateAssetMenu(fileName="TankGunProperties", menuName="SunsOutGunsOut/AI/TankGunProperties")]
public class WP_TNK_Gun : ScriptableObject
{

    public float         mMaxChargeUpTime;
	public float 		mTimeBetweenShots;			// actually the time in between firings once we're actually charged up. Remember it's a volley.

	public int 			mNumInVolley;

    public GameObject              	mBlueBullet;
	public GameObject				mRedBullet;
}

