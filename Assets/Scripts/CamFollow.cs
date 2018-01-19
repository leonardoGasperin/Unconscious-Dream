using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
	[SerializeField]Vector3 defaultDist = new Vector3 (0f, 7, -10);//seta uma distancia padrao para camera
	[SerializeField]float velDamp = 2f;//distancia maxima do afastamento da camera

	GameObject playerPrefab;
	Vector3 velocity = Vector3.one;

	void Awake()
	{
		playerPrefab = GameObject.FindGameObjectWithTag ("Player");
	}

	//Update apos FixedUpdate e Update
	void LateUpdate()
	{
		SmoothFollow ();
	}

	void SmoothFollow()
	{
		Vector3 toPos = playerPrefab.transform.position + (playerPrefab.transform.rotation * defaultDist);//um vetor para indicar a posisao do target e onde a camera dee parar
		transform.position = Vector3.SmoothDamp (transform.position, toPos, ref velocity ,velDamp);//um vertor smoothdamp para mandar a camera se mover
		transform.LookAt (playerPrefab.transform.position, playerPrefab.transform.up);//manda a camera rotacionar
	}
}
