using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	[SerializeField] private GameObject menuOverlay;

	public void OnResumeGame() {
		Debug.Log("Resume game");
		menuOverlay.SetActive(false);

		GameObject managers = GameObject.Find("Managers");
		TimerManager timerManager = managers.GetComponent<TimerManager>();
		timerManager.Resume();
	}
}
