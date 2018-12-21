using UnityEngine;

/*********************************************************************************************************
CARLOS: 
Please rewrite this script as you would like. However, it would be very elegant to have projectiles inheriting
from this, since then I can just write that the AI_Gun parent class contains a reference to a projectile prefab,
which will be made. 
This is just an example of what the projectile parent class might look like. Again, please rewrite.
********************************************************************************************************* */
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public abstract class PROJ_Projectile : MonoBehaviour {
	
    [SerializeField]
	[Tooltip("Duration in seconds before object terminates itself")]
	protected float lifeTime = 1;

    protected virtual void Start(){
        Invoke("Terminate", lifeTime);
    }

    protected virtual void Terminate(){
        //var clone = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}