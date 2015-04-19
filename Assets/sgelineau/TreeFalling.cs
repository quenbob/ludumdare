using UnityEngine;
using System.Collections;

public class TreeFalling : MonoBehaviour {

	public float fallingStrength = 200;
	public float secondsBeforeDisappearing = 0;
	public float secondsBlendingOut = 1;
	public float timeBeforeSound = 1.1f;
	public int timeAdded = 5;

	private AudioSource fallAudio;
	bool misDying = false;
	bool isDying
	{
		get { return misDying; }
		set
		{
			if (misDying != value)
			{
				misDying = value;

				if(value == true)
					Invoke("StartSound", timeBeforeSound);
			}
		}
	}

	float t;
	int animationPhase;

	// Use this for initialization
	void Start () {
		fallAudio = GetComponent<AudioSource>();
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
					Destroy (transform.Find("..").gameObject);
				} else {
					GetComponent<TreeTransparency>().setAlpha(alpha);
				}
				break;
			}
		}
	}

	public void cutDownFrom(Vector3 forceSource, bool score = true) {
		if (!isDying) {
			Rigidbody rigidBody = GetComponent<Rigidbody> ();

			// enable physics while the tree is falling
			rigidBody.isKinematic = false;

			// fall opposite the player
			Vector3 forceDirection = (this.transform.position - forceSource).normalized;
			rigidBody.AddForce(fallingStrength * forceDirection);

			// launch fade out animation
			isDying = true;
			t = 0;
			animationPhase = 0;

			if(score)
			{
				// increment score
				GameObject scoreLabel = GameObject.Find("Managers");
				if (scoreLabel) {
					scoreLabel.GetComponent<ScoreManager>().AddScore(1);
				}

				// give the player more time
				GameObject timerLabel = GameObject.Find("Managers");
				if (timerLabel) {
					timerLabel.GetComponent<TimerManager>().AddTime(timeAdded);
				}
			}
		}
	}

	void StartSound()
	{
		if(fallAudio)
			fallAudio.Play();
	}
}
