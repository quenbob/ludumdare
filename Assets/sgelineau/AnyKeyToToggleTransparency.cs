using UnityEngine;
using System.Collections;

public class AnyKeyToToggleTransparency : MonoBehaviour {

	bool isOpaque = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject tree = GameObject.Find ("Tree/Model");
		TreeTransparency treeTransparency = tree.GetComponent<TreeTransparency> ();
		
		if (Input.anyKeyDown) {
			if (isOpaque) {
				treeTransparency.makeTransparent();
				isOpaque = false;
			} else {
				treeTransparency.makeOpaque();
				isOpaque = true;
			}
		}	
	}
}
