/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System.Collections;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [SerializeField]
	private PlayerState playerState;
	[SerializeField]
    private BooleanVariable disableInput;
	[SerializeField]
	private InputState input;
	[SerializeField]
	private BooleanVariable canSlide;
    [SerializeField]
	private FloatVariable slideStatus;
	
	[SerializeField]
    internal float slideSpeed = 1.5f;
    [SerializeField]
    private float slideUseSpeedRate = 0.05f;
    [SerializeField]
    private float slideRecoveryRate = 0.05f;
	[SerializeField]
    private float slideCooldownTime = 2f;
	[SerializeField]
    private float minimumSlideTime = 1f;

	[Space, Header("Slide Events")]
	[SerializeField]
	private GameEvent OnSlideStart;
	[SerializeField]
	private GameEvent OnSlideReplentish;

	[Space, Header("Points")]
	[SerializeField]
	private GameEvent addScore;
	[SerializeField]
	private float pointsForSliding = 20f;

	private float lastSlideTimestamp = 0f;
	private bool recoveringSlide = false;
	private Rigidbody rb;
	private Grounded grounded;
	private OilTrail oilTrail;

	private AD_Slide slideAudioComp;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		grounded = GetComponentInChildren<Grounded>();
		oilTrail = GetComponentInChildren<OilTrail>();
		slideAudioComp = GetComponent<AD_Slide>();
		Reset(null);
	}

	private void Update() {
		if (disableInput.Value) return;
		
		if (input.rightBumperPressed && 
			playerState.sliding == false && 
			canSlide.Value == true && 
			grounded.IsGrounded() && 
			playerState.jumping == false &&
			playerState.deflecting == false &&
			playerState.throwingObject == false &&
			playerState.throwingGrenade == false &&
			playerState.meleeing == false) {
			SetSlidingStartState();
		}

		if (playerState.sliding) {
			HandleSliding();
		}

		if (recoveringSlide) {
			RecoverSlide();
		}
	}

	private void SetSlidingStartState() {
		playerState.sliding = true;
		canSlide.Value = false;
		lastSlideTimestamp = Time.time;
		OnSlideStart.Raise(null);
		oilTrail.StartDropParticles();
		slideAudioComp.HandleStartSliding();
		addScore.Raise(pointsForSliding);
	}

	public void HandleSliding() {
		
		PerformSlide();

		if (Time.time < lastSlideTimestamp + minimumSlideTime) return;

		if (grounded.IsGrounded() == false || input.rightBumperHold == false || slideStatus.Value <= 0f || playerState.jumping || playerState.isPlayerDead) {
			FinishSlide();
		}
	}

	private void PerformSlide() {
		oilTrail.DropSweat();
		slideStatus.Value -= slideUseSpeedRate * Time.deltaTime;
	}

	private void RecoverSlide() {
		if (slideStatus.Value < 1f) {
			slideStatus.Value += slideRecoveryRate * Time.deltaTime;
			if (slideStatus.Value > 1f) slideStatus.Value = 1f;
			return;
		}

		recoveringSlide = false;
		canSlide.Value = true;
		OnSlideReplentish.Raise(null);
	}

	private void FinishSlide() {
		slideStatus.Value = 0f;
		canSlide.Value = false;
		playerState.sliding = false;
		lastSlideTimestamp = 0f;
		recoveringSlide = true;
		oilTrail.StopDropParticles();
		slideAudioComp.HandleStopSliding();
	}

	// The player was sliding when he died
	public void InterruptSlide(object data) {
		if (playerState.sliding) {
			FinishSlide();
		}
	}

	public void Reset(object data) {
		recoveringSlide = false;
		canSlide.Value = true;
		playerState.sliding = false;
		slideStatus.Value = 1f;
		lastSlideTimestamp = 0f;
		slideAudioComp.HandleStopSliding();
		StartCoroutine(ActivateSlideUI());
	}

	private IEnumerator ActivateSlideUI() {
		yield return null;
		OnSlideReplentish.Raise(null);
		yield break;
	}
}