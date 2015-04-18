using UnityEngine;
using System.Collections;

public class TreeFalling : MonoBehaviour {

	public float fallingStrength = 2000;
	public float secondsBeforeDisappearing = 0;
	public float secondsBlendingOut = 1;

	bool isDying = false;
	float t;
	int animationPhase;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isDying) {
			t += Time.deltaTime * 0.2f;

			switch (animationPhase) {
			case 0:
				if (t >= secondsBeforeDisappearing) {
					t = 0;
					++animationPhase;
				}
				break;
			case 1:
				float alpha = 1 - t / secondsBlendingOut;
				if (alpha < 0) {
					alpha = 0;
					Destroy (gameObject);
				} else {
					transform.Find ("Grown/Sphere").gameObject.GetComponent<Renderer> ().material.SetFloat ("_AlphaMultiplier", alpha);
					transform.Find ("Grown/Cylinder").gameObject.GetComponent<Renderer> ().material.SetFloat ("_AlphaMultiplier", alpha);
				}
				break;
			}
		}
	}

	public void cutDownFrom(Vector3 forceSource) {
		if (!isDying) {
			Rigidbody rigidBody = GetComponent<Rigidbody> ();

			// enable physics while the tree is falling
			rigidBody.isKinematic = false;

			// fall opposite the player
			Vector3 forceDirection = (this.transform.position - forceSource).normalized;
			rigidBody.AddForce(fallingStrength * forceDirection);

			// launch death animation
			isDying = true;
			t = 0;
			animationPhase = 0;

			// increment score
			GameObject scoreObject = GameObject.Find("Score");
			if (scoreObject) {
				scoreObject.GetComponent<ScoreKeeper>().Score(1);
			}
		}
	}
}
