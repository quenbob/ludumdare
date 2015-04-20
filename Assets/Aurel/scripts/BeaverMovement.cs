using UnityEngine;
using System.Collections;

public class BeaverMovement : MonoBehaviour 
{
	public float shakingIntensity = 5.0f;
	public bool isShaking = false;

	private float lastRotation = 0.0f;
	private PlayerAttack playerAttack;
	private Quaternion originRotation;

	// Use this for initialization
	void Start () 
	{
		originRotation = transform.localRotation;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player)
		{
			playerAttack = player.GetComponent<PlayerAttack>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(playerAttack && playerAttack.treeInRange)
		{
			isShaking = true;
			float nextRotation = (lastRotation != 0.0f) ? -lastRotation : shakingIntensity;
			lastRotation = nextRotation;
			transform.localRotation = originRotation;
			transform.Rotate(new Vector3(nextRotation, 0.0f, 0.0f));
			transform.Rotate(new Vector3(0.0f, nextRotation, 0.0f));
		}
		else
		{
			if (lastRotation != 0.0f)
			{
				transform.Rotate(new Vector3(-lastRotation, 0.0f, 0.0f));
				transform.Rotate(new Vector3(0.0f, -lastRotation, 0.0f));
				transform.localRotation = originRotation;
				lastRotation = 0.0f;
			}
			isShaking = false;
		}
	}
}
