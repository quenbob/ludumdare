using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5.0f;
	public float minFov = 15f;
	public float maxFov = 90f;
	public float sensitivity = 10f;

	private Vector3 offset = new Vector3();
	
	void Start ()
	{
		if (target)
			offset = Camera.main.transform.position - target.position;
	}
	
	void FixedUpdate ()
	{
		if (target)
		{
			Vector3 targetCamPos = target.position + offset;
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
	}
	
	void Update () {
		float fov  = Camera.main.fieldOfView;
		fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;
	}
}
