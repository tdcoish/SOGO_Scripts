using UnityEngine;
using UnityEngine.UI;

/*********************************************************************************************
Controls UI for the tutorial area. Basically it works entirely through the Events system.
******************************************************************************************** */
public class UI_Tutorial : MonoBehaviour {

    // UI_TutComp is basically just a tag.

    [SerializeField]
    private UI_TutComp      _MovePromptScreen;

    [SerializeField]
    private UI_TutComp      _PickupPromptScreen;
    [SerializeField]
    private UI_TutComp      _ThrowPromptScreen;

    [SerializeField]
    private UI_TutComp      _JumpPromptScreen;  

    [SerializeField]
    private UI_TutComp      _SlidePromptScreen; 

    [SerializeField]
    private UI_TutComp      _LeftTriggerPromptScreen;

    [SerializeField]
    private UI_TutComp      _RightTriggerPromptScreen;

    [SerializeField]
    private UI_TutComp      _MeleePromptScreen;
    
    [SerializeField]
    private UI_TutComp      _GrenadePromptScreen;

    public void OnTutorialStarted(){
        _MovePromptScreen.gameObject.SetActive(true);
    }

    public void OnJumpPlatformHit(){
        _JumpPromptScreen.gameObject.SetActive(true);
    }
    public void OnJumpPlatformLeft(){
        _JumpPromptScreen.gameObject.SetActive(false);
    }

    public void OnSlideAreaReached(){
        _JumpPromptScreen.gameObject.SetActive(false);
        _SlidePromptScreen.gameObject.SetActive(true);
    }
    public void OnSlideAreaLeft(){
        _SlidePromptScreen.gameObject.SetActive(false);
    }

    public void OnPoseAreaReached(){
        _SlidePromptScreen.gameObject.SetActive(false);
        _LeftTriggerPromptScreen.gameObject.SetActive(true);
    }
    public void OnPoseAreaExit(){
        _LeftTriggerPromptScreen.gameObject.SetActive(false);
    }

    public void OnFlexAreaHit(){
        _RightTriggerPromptScreen.gameObject.SetActive(true);
    }
    public void OnFlexAreaLeft(){
        _RightTriggerPromptScreen.gameObject.SetActive(false);
    }

    public void OnMeleeAreaEnter(){
        _MeleePromptScreen.gameObject.SetActive(true);
    }
    public void OnMeleeAreaExit(){
        _MeleePromptScreen.gameObject.SetActive(false);
    }

    public void OnGrenadeRoomEnter(){
        _GrenadePromptScreen.gameObject.SetActive(true);
    }
    public void OnGrenadeRoomExit(){
        _GrenadePromptScreen.gameObject.SetActive(false);
    }

    public void OnTutorialEnded(){
        gameObject.SetActive(false);
    }
}
