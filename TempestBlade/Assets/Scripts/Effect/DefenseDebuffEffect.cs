using UnityEngine;
using System.Collections;

public class DefenseDebuffEffect : AttributeBuff {

	public DefenseDebuffEffect(float buffValue,float buffLength)
	{
		DfMultiplier -= buffValue;
		BuffLength = buffLength;
		type = BuffType.DEBUFF;
	}

	public override void OnEndEffect (Soldier soldier)
	{
		soldier._DfMultiplier = 1;
	}
}
