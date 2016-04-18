using UnityEngine;
using System.Collections;

public class AttackBuffEffect : AttributeBuff {

	public AttackBuffEffect(float buffValue,float buffLength)
	{
		DmgMultiplier += buffValue;
		BuffLength = buffLength;
		type = BuffType.BUFF;
	}

	public override void OnEndEffect (Soldier soldier)
	{
		soldier._DmgMultiplier = 1;
	}
}
