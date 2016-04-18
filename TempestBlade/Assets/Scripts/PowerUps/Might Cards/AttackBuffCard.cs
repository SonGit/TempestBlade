using UnityEngine;
using System.Collections;
using Shared;

public class AttackBuffCard : PowerUpCard {

	// Use this for initialization
	void Awake () {
		Init ();
	}

	public override void Init()
	{
		_type = CardType.ATTACK_BUFF;
		_effectBuff = Cache.instance.cardEffect [_type];
	}

	protected override void PlayEffect(SquadLeader target)
	{

	}

}
