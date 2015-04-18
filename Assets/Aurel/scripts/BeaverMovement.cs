using UnityEngine;
using System.Collections;

public class BeaverMovement : MonoBehaviour {
	public PlayerAttack playerAttack;

	// Use this for initialization
	void Start () {
		playerAttack = transform.Find("../Player").gameObject.GetComponent<PlayerAttack>()
	}
	
	// Update is called once per frame
	void Update () {
		if(playerAttack.treeInRange)
		{

		}
	}
}
