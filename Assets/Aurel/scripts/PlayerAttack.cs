using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	
	bool treeInRange;
	float timer;
	TreeHealth health;
	
	void OnTriggerEnter (Collider other)
	{
		Debug.Log("Enter !!!");
		if(other.gameObject.tag == "Tree")
		{
			health = other.gameObject.GetComponent<TreeHealth>();
			treeInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		Debug.Log("Exit !!!");
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
		Debug.Log("Attack !!!");
		
		if(health.currentHealth > 0)
		{
			health.TakeDamage(attackDamage);
		}
	}
}
