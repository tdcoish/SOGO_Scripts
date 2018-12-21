using UnityEngine;

public class TU_GrenadeRoom : MonoBehaviour {

    [SerializeField]
    private GameObject      mShieldPrefab;
    private GameObject      mShieldInstance;

    [SerializeField]
    private GameObject      mSpawnPoint;

    private bool        mRoomEntered = false;

    public void OnRoomEntered(){
        if(mRoomEntered) return;
        mRoomEntered = true;

        // now spawn in the shield thingamajiggy.
        var clone = Instantiate(mShieldPrefab, mSpawnPoint.transform.position, mSpawnPoint.transform.rotation);
        mShieldInstance = clone;
        if(mShieldInstance == null){
            return;
        }
        // Now we make the shield follow the spawn point.
        mShieldInstance.transform.parent = mSpawnPoint.transform;
    }

    public void OnRoomExit(){
        if(mShieldInstance != null){
            Destroy(mShieldInstance.gameObject);
        }
        mRoomEntered = false;
    }
}
