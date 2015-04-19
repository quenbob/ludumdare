using UnityEngine;
using System.Collections;

public class LogSpawning : MonoBehaviour {

	public GameObject objectToSpawn;

	public void spawnLog() {
		float angle = Random.Range (0, 360);
		Instantiate(objectToSpawn, transform.position, Quaternion.Euler(0,angle,0));
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
