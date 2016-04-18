using UnityEngine;
using System.Collections;

public class AttackDebuffEffect : AttributeBuff {

	public AttackDebuffEffect(float buffValue,float buffLength)
	{
		DmgMultiplier -= buffValue;
		BuffLength = buffLength;
		type = BuffType.DEBUFF;
	}

	public override void OnEndEffect (Soldier soldier)
	{
		soldier._DmgMultiplier = 1;
	}
}
