﻿using UnityEngine;
using System.Collections;

public class FrenzyCard : PowerUpCard {
	public float _buffValue;
	public float _buffLength;

	// Use this for initialization
	void Awake () {
		Init ();
	}

	public override void Init()
	{
		_type = CardType.FRENZY;
	}

	protected override void PlayEffect(SquadLeader target)
	{
		if (_prefab == null) {
			print ("Null prefab!");
			return;
		}

		Vector3 targetPos = target.transform.position;
		targetPos.y += 10;

		GameObject go = (GameObject)Instantiate (_prefab, targetPos, _prefab.transform.rotation);
	}

	protected override void ApplyEffect(SquadLeader target)
	{
		SquadLeader ally = TacticalAnalyst.instance._allyLeader;
		SquadLeader enemy = TacticalAnalyst.instance._enemyLeader;

		ally.AttackBuff (_buffLength,_buffValue);
		enemy.AttackBuff (_buffLength,_buffValue);
	}
}