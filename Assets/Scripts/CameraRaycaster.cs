using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
	public Layer[] layerPriorities = {
		Layer.Enemy,
		Layer.Walkable
	};

	float distanceToBackground = 100f;
	Camera viewCamera;

	RaycastHit v_hit;
	public RaycastHit hit
	{
		get { return v_hit; }
	}

	Layer v_layerHit;
	public Layer layerHit
	{
		get { return v_layerHit; }
	}

	public delegate void LayerHand(Layer d_layerHit);
	public event LayerHand layerHandObs;

	void Start()
	{
		viewCamera = Camera.main;
	}

	void Update()
	{
		// Look for and return priority layer hit
		foreach (Layer layer in layerPriorities)
		{
			var hit = RaycastForLayer(layer);
			if (hit.HasValue)
			{
				v_hit = hit.Value;

				if (v_layerHit != layer)
				{
					v_layerHit = layer;
					layerHandObs (layer);
				}
				return;
			}
		}

		// Otherwise return background hit
		v_hit.distance = distanceToBackground;
		v_layerHit = Layer.RaycastEndStop;
		layerHandObs (v_layerHit);
	}

	RaycastHit? RaycastForLayer(Layer layer)
	{
		int layerMask = 1 << (int)layer; // See Unity docs for mask formation
		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit; // used as an out parameter
		bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
		if (hasHit)
		{
			return hit;
		}
		return null;
	}
}