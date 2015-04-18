using UnityEngine;
using System.Collections;

public class LogOptimizing : MonoBehaviour {

	public float secondsOfPhysicsSimulation = 5;

	bool isSimulating = true;
	float t = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isSimulating) {
			t += Time.deltaTime;

			if (t >= secondsOfPhysicsSimulation) {
				isSimulating = false;

				GetComponent<Rigidbody> ().isKinematic = true;
			}
		}
	}
}
