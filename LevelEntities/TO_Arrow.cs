using UnityEngine;

public class TO_Arrow : MonoBehaviour{

    [SerializeField]
    private TransformVariable       PlayersTransform;

    private ThrowableObject     throwScript;

    void Awake(){
        throwScript = GetComponent<ThrowableObject>();
    }

    // on start, we make it roughly at the ground.
    private void Start(){
        // This might screw us up if we have it in our possesion.
        Invoke("Disappear", 20f);
    }

    public void Disappear(){
        if(throwScript.pickedUp)
            return;

        Vector3 playerPos = PlayersTransform.Value.position;
        if(Vector3.Distance(playerPos, transform.position) < 5f){
            Invoke("Disappear", 10f);
            return;
        }

        Destroy(gameObject);
    }
}