using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public enum StateEnum
	{
		idle,
		walk,
		follow
	}
	public StateEnum currentState = StateEnum.idle;
	public float sightRadius = 50.0f;

	private Vector3 movement;
	private Transform player;
	private NavMeshAgent nav;
	private float timeSinceLastChange = 0.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		nav = GetComponent <NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState != StateEnum.follow)
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

			if (timeSinceLastChange > 3.0f)
			{
			int whatNext = Random.Range(0, 100);
			if (whatNext > 95)
				ChangeDirection();
			else if (whatNext < 5)
				Stop();
			}
			else
			{
				timeSinceLastChange += Time.deltaTime;
			}

			if (nav.isActiveAndEnabled)
				nav.SetDestination(movement);
		}
		else
		{
			if (!nav.isActiveAndEnabled)
				nav.enabled = true;

			// Verify if player is still in range
			Collider[] cols = Physics.OverlapSphere(transform.position, sightRadius);

			bool found = false;
			foreach(Collider col in cols)
			{
				if (col.tag == "Player")
				{
					found = true;
					break;
				}
			}

			if (found && player && nav)
			{
				nav.SetDestination(player.position);
			}
			else
			{
				currentState = StateEnum.idle;
				Stop();
			}
		}
	}

	private void ChangeDirection()
	{
		if (!nav.isActiveAndEnabled)
			nav.enabled = true;
		movement = new Vector3(Random.Range(70, 400), transform.position.y, Random.Range (30, 330));

		timeSinceLastChange = 0.0f;
	}

	private void Stop()
	{
		if (nav.isActiveAndEnabled)
			nav.enabled = false;

		timeSinceLastChange = 0.0f;
	}
}
