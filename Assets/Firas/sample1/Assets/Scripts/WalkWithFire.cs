using UnityEngine;
using System.Collections;

public class WalkWithFire : MonoBehaviour {

	public ParticleSystem OnFireParticleSystem;
	// Use this for initialization
	void Start () {
		//ParticleSystem currentParticleSystem = GetComponent<ParticleSystem> ();
		OnFireParticleSystem.Pause ();

		//currentParticleSystem.Emit (5);
		//OnFireParticleSystem.enableEmission (false);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find ("Player");
		PlayerRunningAnimation runningAnimation = player.GetComponent<PlayerRunningAnimation> ();

		if (Input.GetKeyDown ("space")) {
			//OnFireParticleSystem.Play ();

			OnFireParticleSystem.Play ();
		}
		else if(Input.GetKeyDown ("s"))
		{
			//OnFireParticleSystem.Pause ();
			//OnFireParticleSystem.Clear();

			OnFireParticleSystem.Stop ();
		}

		if (Input.anyKey) {
			runningAnimation.startRunning();

		} else {
			runningAnimation.stopRunning();

		}


	}
}
