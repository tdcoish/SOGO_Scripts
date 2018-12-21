using System.Collections;
using UnityEngine;

public class PoolableObject : MonoBehaviour 
{
	[SerializeField]
	[Tooltip("-1 = Infinite")]
	private float lifeSpan = -1f; // This is to enable and disable the object, not to destroy

	private Coroutine disablerPointer;

	private void Awake()
	{
		disablerPointer = StartCoroutine(Disabler());
	}
	
	protected void OnEnable()
	{
		if(lifeSpan >= 0)	
		{
			StartCoroutine(Disabler());
		}
		OnEnable_PO();
	}

	protected void OnDisable()
	{
		if(disablerPointer != null)
		{
			StopCoroutine(Disabler());
		}
		OnDisable_PO();
	}

	IEnumerator Disabler()
	{
		yield return null;
		gameObject.SetActive(false);
	}

	protected virtual void OnEnable_PO() {}
	protected virtual void OnDisable_PO() {}

}
