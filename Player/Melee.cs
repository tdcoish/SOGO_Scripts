using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

	[SerializeField]
	private PlayerState playerState;
	[SerializeField]
    private BooleanVariable disableInput;
	[SerializeField]
	private InputState input;
	[SerializeField]
	private float attackTime = 0.15f;
	[SerializeField]
	private float meleeCooldownTime = 1f;
	[SerializeField]
	private float activateMeleeColliderTime = 0.3f;

	private bool canMelee = true;
	private float lastMeleeAttackTime = 0f;
	private CapsuleCollider coll;
	private PlayerAnimationController animationController;
	private AD_Melee 	audMelee;

	private void Awake() {
		playerState.meleeing = false;
		lastMeleeAttackTime = 0f;
		coll = GetComponentInChildren<CapsuleCollider>();
		animationController = GetComponentInParent<PlayerAnimationController>();
		audMelee = GetComponent<AD_Melee>();
	}

	private void Update() {
		if (disableInput.Value) return;
		
		if (input.xButtonPressed && canMelee == true && playerState.deflecting == false && playerState.sliding == false && playerState.throwingObject == false && playerState.throwingGrenade == false) {
			StartAttack();
		}
		
		if (canMelee == false) {
			HandleMeleeState();
		}
	}

    private void StartAttack() {
		animationController.PlayMeleeAnimation();
		playerState.meleeing = true;
		canMelee = false;
		lastMeleeAttackTime = Time.time;
		audMelee.PlayMeleeSounds();
    }

    private void HandleMeleeState() {		
		if (Time.time >= lastMeleeAttackTime + activateMeleeColliderTime && coll.enabled == false) coll.enabled = true;
		if (Time.time >= lastMeleeAttackTime + attackTime) FinishAttack();
	}

	private void FinishAttack() {
		coll.enabled = false;
		playerState.meleeing = false;
	}

	public void ResetMeleeState(object data) {
		canMelee = true;
		lastMeleeAttackTime = 0f;
	}

	public void InterruptMelee(object data) {
		FinishAttack();
		ResetMeleeState(null);
	}

}
