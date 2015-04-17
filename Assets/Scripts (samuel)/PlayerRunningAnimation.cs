﻿using UnityEngine;
using System.Collections;

public class PlayerRunningAnimation : MonoBehaviour {

	public float legWigglePerSecond = 2;
	public float legExtentInDegrees = 45;
	public float bounceHeight = 0.1f;

	private float t = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		float wiggles = legWigglePerSecond * t;
		float angle = Mathf.Sin (wiggles * 2*Mathf.PI) * legExtentInDegrees;
		float height = Mathf.Abs (Mathf.Sin (wiggles * 2*Mathf.PI) * bounceHeight);

		GameObject playerModel = GameObject.Find("Model");
		GameObject leftLeg = GameObject.Find("Left leg");
		GameObject rightLeg = GameObject.Find("Right leg");

		playerModel.transform.position = new Vector3(playerModel.transform.position.x, height, playerModel.transform.position.z);
		leftLeg.transform.rotation = Quaternion.Euler(new Vector3 (angle, 0, 0));
		rightLeg.transform.rotation = Quaternion.Euler(new Vector3 (-angle, 0, 0));
	}
}
