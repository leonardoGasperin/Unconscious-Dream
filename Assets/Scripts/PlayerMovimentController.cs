using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovimentController : MonoBehaviour
{
	[Range(0f,1f)][SerializeField]float radDistance = 1f;//stop the character when close to click walk
	[Range(0f,10f)][SerializeField]float rangeAttack = 5f;//stop the character when close to click walk
	ThirdPersonCharacter character;// A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
	bool controllerMode = false;//check if is mouse&keyboard or gamepad
	bool jump;
	bool crounch;
	Vector3 currentDestination, clickPoint;

	private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
		currentDestination = transform.position;
    }

	private void Update()
	{
		if (Input.GetButton ("Jump")) {
			jump = true;
		} else
			jump = false;
		if (Input.GetKey (KeyCode.JoystickButton1) || Input.GetKey (KeyCode.C)) {
			crounch = true;
		} else
			crounch = false;
	}

    // Fixed update is called in sync with physics
    private void FixedUpdate()
	{
		if(Input.GetKeyDown(KeyCode.P))//TODO this to change from keyboard&mouse to gamepas and add it to menu 
		{//if false mouse&keyboard if true gamepad
			controllerMode = !controllerMode;
			currentDestination = transform.position;
		}
		if (controllerMode)
		{
			print ("Enter on gamepad mode");
			GamePadControl ();
		} else
		{
			print ("enter um keyboard & mouse mode");
			MouseNKeyboard ();
		}
	}

	private void GamePadControl()
	{
		float h = Input.GetAxis("HorizontalGP");
		float v = Input.GetAxis("VerticalGP");
		// calculate camera relative direction to move:
		Vector3 camFoward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 move = v*camFoward + h*Camera.main.transform.right;
		
		character.Move (move, crounch, jump);
	}

	void MouseNKeyboard()
	{
		
		if (Input.GetMouseButton (0))
		{
			clickPoint = cameraRaycaster.hit.point;
			switch (cameraRaycaster.layerHit)
			{
			case Layer.Walkable:
				currentDestination = Destination (clickPoint, radDistance);
				break;

			case Layer.Enemy:
				currentDestination = Destination (clickPoint, rangeAttack);
				break;

			default:
				print ("Not to be here! ");
				return;
			}
		}

		MoveToClick ();

		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			KeyBoardMove ();
		}
	}

	void MoveToClick()
	{
		var playerToClickPoint = (currentDestination - transform.position);
		if (playerToClickPoint.magnitude >= radDistance)
			character.Move (playerToClickPoint, crounch, jump);
		else
			character.Move (Vector3.zero, crounch, jump);
	}

	void KeyBoardMove()
	{
		currentDestination = transform.position;
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 move = v*camForward + h*Camera.main.transform.right;

		character.Move (move, crounch, jump);
	}

	Vector3 Destination(Vector3 destination, float shortening)
	{
		Vector3 reductionVector = (destination - transform.position).normalized * shortening;
		return destination - reductionVector;
	}

	//Draw Gizmo to see on Game visio when running game
	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;//change gizmo color
		Gizmos.DrawLine (transform.position, currentDestination);//draw a line being on transform.position to the current click
		Gizmos.DrawSphere (currentDestination, 0.15f);//draw a sphere on current click
		Gizmos.DrawSphere (clickPoint, 0.1f);

		//drow ranged attack distance sphere
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, rangeAttack);
	}
}
