using UnityEngine;
using System.Collections;

public enum TreeStateEnum {
	Sprout, Grown, Stump
}

public enum TreeTypeEnum {
	Type1 = 1,
	Type2 = 2,
	Type3 = 3,
	Type4 = 4
}

public class TreeState : MonoBehaviour {

	public TreeStateEnum initialState = TreeStateEnum.Sprout;

	public bool useGrownModelAsSproutModel = true;
	public float secondsUntilGrownSprout = 1;
	public float secondsBetweenGrowthPhases = 0.5f;
	public float springStrength = 50;
	public float springDamping = 3;
	public float sproutPhaseTargetScale = 0.1f;
	public float springPhaseInitialScale = 0.1f;
	public float springPhaseTargetScale = 1f;

	private TreeTypeEnum mtreeType = TreeTypeEnum.Type1;
	public TreeTypeEnum treeType
	{
		get { return mtreeType; }
		set
		{
			if (mtreeType != value)
			{
				mtreeType = value;

				transform.Find("Grown/Tree model/TreeTop1").gameObject.SetActive(false);
				transform.Find("Grown/Tree model/TreeTop2").gameObject.SetActive(false);
				transform.Find("Grown/Tree model/TreeTop3").gameObject.SetActive(false);
				transform.Find("Grown/Tree model/TreeTop4").gameObject.SetActive(false);

				switch(mtreeType)
				{
				case TreeTypeEnum.Type1:
					transform.Find("Grown/Tree model/TreeTop1").gameObject.SetActive(true);
					break;
				case TreeTypeEnum.Type2:
					transform.Find("Grown/Tree model/TreeTop2").gameObject.SetActive(true);
					break;
				case TreeTypeEnum.Type3:
					transform.Find("Grown/Tree model/TreeTop3").gameObject.SetActive(true);
					break;
				case TreeTypeEnum.Type4:
					transform.Find("Grown/Tree model/TreeTop4").gameObject.SetActive(true);
					break;
				}
			}
		}
	}

	private TreeStateEnum privateState = TreeStateEnum.Grown;
	public TreeStateEnum currentState {
		get { return privateState; }
		set {
			if (value == TreeStateEnum.Sprout && privateState != TreeStateEnum.Sprout) {
				t = 0;
				isSprouting = true;
				animationPhase = 0;

				if (useGrownModelAsSproutModel)
					grown.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
				else
					sprout.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
			}

			privateState = value;

			if (useGrownModelAsSproutModel) {
				sprout.SetActive(false);
				grown.SetActive(privateState == TreeStateEnum.Sprout || privateState == TreeStateEnum.Grown);
				stump.SetActive(privateState == TreeStateEnum.Stump);
			} else {
				sprout.SetActive(privateState == TreeStateEnum.Sprout);
				grown.SetActive(privateState == TreeStateEnum.Grown);
				stump.SetActive(privateState == TreeStateEnum.Stump);
			}
		}
	}

	GameObject sprout;
	GameObject grown;
	GameObject stump;

	bool isSprouting = false;
	float t;
	int animationPhase;
	float scalePosition;
	float scaleVelocity;

	// Use this for initialization
	void Start () {
		sprout = transform.Find("Sprout").gameObject;
		grown = transform.Find("Grown").gameObject;
		stump = transform.Find("Stump").gameObject;

		currentState = initialState;

		treeType = (TreeTypeEnum)Random.Range(1, 4);
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

				float scaleFactor = fraction * sproutPhaseTargetScale;

				if (useGrownModelAsSproutModel) {
					grown.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
				} else {
					sprout.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
				}

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

					scalePosition = springPhaseInitialScale;
					scaleVelocity = 0;
				}
				break;
			}
			case 2:
			{
				// spring to full size

				float acceleration = springStrength * (springPhaseTargetScale - scalePosition) - springDamping * scaleVelocity;
				scaleVelocity = scaleVelocity + acceleration * Time.deltaTime;
				float newScalePosition = scalePosition + scaleVelocity * Time.deltaTime;
				
				float scaleFactor = newScalePosition;
				grown.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

				if (Mathf.Abs (acceleration) < 1e-2 && Mathf.Abs(newScalePosition - scalePosition) < 1e-2) {
					t = 0;
					++animationPhase;

					isSprouting = false;
				} else {
					scalePosition = newScalePosition;
				}
				break;
			}
			}
		}
	}
}
