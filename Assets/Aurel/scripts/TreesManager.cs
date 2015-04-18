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

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", timeBeforeFirstSpawn, timeBetweenTwoSpawn);
	}
	
	void Spawn () {
		if(!canSpawn)
		{
			return;
		}

		Vector3 pos = new Vector3();
		int nbTry = 0;
		bool ok = false;

		while(!ok && nbTry < 10) {
			nbTry++;
			pos = new Vector3(Random.Range (xMin, xMax), 0.0f, Random.Range (zMin, zMax));
			Collider[] cols = Physics.OverlapSphere(pos, clearRadiusAroundNewSpawn);

			if (cols.Length == 0)
				ok = true;
		}

		if (ok)
		{
			Quaternion rot = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up);
			Instantiate(treeObject, pos, rot);
		}
	}
}
