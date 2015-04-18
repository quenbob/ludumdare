using UnityEngine;
using System.Collections;

public class TreeFalling : MonoBehaviour {

	public float fallingStrength = 200;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other) {
		/*if (other.collider.gameObject.tag == "Player") {
			Rigidbody rigidBody = GetComponent<Rigidbody> ();

			// enable physics while the tree is falling
			rigidBody.isKinematic = false;

			// fall opposite the player
			Vector3 playerToTree = (this.transform.position - other.transform.position).normalized;
			rigidBody.AddForce(fallingStrength * playerToTree);
		}*/
	}
}
