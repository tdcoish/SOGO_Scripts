using UnityEngine;
using UnityEngine.UI;

public class MN_ActiveButton : UT_Singleton<MN_ActiveButton> {

	// This gets set by a button with the BTN_Select script. 
	// Give it one by default.
	public Button mActiveButton;
    
	protected override void SingletonAwake()
    {
        
    }

}
