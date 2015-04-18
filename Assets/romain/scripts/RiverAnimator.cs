using UnityEngine;
using System.Collections;

public class RiverAnimator : MonoBehaviour {
	public float scrollSpeed = 0.20f;
	public float offset = 0f;
	
	void Update () {
		offset += (Time.deltaTime * scrollSpeed) / 10.0f;
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}