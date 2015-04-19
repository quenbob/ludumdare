using UnityEngine;
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
			if (tree && tree.tag == "Tree")
			{
				if (listeMasked.Contains(tree) == false)
				{
					TreeTransparency script = tree.GetComponentInChildren<TreeTransparency>();
					if (script)
					{
						script.makeTransparent();
					}

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
			if (t)
			{
				TreeTransparency script = t.GetComponentInChildren<TreeTransparency>();
				if (script)
				{
					script.makeOpaque();
				}
				listeMasked.Remove(t);
			}
		}
		listCopy.Clear();
	}
}
