using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	
	bool treeInRange;
	float timer;
	TreeHealth health;
	TreeState state;
	AudioSource chompAudio;

	void Awake ()
	{
		chompAudio = GetComponent <AudioSource> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "Tree")
		{
			health = other.gameObject.GetComponent<TreeHealth>();
			state = other.gameObject.GetComponent<TreeState>();
			treeInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject.tag == "Tree")
		{
			treeInRange = false;
		}
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if(timer >= timeBetweenAttacks && treeInRange)
		{
			Attack ();
		}
	}
	
	
	void Attack ()
	{
		timer = 0.0f;
		
		if(health.currentHealth > 0 && state.currentState == TreeStateEnum.Grown)
		{
			chompAudio.Play ();
			health.TakeDamage(attackDamage, transform.position);
		}
	}
}
