using UnityEngine;
using System.Collections;

public class AnyKeyToToggleTreeState : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject tree = GameObject.Find ("Tree");
		TreeState treeState = tree.GetComponent<TreeState> ();

		if (Input.anyKeyDown) {
			switch (treeState.currentState) {
			case TreeStateEnum.Stump:
				treeState.currentState = TreeStateEnum.Sprout;
				break;
			case TreeStateEnum.Sprout:
				treeState.currentState = TreeStateEnum.Grown;
				break;
			case TreeStateEnum.Grown:
				treeState.currentState = TreeStateEnum.Stump;
				break;
			}
		}	
	}
}
