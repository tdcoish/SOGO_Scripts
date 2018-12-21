using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************
* NOTE: This is basically just a test of animations. The animations are actually
* of the player.
************************************************************ */
public class Anim_EnemyGrunt : MonoBehaviour {

    // Drag and drop carlos' scriptable object in here.
    [SerializeField]
	private InputState input;

    private Animator            anim;
    
    // implicitly figure out if its idling or not.
    private bool                isMoving;
    private bool                isFlexing;
    private bool                isIdling;
    
    private void Awake(){
        anim = GetComponent<Animator>();
    }

    private void Update(){
        // Top down setting of bools
        if(input.numKey1) isMoving = !isMoving;
        if(input.numKey2) isFlexing = !isFlexing;
        if(input.numKey3) isIdling = !isIdling;

        if(isMoving){
            Debug.Log("Here");
        }

        SetAnimationState();
    }

    public void SetAnimationState(){
        anim.SetBool("isRunning", isMoving);
        anim.SetBool("isFlexing", isFlexing);
        anim.SetBool("isIdling", isIdling);
    }
}
