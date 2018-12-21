using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BTN_Select : MonoBehaviour, ISelectHandler, IDeselectHandler{

    private Color               mSelectedColour;
    private Color               mUnselectedColour;

    private int         mColInc = 40;

    private void Awake(){
        mSelectedColour = new Color(0.1f, 0.6f, 0.9f, 1f);
        mUnselectedColour = new Color(0f, 0f, 0f, 1f);

        GetComponentInChildren<Text>().color = mUnselectedColour;
    }

    public void OnSelect(BaseEventData eventData)
    {
        Vector3 temp = transform.localScale;
        temp.x += 0.1f;
        transform.localScale = temp;
        Button btn = GetComponent<Button>();

        MN_ActiveButton.Instance.mActiveButton = btn;

        GetComponentInChildren<Text>().color = mSelectedColour;
    }

    public void OnDeselect(BaseEventData data){
        Vector3 temp = transform.localScale;
        temp.x -= 0.1f;
        transform.localScale = temp;

        ResetColourOnLeaving();
    }

    public void ResetColourOnLeaving(){
        GetComponentInChildren<Text>().color = mUnselectedColour;
    }
}
