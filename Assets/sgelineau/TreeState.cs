using UnityEngine;
using System.Collections;

public enum TreeStateEnum {
	Sprout, Grown, Stump
}

public class TreeState : MonoBehaviour {

	public TreeStateEnum initialState = TreeStateEnum.Grown;

	public float secondsUntilGrownSprout = 1;
	public float secondsBetweenGrowthPhases = 0.5f;
	public float springStrength = 1;
	public float springPhaseInitialScale = 0.1f;
	public float springPhaseTargetScale = 1f;


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
			// animate the growth of the tree
			t += Time.deltaTime;

			switch (animationPhase) {
			case 0:
			{
				// sprout growth

				float fraction = t / secondsUntilGrownSprout;
				if (fraction >= 1) fraction = 1;
				fraction = Mathf.Sqrt(fraction); // slow down towards the end

				float scaleFactor = fraction;

				sprout.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

				if (t >= secondsUntilGrownSprout) {
					t = 0;
					++animationPhase;
				}
				break;
			}
			case 1:
			{
				// pause between the two phases

				if (t >= secondsBetweenGrowthPhases) {
					t = 0;
					++animationPhase;

					currentState = TreeStateEnum.Grown;

					float scaleFactor = springPhaseInitialScale;
					grown.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
				}
				break;
			}
			case 2:
				break;
			}
		}
	}
}
