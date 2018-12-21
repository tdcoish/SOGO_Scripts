using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonScaler : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IDeselectHandler, IPointerExitHandler {

	[SerializeField]
	private Vector3 highlightedScale;

	// Select
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = highlightedScale;
    }
    
	public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = highlightedScale;
    }

	// Deselect
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void OnDeselect(BaseEventData eventData)
    {
		transform.localScale = Vector3.one;
    }
}
