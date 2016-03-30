using UnityEngine;
using System.Collections;

public class ScorchedEarthCard : PowerUpCard {

	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Init()
	{
		_type = CardType.SCORCHED_EARTH;
	}

	public override void Execute(SquadLeader target)
	{
		float num = 20;

		for (int i = 0; i < num; i++) {

			GameObject go = (GameObject)Instantiate (_prefab);
			SkyfallObject skyfall_object = go.GetComponent<SkyfallObject> ();
			skyfall_object.Fall (target);
		}
	}

}
