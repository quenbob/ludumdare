using UnityEngine;
using System.Collections;

// should be named EnemyRunningAnimation
public class EnemyRunningAnimation : MonoBehaviour {

	public float legWigglePerSecond = 2;
	public float legExtentInDegrees = 45;
	public float bounceHeight = 0.1f;

	private bool isRunning = false;

	private bool rightLegFirst = true;
	private float t = 0;

	private float lastWiggles = 0;
	private float targetWiggles = 0;

	GameObject enemyModel;
	GameObject leftLeg;
	GameObject rightLeg;
	private float origHeight;

	public void startRunning() {
		if (!isRunning) {
			t = 0;
			isRunning = true;
			rightLegFirst = !rightLegFirst;
		}
	}
	
	public void stopRunning() {
		if (isRunning) {
			targetWiggles = Mathf.Ceil(lastWiggles*2)/2;
			isRunning = false;
		}
	}
	
	// Use this for initialization
	void Start () {
		enemyModel = transform.Find("Model").gameObject;
		leftLeg = transform.Find("Model/Body/Left leg").gameObject;
		rightLeg = transform.Find("Model/Body/Right leg").gameObject;
		origHeight = enemyModel.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		float wiggles;
		if (isRunning) {
			t += Time.deltaTime;
			wiggles = legWigglePerSecond * t;
		} else {
			wiggles = lastWiggles + (targetWiggles - lastWiggles) * 0.5f;
		}

		lastWiggles = wiggles;
		float angle = (rightLegFirst ? 1 : -1) * Mathf.Sin (wiggles * 2*Mathf.PI) * legExtentInDegrees;
		float height = origHeight + Mathf.Abs (Mathf.Sin (wiggles * 2*Mathf.PI) * bounceHeight);

		enemyModel.transform.position = new Vector3(enemyModel.transform.position.x, height, enemyModel.transform.position.z);
		leftLeg.transform.rotation = Quaternion.Euler(new Vector3 (angle, 0, 0));
		rightLeg.transform.rotation = Quaternion.Euler(new Vector3 (-angle, 0, 0));
	}
}
