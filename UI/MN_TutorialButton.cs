using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MN_TutorialButton : MonoBehaviour{

    public void OnClick(){
        MN_Manager.Instance.mSceneToLoad = "LVL_BeachTutorialArena";
        MN_Manager.Instance.ShowScreen<MN_LoadScreen>();
        // ALso, got to stop playing menu music.
        AkSoundEngine.StopAll();
    }
}