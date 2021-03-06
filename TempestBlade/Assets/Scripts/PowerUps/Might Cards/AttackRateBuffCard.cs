﻿using UnityEngine;
using System.Collections;

public class AttackRateBuffCard : PowerUpCard {
	
	public float _buffValue;
	public float _buffLength;

	// Use this for initialization
	void Awake () {
		Init ();
	}

	public override void Init()
	{
		_type = CardType.ATTACK_RATE_BUFF;
		_effectBuff = Cache.instance.cardEffect [_type];
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
}
