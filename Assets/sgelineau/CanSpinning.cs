using UnityEngine;
using System.Collections;

public class CanSpinning : MonoBehaviour {

	public float degreesPerSecond = 180;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float angle = Time.deltaTime * degreesPerSecond;
		transform.Rotate(new Vector3(0, angle, 0));
	}
}
