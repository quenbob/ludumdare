using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : MonoBehaviour {

	public float startTime;
	public Text timeLabel;

	private CameraFollow cameraScript;

	// Use this for initialization
	void Start() 
	{
		cameraScript = GetComponent<CameraFollow>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		float timeRemaining = startTime - Time.realtimeSinceStartup;
		int min = (int)(timeRemaining / 60);
		int sec = (int)(timeRemaining - min*60);
		string dixs = (sec < 10) ? "0" : "";
		string dixm = (min < 10) ? "0" : "";

		if (timeLabel)
		{
			timeLabel.text = "Time: " + dixm + min + ":" + dixs + ((sec > 0) ? sec : 0).ToString();
		}

		if(Time.realtimeSinceStartup > startTime)
		{
			//Application.LoadLevel("Lose Screen");
			if (cameraScript)
				cameraScript.ShowLogs();
		}
	}

	public void AddTime(int amount) 
	{
		startTime += amount;
	}
}
