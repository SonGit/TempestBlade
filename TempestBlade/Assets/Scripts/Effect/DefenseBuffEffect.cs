using UnityEngine;
using System.Collections;

public class DefenseBuffEffect : AttributeBuff {

	public DefenseBuffEffect(float buffValue,float buffLength)
	{
		DfMultiplier += buffValue;
		BuffLength = buffLength;
		type = BuffType.BUFF;
	}

	public override void OnEndEffect (Soldier soldier)
	{
		soldier._DfMultiplier = 1;
	}

}
