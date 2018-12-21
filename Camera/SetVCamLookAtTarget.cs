using UnityEngine;
using Cinemachine;

public class SetVCamLookAtTarget : MonoBehaviour
{

	[SerializeField]
	private TransformVariable transformToLookAt;

	private void Start()
	{
		GetComponent<CinemachineVirtualCamera>().LookAt = transformToLookAt.Value;
	}

}
