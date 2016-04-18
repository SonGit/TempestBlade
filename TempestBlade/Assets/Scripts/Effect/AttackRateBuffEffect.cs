using UnityEngine;
using System.Collections;

public class AttackRateBuffEffect : AttributeBuff {

	public AttackRateBuffEffect(float buffValue,float buffLength)
	{
		AttackRate += buffValue;
		BuffLength = buffLength;
		type = BuffType.BUFF;
	}

	public override void OnEndEffect (Soldier soldier)
	{
		soldier._AttackRate = 1;
	}
}
