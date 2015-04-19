using UnityEngine;
using System.Collections;

public class LogSpawning : MonoBehaviour {

	public GameObject objectToSpawn;

	private AudioSource fallAudio;

	public void spawnLog() {
		float angle = Random.Range (0, 360);
		Instantiate(objectToSpawn, transform.position, Quaternion.Euler(0,angle,0));

		if(fallAudio)
			fallAudio.Play();
	}

	// Use this for initialization
	void Start () {
		fallAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
