using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaskManager : MonoBehaviour {

	List<GameObject> listeMasked = new List<GameObject>();
	Ray maskRay;
	RaycastHit maskHit;
	int maskableMask;

	void Awake ()
	{
		maskableMask = LayerMask.GetMask ("Maskable");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit[] hits;
		List<GameObject> listCopy = new List<GameObject>(listeMasked.ToArray());

		hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 100.0F);
		int i = 0;
		while (i < hits.Length) {
			RaycastHit hit = hits[i];
			GameObject tree = hit.transform.gameObject;
			if (listeMasked.Contains(tree) == false)
			{
				tree.SetActive(false);
				listeMasked.Add(tree);
			}
			else
			{
				listCopy.Remove(tree);
			}
			i++;
		}

		foreach (GameObject t in listCopy)
		{
			t.SetActive(true);
		}
		listCopy.Clear();
	}
}
