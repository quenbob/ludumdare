using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour 
{
	GameObject player;
	public bool playerInRange;
	float timer;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
		{
			Debug.Log ("stop fire");
			playerInRange = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerInRange)
		{
			TimerManager timerManager = GameObject.Find ("Managers").GetComponent<TimerManager>();
			timerManager.isGameOver = true;
		}
	}
}
