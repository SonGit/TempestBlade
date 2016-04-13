using UnityEngine;
using System.Collections;

public class ElectricGroundCard : PowerUpCard {

	public float _buffValue;
	public float _buffLength;

	// Use this for initialization
	void Awake () {
		Init ();
	}

	// Update is called once per frame
	void Update () {

	}

	public override void Init()
	{
		_type = CardType.ELECTRIC_GROUND;
	}

	protected override void PlayEffect(SquadLeader target)
	{
		if (_prefab == null) {
			print ("Null prefab!");
			return;
		}

		Vector3 targetPos = target.transform.position;
		//targetPos.y += 10;

		GameObject go = (GameObject)Instantiate (_prefab, targetPos, _prefab.transform.rotation);

		ParticleSystem particle = go.GetComponent<ParticleSystem> ();
		if (particle != null) {
			//particle.startLifetime  = _buffLength;
		} else {
			print ("Null particle");
		}
	}

	protected override void ApplyEffect(SquadLeader target)
	{

	}
}
