using UnityEngine;
using System.Collections;

public class TreeFalling : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other) {
		if (other.collider.gameObject.tag == "Player") {
			Rigidbody rigidBody = GetComponent<Rigidbody> ();
			rigidBody.isKinematic = false;
			rigidBody.AddForce(new Vector3(100,0,0));
		}
	}
}
