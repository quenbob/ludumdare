using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour 
{
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

	private float timer;
	private TreeHealth health;
	private TreeState state;
	private AudioSource chompAudio;
	private PlayerMovement playerMovement;

	void Awake()
	{
		chompAudio = GetComponent<AudioSource>();
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

		timer += Time.deltaTime;
		
		if(timer >= timeBetweenAttacks && treeInRange)
		{
			Attack();
		}
	}
	
	
	void Attack()
	{
		timer = 0.0f;
		
		if(health && state && health.currentHealth > 0 && state.currentState == TreeStateEnum.Grown)
		{
			chompAudio.Play ();
			health.TakeDamage(attackDamage, transform.position);
		}
		else
		{
			treeInRange = false;
		}
	}
}
