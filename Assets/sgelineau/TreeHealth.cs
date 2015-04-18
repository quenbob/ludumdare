﻿using UnityEngine;
using System.Collections;

public class TreeHealth : MonoBehaviour {

	public float currentHealth = 30.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(float amount, Vector3 playerPosition)
	{
		ParticleSystem ps = transform.Find("WoodChips").gameObject.GetComponent<ParticleSystem>();
		ps.Play(true);

		currentHealth -= amount;
		if (currentHealth <= 0) {
			currentHealth = 0;

			GetComponent<TreeFalling>().cutDownFrom(playerPosition);
		}
	}
}
