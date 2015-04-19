using UnityEngine;
using System.Collections;

public class AnyKeyToToggleTransparency : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject tree = GameObject.Find ("Tree/Model");
		TreeState treeState = tree.GetComponent<TreeState> ();
		
		if (Input.anyKeyDown) {
		}	
	}
}
