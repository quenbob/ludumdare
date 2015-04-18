using UnityEngine;
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

	private float x = -50.0f;
	private float y = 50.0f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", timeBeforeFirstSpawn, timeBetweenTwoSpawn);
	}
	
	void Spawn () {
		Instantiate(treeObject, new Vector3(x, 0.0f, y), Quaternion.identity);
		x += 6.0f ;

		if (x >= 50.0f)
		{
			x = -50.0f;
			y -= 6.0f;
		}
		/*if(!canSpawn)
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
		}*/
	}
}
