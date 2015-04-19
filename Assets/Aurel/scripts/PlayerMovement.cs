using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 60.0f;
	public float turnSmoothing = 15f;
	public bool canMove = true;

	private Vector3 movement;
	private Rigidbody playerRigidbody;
	private PlayerAttack playerAttack;
	private float recordedH = 0.0f;
	private float recordedV = 0.0f;
	private bool recorded = false;

	// Use this for initialization
	void Start () 
	{
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if (!canMove)
			return;

		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		if (recorded)
		{
			if (h != recordedH || v != recordedV)
				recorded = false;
		}

		if (!recorded)
		{
			Move (h, v);

			if (h != 0 || v != 0)
				Turn(h, v);	
		}
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0.0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		if (playerRigidbody)
			playerRigidbody.MovePosition(transform.position + movement);
		else
			Debug.Log("Please attach a RigdBody to your Player GameObject");
	}
	
	void Turn(float h, float v) 
	{
		Vector3 targetDirection = new Vector3(h, 0f, v);
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(playerRigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);

		if(playerRigidbody)
			playerRigidbody.MoveRotation(newRotation);
		else
			Debug.Log("Please attach a RigdBody to your Player GameObject");
	}

	public void TreeInRange(bool inRange)
	{
		recordedH = Input.GetAxisRaw ("Horizontal");
		recordedV = Input.GetAxisRaw ("Vertical");
		recorded = inRange;
	}
}