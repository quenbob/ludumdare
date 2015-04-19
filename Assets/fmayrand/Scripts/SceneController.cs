using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour {

	[SerializeField] private GameObject menuOverlay;
	[SerializeField] private GameObject hudOverlay;
	[SerializeField] private GameObject loseOverlay;
	[SerializeField] private Text loseScore;

	private ScoreManager scoreManager;
	private TimerManager timerManager;

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
		timerManager.Pause();
	}

	public void ResumeGame() {
		Debug.Log("Resume game");
		menuOverlay.SetActive(false);
		hudOverlay.SetActive(true);
		loseOverlay.SetActive(false);
		timerManager.Resume();
	}

	public void EndGame() {
		Debug.Log("End game");
		menuOverlay.SetActive(false);
		hudOverlay.SetActive(false);
		loseScore.text = scoreManager.currentScore.ToString() + " trees cut";
		loseOverlay.SetActive(true);
	}
}
