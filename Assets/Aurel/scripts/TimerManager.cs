using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : MonoBehaviour {

	public float startTime;
	public Text timeLabel;
	public SceneController sceneController;

	public int secondsToBeepFor = 5;
	private int lastSecondBeepedFor = -1;

	private bool mIsPaused = false;
	public bool isPaused {
		get { return mIsPaused; }
		set { mIsPaused = value; }
	}

	private bool mIsGameOver = false;
	public bool isGameOver
	{
		get { return mIsGameOver; }
		set
		{
			if (mIsGameOver != value)
			{
				mIsGameOver = value;

				if (value)
				{
					if (sceneController)
						sceneController.EndGame();
					
					if (cameraScript)
						cameraScript.ShowLogs();
					
					if (playerAttack)
						playerAttack.canAttack = false;
					
					if (playerMovement)
						playerMovement.canMove = false;
				}
			}
		}
	}


	private CameraFollow cameraScript;
	private PlayerAttack playerAttack;
	private PlayerMovement playerMovement;
	private float offsetSinceStartup = 0.0f;
	private float timeRemaining = 0.0f;

	// Use this for initialization
	void Start() 
	{
		cameraScript = GetComponent<CameraFollow>();
		startTime += 1.0f;

		GameObject player =  GameObject.Find("Player");

		if (player)
		{
			playerAttack = player.GetComponent<PlayerAttack>();
			playerMovement = player.GetComponent<PlayerMovement>();
		}

		offsetSinceStartup = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update() 
	{
		if(isPaused || isGameOver)
			return;

		timeRemaining = startTime - Time.realtimeSinceStartup + offsetSinceStartup;

		int intTimeRemaining = (int)timeRemaining;
		if (intTimeRemaining <= secondsToBeepFor && intTimeRemaining != lastSecondBeepedFor) {
			lastSecondBeepedFor = intTimeRemaining;
			Beep ();
		}

		int min = (int)(timeRemaining / 60);
		int sec = (int)(timeRemaining - min*60);
		string dixs = (sec < 10) ? "0" : "";
		string dixm = (min < 10) ? "0" : "";

		if (timeLabel)
		{
			timeLabel.text = "Time: " + dixm + min + ":" + dixs + ((sec > 0) ? sec : 0).ToString();
		}

		if(timeRemaining <= 0)
		{
			isGameOver = true;
		}
	}

	public void AddTime(int amount) 
	{
		startTime += amount;
	}

	public void Beep() {
		GameObject secondsBeeper = GameObject.Find ("SecondsBeeper");
		if (secondsBeeper) {
			secondsBeeper.GetComponent<AudioSource>().Play();
		}
	}

	public void Pause()
	{
		isPaused = true;
	}

	public void Resume()
	{
		isPaused = false;
		offsetSinceStartup = timeRemaining + Time.realtimeSinceStartup - startTime;
	}
}
