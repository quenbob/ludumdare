﻿using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	[SerializeField] private GameObject menuOverlay;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Debug.Log("Show menu");
			menuOverlay.SetActive(true);

			GameObject managers = GameObject.Find("Managers");
			TimerManager timerManager = managers.GetComponent<TimerManager>();
			timerManager.Pause();
		}
	}
}
