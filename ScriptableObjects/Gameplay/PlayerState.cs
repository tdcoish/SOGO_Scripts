using UnityEngine;

[CreateAssetMenu(fileName="NewPlayerState", menuName="SunsOutGunsOut/PlayerState")]
public class PlayerState : ScriptableObject {
	public bool deflecting;
	public bool airDeflect;
	public bool jumping;
	public bool sliding;
	public bool throwingObject;
	public bool pickingUpObject;
	public bool throwingGrenade;
	public bool meleeing;
	public bool grounded;
	public bool isPlayerDead;
	public bool isUsingSpecialPower;
	internal ThrowableObject carryingThrowableObject = null;
}
