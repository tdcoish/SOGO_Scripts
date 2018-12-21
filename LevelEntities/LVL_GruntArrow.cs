using UnityEngine;

public class LVL_GruntArrow : MonoBehaviour {

    [SerializeField]
    private GameObject      BouncyProp;
    [SerializeField]
    private GameObject      DestroyParticles;

    // all it needs to do is maintain a certain distance above it's owners head.
    private void Update(){
        Vector3 pos = transform.parent.position;
        pos.y+=3f;

        // Now we have the arrow moving up and down over some time.
        pos.y += Mathf.Sin(Time.time);

        transform.position = pos;
    }

    // When we're destroyed, we spawn in a throwable game object, the arrow, in our position.
    private void OnDestroy(){  
        // Instantiate(DestroyParticles, transform.position, transform.rotation);
        Instantiate(BouncyProp, transform.position, transform.rotation);
    }
}