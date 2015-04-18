﻿using UnityEngine;
using System.Collections;

public class TreesManager : MonoBehaviour {

	public GameObject treeObject;
	public float timeBeforeFirstSpawn = 3.0f;
	public float timeBetweenTwoSpawn = 3.0f;
	public float xMin = -100.0f;
	public float xMax = 100.0f;
	public float zMin = -100.0f;
	public float zMax = 100.0f;
	public bool canSpawn = true ;
	public float clearRadiusAroundNewSpawn = 5.0f;
	public float radiusAroundPlayer = 20.0f;
	public float groundHeight = 10.0f;

	public GameObject player;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", timeBeforeFirstSpawn, timeBetweenTwoSpawn);
	}
	
	void Spawn () {
		if(!canSpawn)
		{
			return;
		}

		float xM = Mathf.Min (xMax, player.transform.position.x + radiusAroundPlayer);
		float zM = Mathf.Min (zMax, player.transform.position.z + radiusAroundPlayer);
		float xm = Mathf.Max (xMin, player.transform.position.x - radiusAroundPlayer);
		float zm = Mathf.Max (zMin, player.transform.position.z - radiusAroundPlayer);

		Vector3 pos = new Vector3();
		int nbTry = 0;
		bool ok = false;

		while(!ok && nbTry < 10) {
			nbTry++;
			pos = new Vector3(Random.Range (xm, xM), groundHeight, Random.Range (zm, zM));
			Collider[] cols = Physics.OverlapSphere(pos, clearRadiusAroundNewSpawn);

			ok = true;
			foreach (var col in cols)
			{
				if (col.attachedRigidbody && col.attachedRigidbody.gameObject.tag == "Tree")
				{
					ok = false;
					break;
				}
			}
		}

		if (ok)
		{
			Quaternion rot = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up);
			Instantiate(treeObject, pos, rot);
		}
	}
}
