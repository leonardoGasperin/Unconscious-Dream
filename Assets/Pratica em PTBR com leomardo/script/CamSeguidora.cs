using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSeguidora : MonoBehaviour
{
	[SerializeField]Vector3 distOriginal = new Vector3 (0f, 1.75f, -3.25f);//distancia do alvo
	[SerializeField]float camProfundidade = 2f;//profundidade e velocidade de rotaćao

	GameObject alvo;//instanciaćao do alvo
	Vector3 velocidade = Vector3.one;//referencia de velocidade (1/unid.)

	void Start()
	{
		alvo = GameObject.FindGameObjectWithTag ("Player");//atribui instancia
	}

	void LateUpdate()
	{
		SeguirSuave ();
	}

	void SeguirSuave()
	{
		Vector3 destino = alvo.transform.position + (alvo.transform.rotation * distOriginal);

		transform.position = Vector3.SmoothDamp (transform.position, destino, ref velocidade, camProfundidade);
		transform.LookAt (alvo.transform, alvo.transform.up);
	}
}
