using UnityEngine;
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
		ParticleSystem ps = transform.Find("../WoodChips").gameObject.GetComponent<ParticleSystem>();
		{
			Vector3 towardsPlayer = playerPosition - transform.position;
			float angle = Mathf.Atan2(towardsPlayer.z, towardsPlayer.x);

			Quaternion upwards = Quaternion.Euler (315, 0, 0);
			Quaternion resetToPositiveX = Quaternion.Euler (0, 90, 0);
			Quaternion rotateTowardsPlayer = Quaternion.Euler(0, -angle*180/Mathf.PI, 0);

			ps.transform.rotation = rotateTowardsPlayer * resetToPositiveX * upwards;
		}
		ps.Play(true);

		currentHealth -= amount;
		if (currentHealth <= 0) {
			currentHealth = 0;

			GetComponent<TreeFalling>().cutDownFrom(playerPosition);
		}
	}
}
