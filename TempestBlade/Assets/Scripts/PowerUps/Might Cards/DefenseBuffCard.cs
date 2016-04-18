using UnityEngine;
using System.Collections;
using Shared;

public class DefenseBuffCard : PowerUpCard {

	public float _buffValue;
	public float _buffLength;

	// Use this for initialization
	void Awake () {
		Init ();
	}
	
	public override void Init()
	{
		_type = CardType.DEFENSE_BUFF;
		_stackSkill = new MagicAttackUpStack ();
		_consumedStack = new SoldierAttackUpStack ();
		_effectBuff = Cache.instance.cardEffect [_type];
	}

	protected override void PlayEffect(SquadLeader target)
	{

	}
		
}
