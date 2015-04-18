using UnityEngine;
using System.Collections;

public class LogSpawning : MonoBehaviour {

	public GameObject objectToSpawn;

	public void spawnLog() {
		Instantiate(objectToSpawn, transform.position, transform.rotation);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
