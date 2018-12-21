using UnityEngine;

/*********************************************************************************************************
Unfortunately, we can't use our lifetime script, because this object needs to terminate itself. 
********************************************************************************************************* */
public class PROJ_Plasmoid : PROJ_Projectile {

    protected override void Start(){
        Invoke("Terminate", lifeTime);
        // Get it to start it's hum. Although this is so irritating I'm just not.
        // AUD_Manager.PostEvent("PJ_AR_LP_PL", gameObject);
    }

    // Terminate will potentially spawn an object with particles, for example, or, in the case of the FatBoy, spawn an explosion object.
    protected override void Terminate(){
        // AUD_Manager.PostEvent("PJ_AR_LP_SP", gameObject);
        //var clone = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
