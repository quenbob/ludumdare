using UnityEngine;
using System.Collections;

public class TreesManager : MonoBehaviour {

	public GameObject treeObject;
	public float timeBeforeFirstSpawn = 3.0f;
	public float timeBetweenTwoSpawn = 3.0f;
	public float xMin = -10.0f;
	public float xMax = 10.0f;
	public float zMin = -10.0f;
	public float zMax = 10.0f;
	public bool canSpawn = true ;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", timeBeforeFirstSpawn, timeBetweenTwoSpawn);
	}
	
	void Spawn () {
		if(!canSpawn)
		{
			return;
		}
		
		float x = Random.Range (xMin, xMax);
		float z = Random.Range (zMin, zMax);
		Quaternion rot = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up);

		Instantiate(treeObject, new Vector3(x, 0.0f, z), rot);
	}
}
