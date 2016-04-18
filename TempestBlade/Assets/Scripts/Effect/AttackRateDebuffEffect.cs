using UnityEngine;
using System.Collections;

public class AttackRateDebuffEffect : AttributeBuff {

	public AttackRateDebuffEffect(float buffValue,float buffLength)
	{
		AttackRate -= buffValue;
		BuffLength = buffLength;
		type = BuffType.DEBUFF;
	}

	public override void OnEndEffect (Soldier soldier)
	{
		soldier._AttackRate = 1;
	}
}
