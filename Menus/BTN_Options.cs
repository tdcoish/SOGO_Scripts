using UnityEngine;
using UnityEngine.UI;

public class BTN_Options : MonoBehaviour{

    [SerializeField]
    protected Text        mLeftArrow;
    [SerializeField]
    protected Text        mRightArrow;

    public virtual void OnPressLeft(){
        Vector3 temp = mLeftArrow.transform.localScale;
        mLeftArrow.fontSize += 5;
        Invoke("ResetLeft", 0.1f);
    }
    public virtual void OnPressRight(){
        Vector3 temp = mRightArrow.transform.localScale;
        mRightArrow.fontSize += 5;
        Invoke("ResetRight", 0.1f);
    }

    protected void ResetLeft(){
        Vector3 temp = mLeftArrow.transform.localScale;
        mLeftArrow.fontSize -= 5;        
    }

    protected void ResetRight(){
        Vector3 temp = mRightArrow.transform.localScale;
        mRightArrow.fontSize -= 5;
    }
}