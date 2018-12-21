using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BTN_IG_Menu : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        AUD_Manager.PostEvent("FE_UI_Down_ST", gameObject);
    }
}