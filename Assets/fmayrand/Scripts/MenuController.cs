using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	[SerializeField] private SceneController sceneController;

	public void OnResumeGame() {
		sceneController.ResumeGame();
	}
}
