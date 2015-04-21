using UnityEngine;
using System.Collections;

// should be named EnemyRunningAnimation
public class EnemyRunningAnimation : MonoBehaviour {
	public float legWigglePerSecond = 2.0f;
	public float legExtentInDegrees = 45.0f;
	public float bounceHeight = 0.1f;
	private float offset = -90.0f;

	public bool isRunning = false;
	private bool rightLegFirst = true;
	private float t = 0.0f;
	private float lastWiggles = 0.0f;
	private float targetWiggles = 0.0f;

	GameObject enemyModel;
	GameObject leftLeg;
	GameObject rightLeg;
	GameObject leftArm;
	GameObject rightArm;
	Quaternion originLeftArmRotation;
	Quaternion originRightArmRotation;
	Quaternion originLeftLegRotation;
	Quaternion originRigheLegRotation;

	public void startRunning() {
		if (!isRunning) {
			t = 0.0f;
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
		leftLeg = transform.Find("Model/BearMesh 1/BearLeftLeg").gameObject;
		rightLeg = transform.Find("Model/BearMesh 1/BearRightLegs").gameObject;
		leftArm = transform.Find("Model/BearMesh 1/BearLeftArm").gameObject;
		rightArm = transform.Find("Model/BearMesh 1/BearBody/BearRightArm").gameObject;
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
		float angle = (rightLegFirst ? 1 : -1) * Mathf.Sin (wiggles * 2*Mathf.PI) * legExtentInDegrees + offset;
		float height = Mathf.Abs (Mathf.Sin (wiggles * 2*Mathf.PI) * bounceHeight);

		enemyModel.transform.position = new Vector3(enemyModel.transform.position.x, height + 10.0f, enemyModel.transform.position.z);
		leftLeg.transform.localRotation = Quaternion.Euler(new Vector3 (angle, 0.0f, 0.0f));
		rightLeg.transform.localRotation = Quaternion.Euler(new Vector3 (180.0f-angle, 0.0f, 0.0f));
		leftArm.transform.localRotation = Quaternion.Euler(new Vector3 (angle, 0.0f, 0.0f));
		rightArm.transform.localRotation = Quaternion.Euler(new Vector3 (270.0f-angle, 0.0f, 0.0f));
	}
}
