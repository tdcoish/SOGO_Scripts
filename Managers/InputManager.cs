/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
* In case we do split screen use XInput
* 
*/

using UnityEngine;

public class InputManager : SingletonBehaviour<InputManager>
{
    [SerializeField]
    private InputState input;
    [SerializeField]
    private FloatVariable leftTrigger;
    [SerializeField]
    private FloatVariable rightTrigger;
    
    protected override void SingletonAwake()
    {
        
    }

    private void Update()
    {
        input.leftStickX = Input.GetAxis("LeftStickHorizontal");    
        input.leftStickY = Input.GetAxis("LeftStickVertical");    
        
        input.rightStickX = Input.GetAxis("RightStickHorizontal");    
        input.rightStickY = Input.GetAxis("RightStickVertical");    
        input.rightStickPressed = Input.GetButtonDown("RightStickButton");

        input.aButtonPressed = Input.GetButtonDown("AButton");
        input.aButtonHold = Input.GetButton("AButton");
        
        input.bButtonPressed = Input.GetButtonDown("BButton");
        input.bButtonHold = Input.GetButton("BButton");
        
        input.xButtonPressed = Input.GetButtonDown("XButton");
        input.xButtonHold = Input.GetButton("XButton");

        input.yButtonPressed = Input.GetButtonDown("YButton");
        input.yButtonHold = Input.GetButton("YButton");

        input.startButtonPressed = Input.GetButtonDown("StartButton");

        input.leftTrigger = leftTrigger.Value = Input.GetAxis("LeftTrigger");
        input.rightTrigger = rightTrigger.Value = Input.GetAxis("RightTrigger");

        input.leftBumperPressed = Input.GetButtonDown("LeftBumper");
        input.leftBumperHold = Input.GetButton("LeftBumper");
        
        input.rightBumperPressed = Input.GetButtonDown("RightBumper");
        input.rightBumperHold = Input.GetButton("RightBumper");

        // These calls must be valid in the Unity Editor. If the defaults get changed, then this won't work.
        input.mouseXInput = Input.GetAxis("Horizontal");
        input.mouseZInput = Input.GetAxis("Vertical");
        input.mouseYInput = Input.GetAxis("Altitude");

        // Used only for convenience in dev. Not valid in game
        input.numKey1 = Input.GetKeyDown(KeyCode.Alpha1);
        input.numKey2 = Input.GetKeyDown(KeyCode.Alpha2);
        input.numKey3 = Input.GetKeyDown(KeyCode.Alpha3);
    }
}