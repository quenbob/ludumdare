﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour {

	[SerializeField] private GameObject menuOverlay;
	[SerializeField] private GameObject hudOverlay;
	[SerializeField] private GameObject loseOverlay;
	[SerializeField] private GameObject creditsOverlay;
	[SerializeField] private Text endMessage;
	[SerializeField] private Text loseScore;
	[SerializeField] private LevelManager levelManager;

	private ScoreManager scoreManager;
	private TimerManager timerManager;
	private bool isGameFinished = false;

	void Start() {
		GameObject managers = GameObject.Find("Managers");
		scoreManager = managers.GetComponent<ScoreManager>();
		timerManager = managers.GetComponent<TimerManager>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			PauseGame();
		}
	}

	public void PauseGame() {
		Debug.Log("Pause game");
		menuOverlay.SetActive(true);
		hudOverlay.SetActive(false);
		loseOverlay.SetActive(false);
		creditsOverlay.SetActive(false);
		timerManager.Pause();
	}

	public void ResumeGame() {
		Debug.Log("Resume game");
		if (isGameFinished) {
			menuOverlay.SetActive(false);
			loseOverlay.SetActive(true);
			return;
		}

		menuOverlay.SetActive(false);
		hudOverlay.SetActive(true);
		loseOverlay.SetActive(false);
		creditsOverlay.SetActive(false);
		timerManager.Resume();
	}

	public void EndGame(string message) {
		Debug.Log("End game");
		if (isGameFinished) {
			return;
		}

		isGameFinished = true;
		menuOverlay.SetActive(false);
		hudOverlay.SetActive(false);
		creditsOverlay.SetActive(false);

		if (scoreManager.currentScore == 1) {
			loseScore.text = "1 tree cut";
		}
		else {
			loseScore.text = scoreManager.currentScore.ToString() + " trees cut";
		}

		endMessage.text = message;
		loseOverlay.SetActive(true);
	}

	public void RestartGame() {
		scoreManager.currentScore = 0;
		levelManager.LoadLevel("Level1");
	}

	public void ShowCredits() {
		menuOverlay.SetActive(false);
		hudOverlay.SetActive(false);
		loseOverlay.SetActive(false);
		creditsOverlay.SetActive(true);
	}
}
