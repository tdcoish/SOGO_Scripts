using UnityEngine;
using UnityEngine.SceneManagement;

/****************************************************************************************
Delete this hack immediately after the demo. It is only useful for switching between scenes, 
and will not be used after today.
**************************************************************************************** */
public class SceneChangerHack : MonoBehaviour{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SceneManager.LoadScene("TimPlayground");
        } else if(Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene("CarlosPlaygroundV2");
        }
    }
}
