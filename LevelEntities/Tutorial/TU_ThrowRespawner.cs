using UnityEngine;

public class TU_ThrowRespawner : MonoBehaviour {

    [SerializeField]
    private GameObject              objPrefab;
    private GameObject[]              objInstances;

    [SerializeField]
    private GameObject              spawnParticles;

    [SerializeField]
    private GameObject[]              mThrowSpawnPoints;

    private bool                    mActive = false;

    private void Awake(){
        objInstances = new GameObject[mThrowSpawnPoints.Length];
        for(int i=0; i<mThrowSpawnPoints.Length; i++){
            objInstances[i] = null;
        }
    }

    private void Update(){
        if(mActive){
            for(int i=0; i<mThrowSpawnPoints.Length; i++){
                if(objInstances[i] == null){
                    objInstances[i] = Instantiate(objPrefab, mThrowSpawnPoints[i].transform.position, mThrowSpawnPoints[i].transform.rotation);
                    AUD_Manager.PostEvent("AM_GravLiftTeleport_ST", objInstances[i].gameObject);
                    // AUD_Manager.PostEvent("PC_Tutorial_ObjectSpawn", objInstances[i].gameObject);
                    Instantiate(spawnParticles, mThrowSpawnPoints[i].transform.position, mThrowSpawnPoints[i].transform.rotation);
                }
            }
        }
    }

    public void OnThrowRoomEntered(){
        mActive = true;
    }

    public void OnThrowRoomExited(){
        mActive = false;
    }
}