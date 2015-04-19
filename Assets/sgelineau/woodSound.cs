using UnityEngine;
using System.Collections;

public class woodSound : MonoBehaviour 
{
	private bool alreadyDone = false;
	private AudioSource fallAudio;
	public bool trigger = false;

	// Use this for initialization
	void Start () {
		fallAudio = GetComponent<AudioSource>();
	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("prout");

		if(alreadyDone || trigger)
			return;

		if(fallAudio)
			fallAudio.Play();

		alreadyDone = true;
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("hello");

		if(alreadyDone || !trigger)
			return;
		
		if(fallAudio)
			fallAudio.Play();
		
		alreadyDone = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
