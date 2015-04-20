using UnityEngine;
using System.Collections;

public class GrabPowerUp : MonoBehaviour {

	public ParticleSystem pickUpEffect;

	bool hasBeenPickedUp = false;

	// Use this for initialization
	void Start () {
		pickUpEffect.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (!hasBeenPickedUp) {
				hasBeenPickedUp = true;

				GameObject.Destroy (transform.Find ("Model").gameObject);
				transform.Find ("PowerUpEffect 1").GetComponent<ParticleSystem> ().Stop ();
				pickUpEffect.Play ();
				GameObject.Destroy (gameObject, 3);

				GetComponent<AudioSource>().Play();
				GameObject.Find ("Managers").GetComponent<TimerManager> ().AddTime (8);
			}
		}
	}
}
