using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : MonoBehaviour {

	public float startTime;
	public Text timeLabel;
	public bool isGameOver = false;

	private CameraFollow cameraScript;
	private PlayerAttack playerAttack;
	private PlayerMovement playerMovement;

	// Use this for initialization
	void Start() 
	{
		cameraScript = GetComponent<CameraFollow>();

		GameObject player =  GameObject.Find("Player");

		if (player)
		{
			playerAttack = player.GetComponent<PlayerAttack>();
			playerMovement = player.GetComponent<PlayerMovement>();
		}
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
			isGameOver = true;

			//Application.LoadLevel("Lose Screen");
			if (cameraScript)
				cameraScript.ShowLogs();

			if (playerAttack)
				playerAttack.canAttack = false;

			if (playerMovement)
				playerMovement.canMove = false;
		}
	}

	public void AddTime(int amount) 
	{
		startTime += amount;
	}
}
