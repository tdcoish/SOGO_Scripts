using UnityEngine;

[CreateAssetMenu(fileName="NewThrowableObjectProperties", menuName="SunsOutGunsOut/ThrowableObjectProperties")]
public class ThrowableObjectProperties : ScriptableObject
{
	public float throwingForce = 100f;
    public float damage = 100f;
    public float transparencyWhenPickedup = 0.2f;
    [Tooltip("Small, Medium or Large")]
    public string objSize = "Large"; 
    public string soundEventName = "PC_BasketNet_ST_PL";

    [Space, Header("Rotation")]
    public Vector3 throwingRotation;
    public float rotationSpeed;

    public GameObject explosionParticlePrefab;
}


