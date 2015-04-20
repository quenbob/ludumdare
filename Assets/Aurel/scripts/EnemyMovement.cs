﻿using UnityEngine;
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
	private TimerManager timeManager;
	private ParticleSystem fireParticleSystem;
	private EnemyRunningAnimation enemyRunningAnimation;

	public float timeToFlameUp = 1.0f;
	public float timeBetweenLoops = 3.0f;
	private AudioSource[] flameAudio;
	private float soundTimer;
	private bool isPlayingSound = false;
	private bool hasFlamedUp = false;

	public bool isOnFire() {
		return (currentState == StateEnum.follow);
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		timeManager = GameObject.Find("Managers").GetComponent<TimerManager>();
		nav = GetComponent <NavMeshAgent>();

		fireParticleSystem = transform.Find ("Fire Continuous").GetComponent<ParticleSystem> ();
		fireParticleSystem.Pause ();

		enemyRunningAnimation = GetComponent<EnemyRunningAnimation> ();
		enemyRunningAnimation.startRunning ();

		flameAudio = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timeManager.isPaused && nav.isActiveAndEnabled)
		{
			nav.enabled = false;
			return;
		}
		else if (!nav.isActiveAndEnabled && currentState != StateEnum.idle)
		{
			nav.enabled = true;
		}

		if (currentState != StateEnum.follow)
		{
			// Verify if player is in range
			Collider[] cols = Physics.OverlapSphere(transform.position, sightRadius);

			foreach(Collider col in cols)
			{
				if (col.tag == "Player")
				{
					currentState = StateEnum.follow;
					fireParticleSystem.Play();
					enemyRunningAnimation.startRunning();

					isPlayingSound = true;
					soundTimer = 0;
					flameAudio[0].Play ();

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
				fireParticleSystem.Stop();
				enemyRunningAnimation.stopRunning();
				Stop();
			}
		}

		soundTimer += Time.deltaTime;
		if (currentState == StateEnum.follow && isPlayingSound) {
			if (!hasFlamedUp) {
				if (soundTimer >= timeToFlameUp) {
					hasFlamedUp = true;
					soundTimer = 0;
					flameAudio[1].Play ();
				}
			} else {
				if (soundTimer >= timeBetweenLoops) {
					soundTimer = 0;
					flameAudio[1].Play ();
				}
			}
		} else {
			isPlayingSound = false;
			hasFlamedUp = false;
			flameAudio[1].Stop();
		}
	}

	private void ChangeDirection()
	{
		currentState = StateEnum.walk;
		fireParticleSystem.Stop();
		enemyRunningAnimation.startRunning();

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
