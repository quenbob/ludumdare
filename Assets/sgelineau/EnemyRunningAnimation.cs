using UnityEngine;
using System.Collections;

// should be named EnemyRunningAnimation
public class EnemyRunningAnimation : MonoBehaviour {

	public float legWigglePerSecond = 2.0f;
	public float legExtentInDegrees = 45.0f;
	public float bounceHeight = 0.1f;

	private bool isRunning = false;

	private bool rightLegFirst = true;
	private float t = 0.0f;

	private float lastWiggles = 0.0f;
	private float targetWiggles = 0.0f;

	GameObject enemyModel;
	GameObject leftLeg;
	GameObject rightLeg;
	GameObject leftArm;
	GameObject rightArm;
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
			targetWiggles = 0.0f; //Mathf.Ceil(lastWiggles*2)/2;
			isRunning = false;
		}
	}
	
	// Use this for initialization
	void Start () {
		enemyModel = transform.Find("Model").gameObject;
		leftLeg = transform.Find("Model/BearMesh 1/BearLeftLeg").gameObject;
		rightLeg = transform.Find("Model/BearMesh 1/BearRightLegs").gameObject;
		leftArm = transform.Find("Model/BearMesh 1/BearLeftArm").gameObject;
		rightArm = transform.Find("Model/BearMesh 1/BearBody/BearRightArm").gameObject;
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
		leftLeg.transform.rotation = Quaternion.Euler(new Vector3 (angle-30, 0, 0));
		rightLeg.transform.rotation = Quaternion.Euler(new Vector3 (-angle-30, 0, 0));
		leftArm.transform.rotation = Quaternion.Euler(new Vector3 (-angle-70, 0, 0));
		rightArm.transform.rotation = Quaternion.Euler(new Vector3 (angle-70, 0, 0));
	}
}
