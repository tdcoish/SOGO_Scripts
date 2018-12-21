using UnityEngine;


/**************************************************************************************
* This object exists mainly to be attached to a prefab. It contains a Lifetime script, so it
lasts a set amount of time, deals a certain amount of damage, and the prefab will have 
particles attached to it.

The idea is that this object lasts for approximately 0.1 seconds. We might need the particles
to last longer than that, so we can put them into a different prefab, remove the lifetime 
script from this, and give it it's own Terminate function.
***************************************************************************************/
[RequireComponent(typeof(Lifetime))]
[RequireComponent(typeof(SphereCollider))]
public class AI_FatBoy_Explosion : MonoBehaviour {

    [Tooltip("Update, this is not per second anymore, this is total damage dealt")]
	[SerializeField]
	public int				damDealt;

}
