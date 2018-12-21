using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************
Largely stolen from the Beario project, although I still found this quite helpful.
*************************************************************** */

//Will automatically add this to the game object this script is added to
// [RequireComponent(typeof(Rigidbody2D))]
// [RequireComponent(typeof(Animator))]
// [RequireComponent(typeof(SpriteRenderer))]

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(BoxCollider))]

public abstract class Unit : MonoBehaviour {
	#region GlobalVars


    	// [Header("Raycast Settings")]
		// [SerializeField]
		// protected float _RaycastDistance = 0.1f;

		// [Tooltip("Horizontal distance based on the Pivot point to...")]
		// [SerializeField]
		// protected float _RaycastOffset = 0.4f;

		// [Header("Movement Settings")]
		// [SerializeField]
		// protected float _MoveSpeed = 8f;
		
		// protected Rigidbody2D _RB;
		// protected Animator _Anim;
		// protected SpriteRenderer _SR;

		protected MeshRenderer _MR;
		protected MeshFilter _MF;
		protected BoxCollider _BC;

		// protected bool _IsGrounded = false;


	#endregion GlobalVars
	
	 //get all of our references
    protected void Awake()
	{
		_MR = GetComponent<MeshRenderer>();
		_MF = GetComponent<MeshFilter>();
		_BC = GetComponent<BoxCollider>();

		// _Anim = GetComponent<Animator>();
		// _RB = GetComponent<Rigidbody2D>();
		// _SR = GetComponent<SpriteRenderer>();
		// _RB.freezeRotation = true;
	}

	protected void Update()
	{
		//can't think of any behaviour right now


		// //isGrounded has tons of side effects
		// _IsGrounded = isGrounded(_RaycastOffset, _RaycastDistance) | isGrounded(-_RaycastOffset, _RaycastDistance);	
	
		// _Anim.SetBool("Grounded", _IsGrounded);
	}

	protected void Move(){
		//Our turrets don't move. This is a touch difficult.


		// _SR.flipX = direction > 0 ? false : (direction<0? true : _SR.flipX);


		// _RB.velocity = new Vector2(direction * _MoveSpeed, _RB.velocity.y);
		// _Anim.SetFloat("Speed", Mathf.Abs(direction));
	}

}
