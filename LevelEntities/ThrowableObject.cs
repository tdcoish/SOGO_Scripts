using UnityEngine;

public class ThrowableObject : MonoBehaviour {

	[SerializeField]
	internal ThrowableObjectProperties properties;
	[SerializeField]
	private LayerMask layerWhenPickedUp;
	[SerializeField]
	private Vector3 rotationWhenPickedUp = Vector3.zero;
	[SerializeField]
	private Collider[] collidersBeforePickup;
	[SerializeField]
	private Collider throwHitboxCollider;

	[Space, Header("Score")]
	[SerializeField]
	private GameEvent addScore;
	[SerializeField]
	private float pointsForEnemyKill = 100f;
	
	private Rigidbody rb;
	internal float pickupStatus = 0f;
	internal bool hasBeenThrown = false;
	private MeshRenderer meshRenderer;
	private LayerMask initialLayer;

	internal bool pickedUp = false;

	private void Awake() {
		meshRenderer = GetComponent<MeshRenderer>();
		SetTransparency(1f);
		rb = GetComponent<Rigidbody>();
		rb.isKinematic = true;
		throwHitboxCollider.isTrigger = true;
		throwHitboxCollider.enabled = false;
		foreach(Collider coll in collidersBeforePickup) {
			coll.isTrigger = false;
			coll.enabled = true;
		}
	}

	private void Update() {
		if (pickedUp && hasBeenThrown == false) {
			transform.localPosition = Vector3.zero;
		}
	}

	private void FixedUpdate() {
		if (hasBeenThrown) {
            Quaternion deltaRotation = Quaternion.Euler(properties.throwingRotation * properties.rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);    
        }
	}

	public void PickUp(Transform parentTransform) {
		foreach(Collider coll in collidersBeforePickup) {
			coll.isTrigger = true;
			coll.enabled = false;
		}
		transform.parent = parentTransform;
		transform.localRotation = Quaternion.Euler(rotationWhenPickedUp);
		initialLayer = gameObject.layer;
		pickedUp = true;
		gameObject.layer = layerWhenPickedUp;
		if (properties.transparencyWhenPickedup != 0.0) SetTransparency(properties.transparencyWhenPickedup);
		AUD_Manager.SetSwitch("ObjectAction", "Pickup", gameObject);
		AUD_Manager.PostEvent(properties.soundEventName, gameObject);
	}

	private void SetTransparency(float newOpacity) {
		meshRenderer.material.SetFloat("_ZWrite", newOpacity);
		Color meshColor = meshRenderer.material.color;
		meshColor.a = newOpacity;
		meshRenderer.material.SetColor("_Color", meshColor);
	}

	public void Throw(Vector3 direction) {
		Drop();
		rb.AddForce(direction * properties.throwingForce, ForceMode.Impulse);
		Invoke("KillMyselfAfterTime", 3f);
	}

	public void Drop() {
		hasBeenThrown = true;
		transform.parent = null;
		rb.isKinematic = false;
		SetTransparency(1f);
		gameObject.layer = initialLayer;
		throwHitboxCollider.isTrigger = true;
		throwHitboxCollider.enabled = true;
		AUD_Manager.SetSwitch("ObjectSize", properties.objSize, gameObject);
		AUD_Manager.PostEvent("PC_ThrowWhooshes_ST_PL", gameObject);
	}

	public void DestroyObject() {
		AUD_Manager.SetSwitch("ObjectAction", "Impact", gameObject);
		AUD_Manager.PostEvent(properties.soundEventName, gameObject);
		if (properties.explosionParticlePrefab != null) {
			Instantiate(properties.explosionParticlePrefab, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		// Also want it destroying the lanky shield if it hits him
		if(hasBeenThrown){
			EnemyForceField	forceField = UT_FindComponent.FindComponent<EnemyForceField>(other.gameObject);
			if(forceField != null){
				forceField.TakeDamage(10000f);
				DestroyObject();
				return;
			}
		}

		if ((other.GetComponent<DestroyObjectOnCollision>() != null && other.GetComponent<ThrowableObject>() == null) && hasBeenThrown) {
			DestroyObject();
			return;
		}

		AI_Controller npc = UT_FindComponent.FindComponent<AI_Controller>(other.gameObject);
		if (npc != null && hasBeenThrown) {
			npc.TakeDamage(properties.damage);
			addScore.Raise(pointsForEnemyKill);
			DestroyObject();
		}

	}

	// Just in case we never hit anything, we guarantee that we get destroyed.
	private void KillMyselfAfterTime(){
		DestroyObject();
	}
}
