using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Transform logPile;
	public float smoothing = 5.0f;
	public float minFov = 15f;
	public float maxFov = 60f;
	public float sensitivity = 10f;

	private Vector3 offset = new Vector3();
	private Vector3 LogPosition = new Vector3();
	private bool followPlayer = true;
	private LogSpawning script;
	private ScoreManager scoreManager;

	private bool mCameraArrive = false;
	private bool cameraArrive
	{
		set
		{
			if (mCameraArrive != value)
			{
				Debug.Log ("coucou");
				mCameraArrive = value;
				if (value)
					DropLogs();
			}
		}
	}

	void Start ()
	{
		scoreManager = GetComponent<ScoreManager>();

		if (target)
			offset = Camera.main.transform.position - target.position;

		if (logPile)
			LogPosition = new Vector3(logPile.position.x, Camera.main.transform.position.y, logPile.position.z - 10.0f);
	}
	
	void FixedUpdate ()
	{
		if (followPlayer && target)
		{
			Vector3 targetCamPos = target.position + offset;
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
		else if (logPile)
		{
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, LogPosition, smoothing * Time.deltaTime);
			Camera.main.fieldOfView = maxFov;

			float dist = Vector3.Distance(Camera.main.transform.position, LogPosition);

			if (dist < 2.0f)
			{
				cameraArrive = true;
			}
		}
	}
	
	void Update () {
		float fov  = Camera.main.fieldOfView;
		fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;
	}

	public void ShowLogs()
	{
		followPlayer = false;
	}

	public void DropLogs()
	{
		GameObject logSpawner = GameObject.Find ("LogSpawner");

		if (logSpawner && scoreManager)
		{
			script = logSpawner.GetComponent<LogSpawning>();
			if (script)
			{
				InvokeRepeating ("SpawnLog", 0.5f, 0.3f);
			}
		}
	}

	private void SpawnLog()
	{
		if (script && scoreManager)
		{
			if (scoreManager.currentScore > 0)
			{
				script.spawnLog();
				scoreManager.currentScore--;
			}
			else
			{
				CancelInvoke("SpawnLog");
			}
		}
	}
}
