using UnityEngine;
using System.Collections;

public enum TreeStateEnum {
	Sprout, Grown, Stump
}

public class TreeState : MonoBehaviour {

	public TreeStateEnum initialState = TreeStateEnum.Grown;

	private TreeStateEnum privateState;
	public TreeStateEnum currentState {
		get { return privateState; }
		set {
			privateState = value;
			
			sprout.SetActive(privateState == TreeStateEnum.Sprout);
			grown.SetActive(privateState == TreeStateEnum.Grown);
			stump.SetActive(privateState == TreeStateEnum.Stump);
		}
	}

	GameObject sprout;
	GameObject grown;
	GameObject stump;
	
	// Use this for initialization
	void Start () {
		sprout = GameObject.Find("Sprout");
		grown = GameObject.Find("Grown");
		stump = GameObject.Find("Stump");

		currentState = initialState;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
