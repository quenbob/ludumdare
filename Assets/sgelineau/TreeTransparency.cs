using UnityEngine;
using System.Collections;

public class TreeTransparency : MonoBehaviour {

	public Shader transparentShader;
	public Shader opaqueShader;
	
	public void makeTransparent() {
		setAlpha (0.1f);
	}

	public void makeOpaque() {
		setAlpha (1);
	}

	public void setAlpha(float alpha) {
		if (alpha == 1) {
			transform.Find("Grown/Tree model/TreeTrunk").gameObject.GetComponent<Renderer> ().material.shader = opaqueShader;
			transform.Find("Grown/Tree model/TreeTop1").gameObject.GetComponent<Renderer> ().material.shader = opaqueShader;
			transform.Find("Grown/Tree model/TreeTop2").gameObject.GetComponent<Renderer> ().material.shader = opaqueShader;
			transform.Find("Grown/Tree model/TreeTop3").gameObject.GetComponent<Renderer> ().material.shader = opaqueShader;
			transform.Find("Grown/Tree model/TreeTop4").gameObject.GetComponent<Renderer> ().material.shader = opaqueShader;
		} else {
			transform.Find("Grown/Tree model/TreeTrunk").gameObject.GetComponent<Renderer> ().material.shader = transparentShader;
			transform.Find("Grown/Tree model/TreeTop1").gameObject.GetComponent<Renderer> ().material.shader = transparentShader;
			transform.Find("Grown/Tree model/TreeTop2").gameObject.GetComponent<Renderer> ().material.shader = transparentShader;
			transform.Find("Grown/Tree model/TreeTop3").gameObject.GetComponent<Renderer> ().material.shader = transparentShader;
			transform.Find("Grown/Tree model/TreeTop4").gameObject.GetComponent<Renderer> ().material.shader = transparentShader;
			transform.Find("Grown/Tree model/TreeTrunk").gameObject.GetComponent<Renderer> ().material.SetFloat ("_AlphaMultiplier", alpha);
			transform.Find("Grown/Tree model/TreeTop1").gameObject.GetComponent<Renderer> ().material.SetFloat ("_AlphaMultiplier", alpha);
			transform.Find("Grown/Tree model/TreeTop2").gameObject.GetComponent<Renderer> ().material.SetFloat ("_AlphaMultiplier", alpha);
			transform.Find("Grown/Tree model/TreeTop3").gameObject.GetComponent<Renderer> ().material.SetFloat ("_AlphaMultiplier", alpha);
			transform.Find("Grown/Tree model/TreeTop4").gameObject.GetComponent<Renderer> ().material.SetFloat ("_AlphaMultiplier", alpha);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
