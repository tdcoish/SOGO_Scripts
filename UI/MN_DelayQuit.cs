using UnityEngine;

/************************************************************************
The last script that gets executed in the whole game.
********************************************************************** */
public class MN_DelayQuit : MonoBehaviour {

	private void Awake(){
		Invoke("Quit", 3.0f);
	}

	public void Quit(){
		Application.Quit();
	}
}
