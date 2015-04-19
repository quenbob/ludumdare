using UnityEngine;
using System.Collections;

public class PressAnyKeyToRun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject enemy = GameObject.Find ("Enemy");
		EnemyRunningAnimation runningAnimation = enemy.GetComponent<EnemyRunningAnimation> ();

		if (Input.anyKey) {
			runningAnimation.startRunning();
		} else {
			runningAnimation.stopRunning();
		}
	}
}
