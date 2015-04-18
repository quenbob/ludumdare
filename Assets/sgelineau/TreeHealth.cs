using UnityEngine;
using System.Collections;

public class TreeHealth : MonoBehaviour {

	public float currentHealth = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(float amount)
	{
		ParticleSystem ps = transform.Find("WoodChips").gameObject.GetComponent<ParticleSystem>();
		ps.Play(true);
	}
}
