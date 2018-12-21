using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class UIScreen : MonoBehaviour 
{
	[SerializeField]
	protected InputState input;
	[SerializeField]
	protected EventSystem eventSystem;
	[SerializeField]
	protected GameObject firstSelectedObject;
	
	private void OnEnable()
	{
		UIScreenEnabled();	
	}

	private void OnDisable()
	{
		UIScreenDisabled();
	}
	
	protected abstract void UIScreenEnabled();
	protected abstract void UIScreenDisabled();

	protected IEnumerator SetPreselectedObject() {
		eventSystem.SetSelectedGameObject(null);
		yield return null;
		eventSystem.SetSelectedGameObject(firstSelectedObject);
		yield break;
	}

	// NOTE: Need to get OnSelect() for playing audio when the player is selecting between buttons.
}