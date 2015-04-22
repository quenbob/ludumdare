using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeScript : MonoBehaviour {

	public bool TreeOnFire = false;
	public float TimeToBurnOthers = 7.0f;
	public int MinTreesToBurn = 1;
	public int MaxTreesToBurn = 3;
	public float FireRadius = 15.0f;
	public float MinBurnLifetime = 18.0f;
	public float MaxBurnLifetime = 22.0f;

	private float FireStartedTime = -1.0f;
	private int numberOfFiresStarted = 0;
	private bool fireStarted = false;
	private ParticleSystem OnFireParticleSystem;
	private Transform TreeTopTr;
	private int treeToBurn;
	private float BurnLifetime;

	// Use this for initialization
	void Awake () {
		initParticule ();
		initTreeTopTr();

		treeToBurn = Random.Range(MinTreesToBurn, MaxTreesToBurn);
		BurnLifetime = Random.Range(MinBurnLifetime, MaxBurnLifetime);
	}

	private void initParticule()
	{
		if (!OnFireParticleSystem) {
			Transform treefireTr = transform.Find ("TreeFire");
		
			if (treefireTr) {
				GameObject treefire = treefireTr.gameObject;
			
				if (treefire) 
					OnFireParticleSystem = treefire.GetComponent<ParticleSystem> ();
			}
		}
	}

	private void initTreeTopTr()
	{
		if (!TreeTopTr)
			TreeTopTr = transform.Find ("Model/Grown/Tree model/FireHandle");
	}


	public void SetThisOnFire()
	{
		if (!GetComponentInChildren<TreeFalling>().isDying)
		{
			initParticule ();
			TreeOnFire = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(TreeOnFire)
		{
			if(!fireStarted)
			{
				fireStarted = true;
				if (OnFireParticleSystem)
					OnFireParticleSystem.Play ();
			}

			if(FireStartedTime<0)
			{
				FireStartedTime = Time.time;

			}
			else if((Time.time - FireStartedTime)>BurnLifetime) {
				GetComponentInChildren<TreeFalling>().cutDownFrom(new Vector3(Random.Range(-10000.0f, 10000.0f),10.0f,Random.Range(-10000.0f, 10000.0f)),false);
				OnFireParticleSystem.gameObject.transform.position = Vector3.Lerp(OnFireParticleSystem.gameObject.transform.position, TreeTopTr.position, 10.0f * Time.deltaTime);
			}

			if(numberOfFiresStarted < treeToBurn)
			{
				if((Time.time - FireStartedTime) > (TimeToBurnOthers * (numberOfFiresStarted+1)))
				{
					//Burn nearest tree
					Collider[] cols = Physics.OverlapSphere(transform.position, FireRadius);

					foreach (var col in cols)
					{
						if (col.attachedRigidbody && col.attachedRigidbody.gameObject.tag == "Tree")
						{
							GameObject treeObject = col.attachedRigidbody.gameObject.transform.parent.gameObject;

							if(treeObject != gameObject && treeObject.GetComponentInChildren<TreeScript>().TreeOnFire==false)
							{
								treeObject.GetComponentInChildren<TreeScript>().SetThisOnFire();
								
								numberOfFiresStarted = numberOfFiresStarted + 1;
								
								if(treeToBurn<numberOfFiresStarted)
								{
									break;
								}
							}
						}
					}
				}
			}
		}
	}
}


