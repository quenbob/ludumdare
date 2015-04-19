using UnityEngine;
using System.Collections;

public class PressAnyKeyToRun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find ("Player");
		PlayerRunningAnimation runningAnimation = player.GetComponent<PlayerRunningAnimation> ();

		if (Input.anyKey) {
			runningAnimation.startRunning();
		} else {
			runningAnimation.stopRunning();
		}
	}
}
