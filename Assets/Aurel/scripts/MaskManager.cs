﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaskManager : MonoBehaviour 
{
	private List<GameObject> listeMasked = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit[] hits;
		List<GameObject> listCopy = new List<GameObject>(listeMasked.ToArray());

		hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 100.0f);
		int i = 0;

		while (i < hits.Length) 
		{
			RaycastHit hit = hits[i];
			GameObject tree = hit.transform.gameObject;
			if (tree)
			{
				if (listeMasked.Contains(tree) == false)
				{
					//tree.transform.position.Set();
					listeMasked.Add(tree);
				}
				else
				{
					listCopy.Remove(tree);
				}
			}
			i++;
		}

		foreach (GameObject t in listCopy)
		{
			/*if (t)
				t.SetActive(true);*/
		}
		listCopy.Clear();
	}
}
