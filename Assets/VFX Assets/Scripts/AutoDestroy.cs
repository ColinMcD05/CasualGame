using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float destroyTime = 1f;
	
	private void Start()
	{
		Destroy(gameObject, destroyTime);
	}
}
