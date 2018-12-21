/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System;
using System.Collections;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    
    [SerializeField]
    private PlayerState playerState;
    [SerializeField]
    private BooleanVariable disableInput;
    [SerializeField]
    private InputState input;
    [SerializeField]
    private GrenadeLauncherProperties grenadeLauncherProperties;
    [SerializeField]
    private FloatVariable throwingForce;
    [SerializeField]
    private GameObject grenadePrefab;
    [SerializeField]
    private GameEvent onPlayerAim;
    [SerializeField]
    private bool debugMode = false;
    [SerializeField]
    private BooleanVariable isChargingThrow;
    [SerializeField]
    private FloatVariable lastGrenadeThrowTimestamp;
    [SerializeField]
    private BooleanVariable canThrowGrenade;
    [SerializeField]
    private SO_AD_Dialogue  throwDialogue;
    [SerializeField]
    private StringVariable      Player;
    [SerializeField]
    private GameEvent onGrenadeThrown;
    [SerializeField]
    private GameEvent onGrenadeAvailable;
    [SerializeField]
    private FloatVariable camYPos;
    [SerializeField]
    private FloatVariable grenadeCooldownStatus; 


    private Camera mainCamera;
    private PlayerAnimationController animationController;
    private GameObject grenadeInstance = null;

    private void Awake(){
        animationController = GetComponentInParent<PlayerAnimationController>();
        isChargingThrow.Value = playerState.throwingGrenade = false;
        throwingForce.Value = 0f;
        lastGrenadeThrowTimestamp.Value = 0f;
        canThrowGrenade.Value = true;
        mainCamera = Camera.main;
    }

    private void Update() {
        if (disableInput.Value) return;
        
        if (isChargingThrow.Value == false && canThrowGrenade.Value == false) {
            if (Time.time >= lastGrenadeThrowTimestamp.Value + grenadeLauncherProperties.coolDownTime) {
                grenadeCooldownStatus.Value = 1.1f;
                canThrowGrenade.Value = true;
                lastGrenadeThrowTimestamp.Value = 0f;
                onGrenadeAvailable.Raise(null);
                AUD_Manager.PostEvent("IG_UI_Oil_ST", gameObject);
            } else {
                grenadeCooldownStatus.Value = Time.time.Normalize(lastGrenadeThrowTimestamp.Value, lastGrenadeThrowTimestamp.Value + grenadeLauncherProperties.coolDownTime);
            } 
        }

        if (input.leftBumperPressed && canThrowGrenade.Value == true && isChargingThrow.Value == false && playerState.deflecting == false && playerState.sliding == false && playerState.throwingObject == false && playerState.meleeing == false) {
            StartThrowingGrenade();
        }
        
        ChargeGrenadeThrow();
    }

    private void StartThrowingGrenade() {
        animationController.PlayChargeGrenadeAnimation();
        isChargingThrow.Value = playerState.throwingGrenade = true;
        canThrowGrenade.Value = false;
		AUD_Manager.PostEvent("PC_OilPre_ST_PL", gameObject);

        grenadeInstance = Instantiate(grenadePrefab);
        grenadeInstance.transform.SetParent(gameObject.transform);
        grenadeInstance.transform.localPosition = Vector3.zero;
        grenadeInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);

        transform.Rotate(-grenadeLauncherProperties.throwingAngle, 0f, 0f);
        throwingForce.Value = grenadeLauncherProperties.minThrowingForce;
        grenadeInstance.GetComponent<TanningOilGrenade>().throwingForce = grenadeLauncherProperties.minThrowingForce;
        onPlayerAim.Raise(true);
    }

    private void ChargeGrenadeThrow() {
        if (isChargingThrow.Value == false) return;
        
        if (input.leftBumperHold) {
            throwingForce.Value = camYPos.Value.Denormalize(grenadeLauncherProperties.minThrowingForce, grenadeLauncherProperties.maxThrowingForce);
        } else {
            ThrowGrenade();
        }
    }

    private void ResetOriginalState() {
        isChargingThrow.Value = playerState.throwingGrenade = false;
        throwingForce.Value = 0f;
        transform.rotation = transform.parent.rotation;
        lastGrenadeThrowTimestamp.Value = Time.time;
    }

    private void ThrowGrenade() {
        animationController.PlayThrowObjectAnimation();
        onPlayerAim.Raise(false);
        onGrenadeThrown.Raise(null);
        grenadeInstance.GetComponent<TanningOilGrenade>().Throw(throwingForce.Value, transform.forward);
        grenadeInstance = null;
        ResetOriginalState();
        AUD_Manager.PostEvent("PC_OilThrowWhoosh_ST_PL", gameObject);
        AUD_Manager.PlayerDialogue(throwDialogue, gameObject, Player.Value);
    }

    public void DropGrenade(object data) {
        if (playerState.throwingGrenade) {
            onPlayerAim.Raise(false);
            onGrenadeThrown.Raise(null);
            if (grenadeInstance != null) {
                grenadeInstance.GetComponent<TanningOilGrenade>().Throw(0f, transform.forward);
                grenadeInstance = null;
            }
        }
        ResetOriginalState();
    }

    private void OnDrawGizmos()
    {
        if (debugMode == false) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward * 2);        
    }

    public void ActivateGrenade(object data) {
        canThrowGrenade.Value = true;
        lastGrenadeThrowTimestamp.Value = 0f;
        StartCoroutine(ActivateGrenadeUI());
    }

    private IEnumerator ActivateGrenadeUI() {
        yield return null;
        onGrenadeAvailable.Raise(null);
        yield break;
    }
}