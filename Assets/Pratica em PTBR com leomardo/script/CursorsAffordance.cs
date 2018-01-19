using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorsAffordance : MonoBehaviour
{
	CameraRaio mouseRaio;

	void Start()
	{
		mouseRaio = GetComponent<CameraRaio> ();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Debug.Log(mouseRaio.layerHit);
		}
	}
}
