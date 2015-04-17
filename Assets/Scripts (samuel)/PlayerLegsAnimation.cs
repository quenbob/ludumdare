using UnityEngine;
using System.Collections;

public class PlayerLegsAnimation : MonoBehaviour {

	public float legWigglePerSecond = 2;
	public float legExtentInDegrees = 45;

	private float t = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		float wiggles = legWigglePerSecond * t;
		float angle = Mathf.Sin (wiggles * 2*Mathf.PI) * legExtentInDegrees;

		GameObject leftLeg = GameObject.Find("Left leg");
		GameObject rightLeg = GameObject.Find("Right leg");

		leftLeg.transform.rotation = Quaternion.Euler(new Vector3 (angle, 0, 0));
		rightLeg.transform.rotation = Quaternion.Euler(new Vector3 (-angle, 0, 0));
	}
}
