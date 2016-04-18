using UnityEngine;
using System.Collections;

public class MoraleBuffEffect : AttributeBuff {

	public MoraleBuffEffect(int buffValue)
	{
		MoraleRate = buffValue;
		type = BuffType.BUFF;
	}

	protected override void LeaderEffect(SquadLeader leader)
	{
		leader._moraleRestoreRate += MoraleRate;
	}

	public override void OnEndEffect (Soldier soldier)
	{

	}

}
