using UnityEngine;
using System.Collections;

public class MoraleDebuffEffect : AttributeBuff {

	public MoraleDebuffEffect(int buffValue)
	{
		MoraleRate = buffValue;
		type = BuffType.DEBUFF;
	}

	protected override void LeaderEffect(SquadLeader leader)
	{
		leader._moraleRestoreRate -= MoraleRate;
	}

	public override void OnEndEffect (Soldier soldier)
	{
		
	}

}
