using UnityEngine;
using System.Collections;

public enum TreeStateEnum {
	Sprout, Grown, Stump
}

public class TreeState : MonoBehaviour {

	public TreeStateEnum initialState = TreeStateEnum.Grown;

	public float secondsUntilGrownSprout = 1;
	public float secondsBetweenGrowthPhases = 1;
	public float springStrength = 1;
	public float springPhaseInitialScale = 0.1f;


	private TreeStateEnum privateState;
	public TreeStateEnum currentState {
		get { return privateState; }
		set {
			if (value == TreeStateEnum.Sprout && privateState != TreeStateEnum.Sprout) {
				t = 0;
				isSprouting = true;
				animationPhase = 0;
			}

			privateState = value;
			
			sprout.SetActive(privateState == TreeStateEnum.Sprout);
			grown.SetActive(privateState == TreeStateEnum.Grown);
			stump.SetActive(privateState == TreeStateEnum.Stump);
		}
	}

	GameObject sprout;
	GameObject grown;
	GameObject stump;

	float t = 0;
	bool isSprouting = false;
	int animationPhase = 0;

	// Use this for initialization
	void Start () {
		sprout = GameObject.Find("Sprout");
		grown = GameObject.Find("Grown");
		stump = GameObject.Find("Stump");

		currentState = initialState;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSprouting) {
			// animate the growth of the sprout
			t += Time.deltaTime;

			switch (animationPhase) {
			case 0:
				float scaleFactor = t;
				if (scaleFactor >= 1) scaleFactor = 1;

				sprout.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

				if (scaleFactor == 1) {
					t = 0;
					animationPhase = 1;
				}
				break;
			case 1:
				break;
			case 2:
				break;
			}
		}
	}
}
