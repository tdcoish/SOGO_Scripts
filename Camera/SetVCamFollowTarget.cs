using UnityEngine;
using Cinemachine;

public class SetVCamFollowTarget : MonoBehaviour
{

	[SerializeField]
	private TransformVariable transformToFollow;

	private void Start()
	{
		GetComponent<CinemachineVirtualCamera>().Follow = transformToFollow.Value;
	}

}
