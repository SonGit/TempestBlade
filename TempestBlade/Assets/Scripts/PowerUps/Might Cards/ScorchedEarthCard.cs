using UnityEngine;
using System.Collections;
using Shared;

public class ScorchedEarthCard : PowerUpCard {

	// Use this for initialization
	void Awake () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Init()
	{
		_type = CardType.SCORCHED_EARTH;
		_stackSkill = new SoldierAttackUpStack ();
	}

	protected override void PlayEffect(SquadLeader target)
	{
		float num = 20;

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
