using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorsAfford : MonoBehaviour
{
	[SerializeField]Texture2D t2D_wCursors = null;//walk and normal cursor(just for now)
	[SerializeField]Texture2D t2D_eCursors = null;//attack cursor
	[SerializeField]Texture2D t2D_eosCursors = null;//EoS End of Scene ba dum tss

	CameraRaycaster cameraRaycastCheck;
	Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Awake ()
	{
		cameraRaycastCheck = GetComponent<CameraRaycaster> ();
		cameraRaycastCheck.layerHandObs += CursorMarker;
	}

	void CursorMarker (Layer marker)
	{
		switch (marker)
		{
		case Layer.Walkable:
			print (cameraRaycastCheck.layerHit.ToString ());
			Cursor.SetCursor (t2D_wCursors, hotSpot, CursorMode.Auto);
			break;

		case Layer.Enemy:
			print (cameraRaycastCheck.layerHit.ToString ());
			Cursor.SetCursor (t2D_eCursors, hotSpot, CursorMode.Auto);
			break;

		case Layer.RaycastEndStop:
			print (cameraRaycastCheck.layerHit.ToString ());
			Cursor.SetCursor (t2D_eosCursors, hotSpot, CursorMode.Auto);
			break;

		default:
			Debug.LogError ("ERROR!");
			return;
		}
	}
}
