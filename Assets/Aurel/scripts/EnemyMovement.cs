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
	public float timeMinBetweenDirectionChanges = 2.0f;
	public float timeMinBeforeMoveWhenIdle = 1.0f;
	public float chanceToTurnIfItIsMoving = 5.0f;
	private float mchanceToTurnIfItIsMoving
	{
		get { return 100.0f - chanceToTurnIfItIsMoving;}
	}

	public float chanceToMoveAfterCreation = 50.0f;
	private float mchanceToMoveAfterCreation
	{
		get { return 100.0f - chanceToMoveAfterCreation;}
	}

	public float chanceToMoveItItIsIdle = 33.0f;
	private float mchanceToMoveItItIsIdle
	{
		get { return 100.0f - chanceToMoveItItIsIdle;}
	}

	public float chanceToStopItItIsMoving = 3.0f;
	private float mchanceToStopItItIsMoving
	{
		get { return chanceToStopItItIsMoving;}
	}

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
	private bool firstMove = true;

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

		// Enemy starts idle
		enemyRunningAnimation = GetComponent<EnemyRunningAnimation> ();
		Stop();

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
					enemyRunningAnimation.startRunning(true);

					isPlayingSound = true;
					soundTimer = 0;
					flameAudio[0].Play ();

					return;
				}
			}

			// We wait at least 3 sec between two state or destination changes, except if it is its first move
			if ((timeSinceLastChange > timeMinBetweenDirectionChanges && currentState == StateEnum.walk) || (timeSinceLastChange > timeMinBeforeMoveWhenIdle && currentState == StateEnum.idle) || firstMove)
			{
				int whatNext = Random.Range(0, 100);
				if (whatNext > mchanceToTurnIfItIsMoving || (firstMove && whatNext > mchanceToMoveAfterCreation) || (currentState == StateEnum.idle && whatNext > mchanceToMoveItItIsIdle))
					ChangeDirection();
				else if (whatNext < mchanceToStopItItIsMoving)
					Stop(); 
			}
			else
			{
				timeSinceLastChange += Time.deltaTime;
			}

			if (nav.isActiveAndEnabled)
			{
				nav.SetDestination(movement);

				if(Vector3.Distance(movement, transform.position) < 1.0f)
					Stop ();
			}
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
				fireParticleSystem.Stop();
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
		firstMove = false;
		currentState = StateEnum.walk;
		fireParticleSystem.Stop();
		enemyRunningAnimation.startRunning(currentState == StateEnum.follow);

		if (!nav.isActiveAndEnabled)
			nav.enabled = true;
		movement = new Vector3(Random.Range(70, 400), transform.position.y, Random.Range (30, 330));

		timeSinceLastChange = 0.0f;
	}

	private void Stop()
	{
		currentState = StateEnum.idle;
		enemyRunningAnimation.stopRunning();
		movement = transform.position;
		
		if (nav.isActiveAndEnabled)
			nav.enabled = false;

		timeSinceLastChange = 0.0f;
	}
}
