using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeScript : MonoBehaviour {

	public bool TreeOnFire = false;
	public ParticleSystem OnFireParticleSystem;
	public float TimeToBurnOthers = 7.0f;
	public int MaxTreesToBurn = 2;
	public float FireRadius = 15.0f;

	private float FireStartedTime = -1.0f;
	private int numberOfFiresStarted = 0;
	// Use this for initialization
	void Start () {
		OnFireParticleSystem.Stop ();
	}

	void SetThisOnFire()
	{
		TreeOnFire = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(TreeOnFire)
		{
			if(!OnFireParticleSystem.IsAlive())
			{
				OnFireParticleSystem.Play ();
			}

			if(FireStartedTime<0)
			{
				FireStartedTime = Time.time;

			}

			{

				if(numberOfFiresStarted < MaxTreesToBurn)//if we don't do something like that, game may get really slow when there are a lot of fires started
				{
					if(Time.time - FireStartedTime > TimeToBurnOthers * (numberOfFiresStarted+1))
					{
						//Burn nearest tree

						//float shortestDistance = 999999;
						//int shortestIndex = -1;
						List<GameObject> allTrees = new List<GameObject>();// = GameObject.FindObjectsOfType(typeof(Tree)) as Tree[];

						Collider[] cols = Physics.OverlapSphere(transform.position, FireRadius);
						

						foreach (var col in cols)
						{
							if (col.attachedRigidbody && col.attachedRigidbody.gameObject.tag == "Tree")
							{

								//Debug.Log(col.name);
								//allTrees.Add(col.attachedRigidbody.gameObject);
								//col.attachedRigidbody.gameObject.GetComponent<TreeScript>().SetThisOnFire();
								GameObject treeObject = col.attachedRigidbody.gameObject.transform.parent.gameObject;

								if(treeObject != gameObject && treeObject.GetComponent<TreeScript>().TreeOnFire==false)
								{
									//Debug.Log(subModel.name);
									//GameObject model = subModel.transform.parent.gameObject;
									//GameObject treeObject = model.transform.parent.gameObject;
									treeObject.GetComponent<TreeScript>().SetThisOnFire();
									
									numberOfFiresStarted = numberOfFiresStarted + 1;
									
									if(MaxTreesToBurn<numberOfFiresStarted)
									{
										break;
									}
									
								}


							

							}
						}



						/*for (int i=0;i<allTrees.Length;i++) {

							Transform treeTrans = allTrees[i].transform;
							float dist = Vector3.Distance (treeTrans.position,transform.position);
							if(dist < shortestDistance)
							{
								dist= shortestDistance;
								shortestIndex = i;
							}
						}*/

						/*if(shortestIndex>-1)
						{
							numberOfFiresStarted = numberOfFiresStarted + 1;
							allTrees[shortestIndex].GetComponent<TreeScript>().SetThisOnFire();
						}

*/



					}
				}

			}



		}
		if(!TreeOnFire)
		{
			OnFireParticleSystem.Stop ();
		}
	
	}
}


