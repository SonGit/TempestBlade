using UnityEngine;
using System.Collections;

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
	}

	public override void Execute(SquadLeader target)
	{
		target.DefenseBuff (_buffLength,_buffValue);
	}
}
