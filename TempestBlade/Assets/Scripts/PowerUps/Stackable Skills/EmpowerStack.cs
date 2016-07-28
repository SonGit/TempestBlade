using UnityEngine;
using System.Collections;

public class EmpowerStack : StackableSkill {

	// Use this for initialization
	void Start () {
	
	}
	
	public override void ModifyBuff(ref AttributeBuff buff)
	{
		float bonus = numStack * 0.1f;

		if (buff.DmgMultiplier != 1)
			buff.DmgMultiplier += bonus;

		if(buff.DfMultiplier != 1)
			buff.DfMultiplier += bonus;

		if(buff.AttackRate != 1)
			buff.AttackRate += bonus;
	}
}
