/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public abstract class BulletDeflector : MonoBehaviour {
	
	[SerializeField]
	private PlayerState playerState;
	[SerializeField]
    private BooleanVariable disableInput;
	[SerializeField]
	private FloatVariable stamina;
	[SerializeField]
	public EnumVariable bulletTypeToDeflect;
	[SerializeField]
	private FloatVariable mainTrigger;
	[SerializeField]
	private FloatVariable otherTrigger;
	[SerializeField]
	private BooleanVariable active;
	[SerializeField]
	private BooleanVariable otherTriggerActiveState;
	[SerializeField]
	private DeflectorProperties deflectorProperties;
	[SerializeField]
	private GameEvent OnPerfectDeflection;
	[SerializeField]
	private GameEvent OnPoseStarted;
	[SerializeField]
	private GameEvent addScore;
	[SerializeField]
	private float minScoreAddition = 100f;
	[SerializeField]
	private float maxScoreAddition = 250f;


	private Camera mainCamera;
	private CapsuleCollider coll;
	private float maxRadius;
	private bool mainTriggerPreviousState = false;
	private bool otherTriggerPreviousState = false;
	private float startDeflectionTimestamp = 0f;

	private void Awake()
    {
		mainCamera = Camera.main;
        coll = GetComponent<CapsuleCollider>();
		maxRadius = coll.radius;
		playerState.airDeflect = false;
    }

	private void FixedUpdate() {
		if (disableInput.Value || playerState.isPlayerDead) { active.Value = false; return; };
		
		active.Value = 	playerState.sliding == false && 
						playerState.throwingObject == false && 
						playerState.throwingGrenade == false && 
						playerState.meleeing == false && 
						stamina.Value > 0f && 
						((mainTrigger.Value != 0 && otherTrigger.Value == 0) || (mainTrigger.Value != 0 && otherTrigger.Value != 0 && mainTriggerPreviousState)) && 
						otherTriggerActiveState.Value == false && otherTriggerPreviousState == false;
						
		// First frame of deflection
		if (active.Value && mainTriggerPreviousState == false) {
			OnPoseStarted.Raise(this);
			startDeflectionTimestamp = Time.time;
			if (playerState.jumping || playerState.grounded == false) playerState.airDeflect = true;
		}

		// The frame the player let go off the trigger
		if (active.Value == false && mainTriggerPreviousState == true) {
			playerState.airDeflect = false;
		}	

		coll.enabled = active.Value;
		playerState.deflecting = active.Value || otherTriggerActiveState.Value;
		mainTriggerPreviousState = active.Value;
		otherTriggerPreviousState = otherTriggerActiveState.Value;
	}

	private void EvaluateDeflection(Bullet bullet)
    {
		if (Time.time - deflectorProperties.fixedTimeWindow > startDeflectionTimestamp) {
			bullet.DeflectRandomly();
			return;
		}
		
		float distance = Vector3.Magnitude(bullet.transform.position - transform.position);
		if (distance <= deflectorProperties.maxDistanceToPerfectDeflection) {
			distance = Mathf.Clamp(distance, deflectorProperties.minDistanceToPerfectDeflection, deflectorProperties.maxDistanceToPerfectDeflection);
			float multiplier = 1f -distance.Normalize(deflectorProperties.minDistanceToPerfectDeflection, deflectorProperties.maxDistanceToPerfectDeflection);
			bullet.DeflectPerfeclty(multiplier);
			OnPerfectDeflection.Raise(multiplier);
			addScore.Raise(multiplier.Denormalize(minScoreAddition, maxScoreAddition));
		} else {
			bullet.DeflectRandomly();
		}
    }

	private void OnTriggerEnter(Collider other) {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet != null && bullet.bulletType == bulletTypeToDeflect && bullet.bouncedByPlayer == false) {
			EvaluateDeflection(bullet);
        }
    }
}
