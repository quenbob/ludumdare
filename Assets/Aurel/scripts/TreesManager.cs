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
	public float radiusAroundPlayer = 20.0f;
	public float groundHeight = 10.0f;

	public int initalTreeCount = 0;

	public GameObject player;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", timeBeforeFirstSpawn, timeBetweenTwoSpawn);

		for(int i = 0; i < initalTreeCount; i++)
		{
			AreaSpawn();
		}
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
	
		SpawnAt(xM, zM, xm, zm);
	}
	
	void AreaSpawn () {
		if(!canSpawn)
		{
			return;
		}
		
		SpawnAt (xMax, zMax, xMin, zMin, true);
	}

	void SpawnAt(float xM, float zM, float xm, float zm)
	{
		SpawnAt (xM, zM, xm, zm, false);
	}

	void SpawnAt(float xM, float zM, float xm, float zm, bool fullGrown)
	{
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
			GameObject tree = GameObject.Instantiate(treeObject, pos, rot) as GameObject;

			/*if(tree && fullGrown)
			{
				TreeState state = tree.GetComponentInChildren<TreeState>();
				state.currentState = TreeStateEnum.Grown;
			}*/
		}
	}
}
