using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	
	public Text scoreLabel;
	public int currentScore = 0;

	// Use this for initialization
	void Start () {
		scoreLabel = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		scoreLabel.text = "Score: " + currentScore;
	}

	void AddScore(int amount)
	{
		currentScore += amount;
	}
}
