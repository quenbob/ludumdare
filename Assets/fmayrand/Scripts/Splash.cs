using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Splash : MonoBehaviour {

	public float waitingTime = 2;

	IEnumerator Start () {
		yield return new WaitForSeconds(waitingTime);
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
