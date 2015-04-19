using UnityEngine;
using System.Collections;

public class woodSound : MonoBehaviour 
{
	private bool alreadyDone = false;
	private AudioSource fallAudio;

	// Use this for initialization
	void Start () {
		fallAudio = GetComponent<AudioSource>();
	}

	void OnCollisionEnter(Collision other)
	{
		if(alreadyDone)
			return;

		if(fallAudio)
			fallAudio.Play();

		alreadyDone = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if(alreadyDone)
			return;
		
		if(fallAudio)
			fallAudio.Play();
		
		alreadyDone = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
