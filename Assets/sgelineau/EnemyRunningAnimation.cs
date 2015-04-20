using UnityEngine;
using System.Collections;

// should be named EnemyRunningAnimation
public class EnemyRunningAnimation : MonoBehaviour {

	private bool isRunning = false;
	private bool rightLegFirst = true;
	GameObject leftLeg;
	GameObject rightLeg;
	GameObject leftArm;
	GameObject rightArm;
	Quaternion originLeftArmRotation;
	Quaternion originRightArmRotation;
	Quaternion originLeftLegRotation;
	Quaternion originRigheLegRotation;
	private float totalRotation = 0.0f; 

	public void startRunning() {
		if (!isRunning) {
			totalRotation = 0.0f;
			isRunning = true;
			rightLegFirst = true;
		}
	}
	
	public void stopRunning() {
		if (isRunning) {
			isRunning = false;
			leftArm.transform.localRotation = originLeftArmRotation;
			rightArm.transform.localRotation = originRightArmRotation;
			leftLeg.transform.localRotation = originLeftLegRotation;
			rightLeg.transform.localRotation = originRigheLegRotation;
		}
	}
	
	// Use this for initialization
	void Start () {
		leftLeg = transform.Find("Model/BearMesh 1/BearLeftLeg").gameObject;
		rightLeg = transform.Find("Model/BearMesh 1/BearRightLegs").gameObject;
		leftArm = transform.Find("Model/BearMesh 1/BearLeftArm").gameObject;
		rightArm = transform.Find("Model/BearMesh 1/BearBody/BearRightArm").gameObject;
		originLeftArmRotation = leftArm.transform.localRotation;
		originRightArmRotation = rightArm.transform.localRotation;
		originLeftLegRotation = leftLeg.transform.localRotation;
		originRigheLegRotation = rightLeg.transform.localRotation;
	}

	// Update is called once per frame
	void Update () {
		float angle = ((rightLegFirst) ? 1 : -1) * 3.0f;
		totalRotation += angle * Time.deltaTime;
		if(totalRotation > 1.3f && rightLegFirst)
		{
			rightLegFirst = false;
		}
		else if (totalRotation < -0.5f && !rightLegFirst)
		{
			rightLegFirst = true;
		}

		Debug.Log(totalRotation);

		leftLeg.transform.Rotate(new Vector3 (angle, 0.0f, 0.0f));
		rightLeg.transform.Rotate(new Vector3 (-angle, 0.0f, 0.0f));
		leftArm.transform.Rotate(new Vector3 (-angle/1.5f, 0.0f, 0.0f));
		rightArm.transform.Rotate(new Vector3 (angle/1.5f, 0.0f, 0.0f));
	}
}
