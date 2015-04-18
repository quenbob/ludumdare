using UnityEngine;
using System.Collections;

public enum TreeStateEnum {
	Sprout, Grown, Stump
}

public class TreeState : MonoBehaviour {

	public TreeStateEnum currentState = TreeStateEnum.Grown;

	// Use this for initialization
	void Start () {
		GameObject sprout = GameObject.Find("Sprout");
		GameObject grown = GameObject.Find("Grown");
		GameObject stump = GameObject.Find("Stump");

		sprout.SetActive(currentState == TreeStateEnum.Sprout);
		grown.SetActive(currentState == TreeStateEnum.Grown);
		stump.SetActive(currentState == TreeStateEnum.Stump);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
