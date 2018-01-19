using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaio : MonoBehaviour
{
	public LayerUtil[] layerPrioridade = {
		LayerUtil.Walkable,
		LayerUtil.Enemy,
	};

	float distanciaMaxMap = 100f;
	Camera camPrincipal;

	RaycastHit m_hit;
	public RaycastHit hit { get { return m_hit; } }

	LayerUtil m_layerHit;
	public LayerUtil layerHit { get { return m_layerHit; } }

	void Start()
	{
		camPrincipal = Camera.main;
	}

	void Update()
	{
		foreach (LayerUtil layer in layerPrioridade) 
		{
			var t_hit = ChamarLayer (layer);
			if (t_hit.HasValue) 
			{
				m_hit = t_hit.Value;
				m_layerHit = layer;

				return;
			}
		}

		m_hit.distance = distanciaMaxMap;
		m_layerHit = LayerUtil.vaziu;
	}

	RaycastHit? ChamarLayer(LayerUtil layer)
	{
		int layerMask = 1 << (int)layer;

		Ray raio = camPrincipal.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		bool hitExiste = Physics.Raycast (raio, out hit, distanciaMaxMap, layerMask);

		if (hitExiste)
		{
			return hit;
		}

		return null;
	}
}
