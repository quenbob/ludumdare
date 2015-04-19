using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	[SerializeField] private LevelManager levelManager;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			levelManager.LoadLevel("Start Menu");
		}
	}
}
