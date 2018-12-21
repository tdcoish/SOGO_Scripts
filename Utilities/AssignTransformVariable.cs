using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTransformVariable : MonoBehaviour {

	[SerializeField]
	private TransformVariable transformVariable;

	private void Awake()
	{
		transformVariable.Value = transform;
	}
}
