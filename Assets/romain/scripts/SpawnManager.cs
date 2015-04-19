using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
	public GameObject spawnling;
	public float timeBeforeFirstSpawn = 3.0f;
	public float timeBetweenTwoSpawn = 3.0f;
	public float xMin = -100.0f;
	public float xMax = 100.0f;
	public float zMin = -100.0f;
	public float zMax = 100.0f;
	public float yPos = 0.0f;
	public bool canSpawn = true ;
	public float clearRadiusAroundNewSpawn = 15.0f;
	public string objectNameToAvoid = "Tree";

	void Start () {
		InvokeRepeating ("Spawn", timeBeforeFirstSpawn, timeBetweenTwoSpawn);
	}
	
	void Spawn () {
		if(!canSpawn) {
			return;
		}
		
		Vector3 pos = new Vector3();
		int nbTry = 0;
		bool ok = false;
		
		while(!ok && nbTry < 10) {
			nbTry++;
			pos = new Vector3(Random.Range (xMin, xMax), yPos, Random.Range (zMin, zMax));
			Collider[] cols = Physics.OverlapSphere(pos, clearRadiusAroundNewSpawn);
			ok = true;

			foreach (var col in cols) {
				if (col.attachedRigidbody && col.attachedRigidbody.gameObject.tag == objectNameToAvoid) {
					ok = false;

					break;
				}
			}
		}
		
		if (ok) {
			Quaternion rot = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up);
			Instantiate(spawnling, pos, rot);
		}
	}
}