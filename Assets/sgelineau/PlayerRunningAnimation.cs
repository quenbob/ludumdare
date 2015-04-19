﻿using UnityEngine;
using System.Collections;

public class PlayerRunningAnimation : MonoBehaviour {

	public float legWigglePerSecond = 2;
	public float legExtentInDegrees = 45;
	public float bounceHeight = 0.1f;

	private bool isRunning = false;

	private bool rightLegFirst = true;
	private float t = 0;

	private float lastWiggles = 0;
	private float targetWiggles = 0;


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
			Debug.Log(lastWiggles + ", " + targetWiggles);
			isRunning = false;
		}
	}
	
	// Use this for initialization
	void Start () {
		
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
		float height = Mathf.Abs (Mathf.Sin (wiggles * 2*Mathf.PI) * bounceHeight);

		GameObject playerModel = GameObject.Find("Model");
		GameObject leftLeg = GameObject.Find("Left leg");
		GameObject rightLeg = GameObject.Find("Right leg");

		playerModel.transform.position = new Vector3(playerModel.transform.position.x, height, playerModel.transform.position.z);
		leftLeg.transform.rotation = Quaternion.Euler(new Vector3 (angle, 0, 0));
		rightLeg.transform.rotation = Quaternion.Euler(new Vector3 (-angle, 0, 0));
	}
}
