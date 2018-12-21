/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

[CreateAssetMenu(fileName="NewInputState", menuName="SunsOutGunsOut/InputState")]
public class InputState : ScriptableObject
{
    public float leftStickX;
    public float leftStickY;

    public float rightStickX;
    public float rightStickY;
    public bool rightStickPressed;
    public bool invertYAxis;
    public bool invertXAxis;

    public bool aButtonPressed; 
    public bool aButtonHold; 

    public bool bButtonPressed;
    public bool bButtonHold;

    public bool xButtonPressed; 
    public bool xButtonHold; 
    
    public bool yButtonPressed;
    public bool yButtonHold;

    public bool startButtonPressed;

    public float leftTrigger;
    public float rightTrigger;

    public bool leftBumperPressed;
    public bool leftBumperHold;

    public bool rightBumperPressed;
    public bool rightBumperHold;

    // -------------------------------------------- Used for debugging camera on PC
    public float mouseXInput;
    public float mouseZInput;
    public float mouseYInput;
    // --------------------------------------------

    // utilities used for dev, not gameplay.
    public bool     numKey1;
    public bool     numKey2;
    public bool     numKey3;
}