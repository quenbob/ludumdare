using UnityEngine;
using System.Collections;

public class BeaverMovement : MonoBehaviour {
	public PlayerAttack playerAttack;
	public float speed = 10.0f;

	private float angle = 0.0f;
	private bool direction = false;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		if(playerAttack && playerAttack.treeInRange)
		{
			Move ();
		}
		else
		{
			StopMove();
		}

		transform.localRotation = Quaternion.AngleAxis(angle, Vector3.right);
	}

	void Move()
	{
		if (direction)
		{
			angle += speed;

			if (angle >= 45.0f)
				direction = !direction;
		}
		else
		{
			angle -= speed;

			if (angle <= -45.0f)
				direction = !direction;
		}
	}

	void StopMove()
	{
		angle = 0.0f;
	}
}
