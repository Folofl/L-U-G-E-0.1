using UnityEngine;
using System.Collections;

public class BoardCharacter : MonoBehaviour {

	// Use this for initialization

	public Transform RightBound;
	public Transform LeftBound;
	public float MAX_ROTATION = 45;
	public float speed = 1;
	public float turnSpeed = .5f;

	private float cameraOffset;
	private float rightBound;
	private float leftBound;

	private Transform mainCamera;

	void Start () {
		leftBound = LeftBound.position.x;
		rightBound = RightBound.position.x;
		mainCamera = Camera.main.transform;
		cameraOffset = mainCamera.position.y - transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 position = transform.position;
		Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);

		transform.Translate (Vector3.up * speed* Time.deltaTime);

		if ((position.x > leftBound && direction.x < 0) || (direction.x > 0 && position.x < rightBound)) {
			transform.Translate (direction * turnSpeed, Space.World);
			transform.Rotate(Vector3.up * direction.x * MAX_ROTATION);
		}

		RaycastHit hit = new RaycastHit ();

		if (Physics.Raycast (position, Vector3.forward, out hit, 2, 1 << 12)) { 
			Vector3 newPosition = transform.position;
			newPosition.z = hit.point.z - .2f;
			transform.position = newPosition;
		}

		Vector3 camPos = new Vector3(0, transform.position.y + cameraOffset, mainCamera.position.z);
		mainCamera.position = camPos;
	} 
}
