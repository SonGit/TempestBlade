using UnityEngine;
using System.Collections;

public class AvariceStack : StackableSkill {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void ModifyBuff( ref AttributeBuff buff)
	{
		float bonus = numStack * 0.1f;
		buff.BuffLength += 2 * numStack;

		if (buff.DmgMultiplier != 1)
			buff.DmgMultiplier -= bonus;

		if(buff.DfMultiplier != 1)
			buff.DfMultiplier -= bonus;

		if(buff.AttackRate != 1)
			buff.AttackRate -= bonus;
	}
}
