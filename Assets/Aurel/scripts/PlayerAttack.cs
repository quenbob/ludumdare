using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour 
{
	public float timeToRevUp = 1.8f;
	public float timeBetweenLoops = 0.5f;
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	public bool canAttack = true;

	private bool m_treeInRange = false;
	public bool treeInRange
	{
		get { return m_treeInRange; }
		set 
		{ 
			if (value != m_treeInRange)
			{
				m_treeInRange = value;
				if(playerMovement)
					playerMovement.TreeInRange(treeInRange);
			}
		}
	}

	private float attackTimer;
	private float soundTimer;
	private bool isPlayingSound = false;
	private bool hasRevvedUp = false;
	private TreeHealth health;
	private TreeState state;
	private AudioSource[] beaverAudio;
	private PlayerMovement playerMovement;

	void Awake()
	{
		beaverAudio = GetComponents<AudioSource>();
		playerMovement = GetComponent<PlayerMovement>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Tree")
		{
			health = other.gameObject.GetComponent<TreeHealth>();
			state = other.gameObject.GetComponent<TreeState>();
			treeInRange = true;
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Tree")
		{
			treeInRange = false;
			return;
		}
	}
	
	
	void Update()
	{
		if (!canAttack)
		{
			treeInRange = false;
		}

		attackTimer += Time.deltaTime;
		if (attackTimer >= timeBetweenAttacks && treeInRange) {
			Attack ();
		}

		soundTimer += Time.deltaTime;
		if (treeInRange && isPlayingSound) {
			if (!hasRevvedUp) {
				if (soundTimer >= timeToRevUp) {
					hasRevvedUp = true;
					soundTimer = 0;
					beaverAudio[1].Play ();
				}
			} else {
				if (soundTimer >= timeBetweenLoops) {
					soundTimer = 0;
					beaverAudio[1].Play ();
				}
			}
		} else {
			isPlayingSound = false;
			hasRevvedUp = false;
		}
	}
	
	
	void Attack()
	{
		attackTimer = 0.0f;
		
		if(health && state && health.currentHealth > 0 && state.currentState == TreeStateEnum.Grown)
		{
			health.TakeDamage(attackDamage, transform.position);

			if (!isPlayingSound) {
				isPlayingSound = true;
				soundTimer = 0;
				beaverAudio[0].Play ();
			}
		}
		else
		{
			treeInRange = false;
		}
	}
}
