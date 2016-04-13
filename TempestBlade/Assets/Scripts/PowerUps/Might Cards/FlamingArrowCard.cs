using UnityEngine;
using System.Collections;

public class FlamingArrowCard : PowerUpCard {

	// Use this for initialization
	void Awake () {
		Init ();
	}

	// Update is called once per frame
	void Update () {

	}

	public override void Init()
	{
		_type = CardType.FLAMING_ARROW;
	}

	protected override void PlayEffect(SquadLeader target)
	{
		float num = 40;

		for (int i = 0; i < num; i++) {

			GameObject go = (GameObject)Instantiate (_prefab);
			SkyfallObject skyfall_object = go.GetComponent<SkyfallObject> ();
			skyfall_object.Fall (target);
		}
	}

	protected override void ApplyEffect(SquadLeader target)
	{

	}
}
