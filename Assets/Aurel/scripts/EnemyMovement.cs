using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public enum StateEnum
	{
		idle,
		follow
	}
	public StateEnum currentState = StateEnum.idle;
	public float sightRadius = 50.0f;

	private Vector3 movement;
	private Rigidbody enemyRigidbody;
	private Transform player;
	private NavMeshAgent nav;

	// Use this for initialization
	void Start () {
		enemyRigidbody = GetComponent<Rigidbody>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		nav = GetComponent <NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState == StateEnum.idle)
		{
			// Verify if player is in range
			Collider[] cols = Physics.OverlapSphere(transform.position, sightRadius);

			foreach(Collider col in cols)
			{
				if (col.tag == "Player")
				{
					currentState = StateEnum.follow;
					return;
				}
			}

			if (Random.Range(0, 100) > 90)
				ChangeDirection();

			nav.SetDestination(movement);
		}
		else
		{
			if (player && nav)
			{
				nav.SetDestination(player.position);
			}
			else
			{
				currentState = StateEnum.idle;
			}
		}
	}

	private void ChangeDirection()
	{
		movement = new Vector3(Random.Range(70, 400), 10.0f, Random.Range (30, 330));
	}
}
