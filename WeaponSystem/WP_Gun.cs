using UnityEngine;

// Update. This is now just a purely virtual script that my weapons inherit from, so other scripts
// don't have to worry about which weapon.
public class WP_Gun : MonoBehaviour {

    [SerializeField]
    public SO_WP_Gun       mGunProperties;

    [HideInInspector]
	public float 			mFireTimeStamp = 0f;

    [SerializeField]
    protected WeaponChargeVisualIndicator        mChargeParticles;

    public virtual float    GetNormalizedCharge() {return 0f;}

    public virtual void TryToFireGun(Transform victim) {}
    public virtual void StopTryingToFire() {}
    
}
