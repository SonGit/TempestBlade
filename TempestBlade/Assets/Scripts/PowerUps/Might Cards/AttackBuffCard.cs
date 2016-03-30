using UnityEngine;
using System.Collections;

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

	public override void Execute(SquadLeader target)
	{
		target.AttackBuff (_buffLength,_buffValue);
	}
}
