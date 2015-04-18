using UnityEngine;
using System.Collections;

public class RiverAnimator : MonoBehaviour {
	float scrollSpeed = 0.35f;
	float offset = 0f;
	
	void Update () {
		offset += (Time.deltaTime * scrollSpeed) / 10.0f;
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}