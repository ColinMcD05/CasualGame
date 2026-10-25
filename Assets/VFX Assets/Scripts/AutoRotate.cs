using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;
	[SerializeField] private Vector3 rotationAxis = Vector3.up;
	
	private void Update()
	{
		transform.Rotate(rotationAxis * (rotationSpeed * Time.deltaTime), Space.Self);
	}
}
