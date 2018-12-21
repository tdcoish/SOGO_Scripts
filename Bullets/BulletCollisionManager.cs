using UnityEngine;

public class BulletCollisionManager : MonoBehaviour {

    [SerializeField]
    private BooleanVariable effectiveDeflectorActive;
    private Bullet bullet;

	private void Awake() {
		bullet = GetComponent<Bullet>();
	}

	private void OnTriggerEnter(Collider other) {
        
        // Deal with the two deflectors
        BulletDeflector deflector = other.GetComponent<BulletDeflector>();
        if (deflector != null) {
            return;
        }
        
        // Deal with player damage
        IDamageable damageableObject =  other.GetComponentInParent<IDamageable>();
		if (damageableObject != null) {
            if (other.GetComponentInParent<PlayerHealth>() && bullet.bouncedByPlayer == false && effectiveDeflectorActive.Value == false) {
                damageableObject.TakeDamage(bullet.damage);
                AUD_Manager.SetSwitch("ImpactObject", "Character", gameObject);
                AUD_Manager.SetSwitch("ImpactType", "Kinetic", gameObject);
                AUD_Manager.PostEvent("WP_ProjImpact_ST", gameObject);
                bullet.DestroyBullet();
                return;
            }

            // SimpleEnemy se = other.GetComponent<SimpleEnemy>();
            // if (se != null && bullet.bouncedByPlayer == true) {
            //     se.TakeDamage(bullet.damage);
            //     bullet.DestroyBullet();
            //     return;
            // }


            EnemyForceField forceField = other.GetComponent<EnemyForceField>();
            if (forceField != null && bullet.bouncedByPlayer) {
                damageableObject.TakeDamage(bullet.damage);
                bullet.DestroyBullet();
                return;
            }
		}

        // Deal with killing the enemy
        AI_Controller enemyController = other.GetComponentInParent<AI_Controller>();
        if (enemyController != null && bullet.bouncedByPlayer) {
            enemyController.TakeDamage(bullet.damage);
            bullet.DestroyBullet();   
            return;
        }

        // Handle Turret as well.
        AI_Turret turController = other.GetComponentInChildren<AI_Turret>();
        if(turController != null && bullet.bouncedByPlayer){
            turController.TakeDamage(bullet.damage);
            bullet.DestroyBullet();
        }

        // Deal with any object
        if (other.GetComponent<DestroyObjectOnCollision>() != null) {
            ThrowableObject throwableObj = other.GetComponent<ThrowableObject>();
            if (throwableObj != null && bullet.bouncedPerfectly) {
                throwableObj.DestroyObject();
                return;
            }
            AUD_Manager.SetSwitch("ImpactObject", "Environment", gameObject);
            AUD_Manager.SetSwitch("ImpactType", "Energy", gameObject);
            AUD_Manager.PostEvent("WP_ProjImpact_ST", gameObject);
            bullet.DestroyBullet();
        }
    }
}
