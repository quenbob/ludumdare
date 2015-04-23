using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 60.0f;
	public float turnSmoothing = 15f;
	public bool canMove = true;
	public float accelerometerSpeed = 1.5f;
	public float accelerometerDeadZone = 0.075f;

	private Vector3 movement;
	private Rigidbody playerRigidbody;
	private PlayerAttack playerAttack;
	private float recordedH = 0.0f;
	private float recordedV = 0.0f;
	private bool recorded = false;
	private Animator anim;
	private TimerManager timeManager;

	// Use this for initialization
	void Start () 
	{
		playerRigidbody = GetComponent<Rigidbody>();

		GameObject managers = GameObject.Find("Managers");
		if (managers)
			timeManager = GameObject.Find("Managers").GetComponent<TimerManager>();

		anim = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if (!canMove || (timeManager && timeManager.isPaused)) {
			anim.SetBool ("isWalking", false);
			return;
		}

		updateMove();
	}
	
	void updateMove() {
		float h;
		float v;

		if (Input.acceleration.x != 0) {
			h = Input.acceleration.x;
			v = Input.acceleration.y;

			if((h > 0 && h < accelerometerDeadZone) || (h < 0 && h > -accelerometerDeadZone)) {
				h = 0f;
			}
			
			if((v > 0 && v < accelerometerDeadZone) || (v < 0 && v > -accelerometerDeadZone)) {
				v = 0f;
			}
		} else {
			h = Input.GetAxisRaw("Horizontal");
			v = Input.GetAxisRaw("Vertical");
		}

		if (recorded) {
			if (h != recordedH || v != recordedV) {
				recorded = false;
			}
		}
		
		if (!recorded) {
			Move (h, v);
			
			if (h != 0 || v != 0) {
				Turn(h, v);
			}
		}
		
		anim.SetBool ("isWalking", ((h != 0.0f || v != 0.0f) & !recorded));
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0.0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		if (playerRigidbody) {
			playerRigidbody.MovePosition (transform.position + movement);
		} else {
			Debug.Log ("Please attach a RigdBody to your Player GameObject");
		}
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