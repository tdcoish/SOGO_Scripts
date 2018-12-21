/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class CameraOrbit : MonoBehaviour {
    
    [SerializeField]
    private BooleanVariable disableInput;
    [SerializeField]
    private InputState input;
    [SerializeField]
    private float xRotationSpeed = 5f;
    [SerializeField]
    private float maxCameraAngle = 4.0f;
    [SerializeField]
    private float minCameraAngle = 0.5f;
    [SerializeField]
    private FloatVariable normalizedYCamPos;
    
    [SerializeField]
    private float zoomInSpeed = 10f;
    [SerializeField]
    private float minFieldOfView = 38f;
    private float regularFieldOfView;
    private bool isZooming = false;

    private float rotX;
    private CinemachineVirtualCamera vCam;
    private CinemachineOrbitalTransposer cinemachineOrbitalTransposer;
    
    // Post Processing Volume Options
    [SerializeField]
    private PostProcessProfile ppp;
    private MotionBlur motionBlur;
    private ChromaticAberration chromaticAberration;

    private void Awake()
    {
        rotX = 1.75f;
        vCam = GetComponent<CinemachineVirtualCamera>();
        cinemachineOrbitalTransposer = vCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        regularFieldOfView = vCam.m_Lens.FieldOfView;

        ppp.TryGetSettings(out motionBlur);
        ppp.TryGetSettings(out chromaticAberration);
    }
    
    private void LateUpdate()
    {
        if (disableInput.Value) return;
        
        HandleRotation();


        if (input.rightStickPressed) {
            cinemachineOrbitalTransposer.m_RecenterToTargetHeading.m_enabled = true;
            Invoke("DisableCentering", cinemachineOrbitalTransposer.m_RecenterToTargetHeading.m_RecenteringTime);
        }
    }

    private void HandleRotation() {
        cinemachineOrbitalTransposer.m_XAxis.m_InvertAxis = input.invertXAxis;
        
        float rightStickY = input.invertYAxis ? input.rightStickY * -1 : input.rightStickY;
        rotX += rightStickY * xRotationSpeed * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, minCameraAngle, maxCameraAngle);
        Vector3 newOffset = cinemachineOrbitalTransposer.m_FollowOffset;
        newOffset.y = rotX;
        cinemachineOrbitalTransposer.m_FollowOffset = newOffset;
        normalizedYCamPos.Value = 1f - cinemachineOrbitalTransposer.m_FollowOffset.y.Normalize(minCameraAngle, maxCameraAngle);
    }

    private void DisableCentering() {
        cinemachineOrbitalTransposer.m_RecenterToTargetHeading.m_enabled = false;
    }

    public void CameraZoomIn (object data) {
		float multiplier = (float) data;
        if (isZooming == false) {
		    StartCoroutine(ZoomIn());
        }
	}

    IEnumerator ZoomIn() {
        isZooming = true;
        
        motionBlur.shutterAngle.Override(270f);
        chromaticAberration.intensity.Override(1f);
        while (vCam.m_Lens.FieldOfView > minFieldOfView) {
            vCam.m_Lens.FieldOfView -= zoomInSpeed * Time.deltaTime;
            yield return null;
        }

        while (vCam.m_Lens.FieldOfView <= regularFieldOfView) {
            vCam.m_Lens.FieldOfView += zoomInSpeed * Time.deltaTime;
            yield return null;
        }

        motionBlur.shutterAngle.Override(0f);
        chromaticAberration.intensity.Override(0f);
        vCam.m_Lens.FieldOfView = regularFieldOfView;
        isZooming = false;
        yield break;
    }
}