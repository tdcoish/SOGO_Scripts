using UnityEngine;

// AI_Master has reference to this object, and will check every frame if it's time to spawn an enemy 
public class AI_Spawner : MonoBehaviour {

    [SerializeField]
    private SO_AI_Spawner       mSpawnTemplate;
    private float               mSpawnTimeStamp;
    public bool                 mCanSpawnNow;

    // Need this to fix bug where we get the spawn fx even if nothing comes from it.
    [HideInInspector]
    public bool                mHasSpawnedFX = false;

    [SerializeField]
    private GameObject          mSpawnedFX;

    private void FixedUpdate(){

        if(Time.time - mSpawnTimeStamp > mSpawnTemplate.mSpawnRate){
            mCanSpawnNow = true;
        }
    }

    public GameObject SpawnEnemy(GameObject typeToSpawn){

        mSpawnTimeStamp = Time.time;
        mCanSpawnNow = false;

        AUD_Manager.PostEvent("AM_GravLiftTeleport_ST", gameObject);
        
        var clone = Instantiate(typeToSpawn, transform.position, transform.rotation);
        return clone;
    }

    public bool IsReadyToSpawn(){
        return mCanSpawnNow;
    }

    public void PlaySpawnFX(){
        Vector3 spaceShipSpawnPoint = transform.position;
        // spaceShipSpawnPoint.y += 10f;
        Instantiate(mSpawnedFX, spaceShipSpawnPoint, transform.rotation);
        mHasSpawnedFX = true;
    }

    private void OnEnable(){
        mSpawnTimeStamp = Time.time;
    }

    private void OnDisable(){
        mHasSpawnedFX = false;
    }

}
