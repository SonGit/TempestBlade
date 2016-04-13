using UnityEngine;
using System.Collections;
using Shared;

public class AttackBuffCard : PowerUpCard {

	public float _buffValue;
	public float _buffLength;

	// Use this for initialization
	void Awake () {
		Init ();
	}

	public override void Init()
	{
		_type = CardType.ATTACK_BUFF;
	}

	protected override void PlayEffect(SquadLeader target)
	{

	}

	protected override void ApplyEffect(SquadLeader target)
	{
		target.AttackBuff (_buffLength,_buffValue);
	}
}
