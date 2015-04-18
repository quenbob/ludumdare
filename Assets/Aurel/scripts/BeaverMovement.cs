using UnityEngine;
using System.Collections;

public class BeaverMovement : MonoBehaviour {
	public PlayerAttack playerAttack;

	private float angle = 0.0f;
	private bool direction = false;

	// Use this for initialization
	void Start () {
		playerAttack = transform.Find("../Player").gameObject.GetComponent<PlayerAttack>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerAttack.treeInRange)
		{

		}
	}

	void Move()
	{
		if (direction)
		{
			angle += 5.0f;
		}
		else
		{
			angle -= 5.0f;
		}
	}

	void StopMove()
	{

	}
}
