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
	}

	protected override void PlayEffect(SquadLeader target)
	{

	}

	protected override void ApplyEffect(SquadLeader target)
	{
		int numStack = GetNumStack (); //Get existing stacks 
		float value = _buffValue;

		if (numStack > 0) //Add bonus
			value += (numStack * 0.2f);

		print ("_buffValue   " + _buffValue + "  value  " + value);

		target.DefenseBuff (_buffLength,value);
	}
}
