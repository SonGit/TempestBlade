using UnityEngine;
using System.Collections;

public class ProlongStack : StackableSkill {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void ModifyBuff(ref AttributeBuff buff)
	{
		float bonus = numStack * 0.1f;
		buff.BuffLength += 1f * numStack;
	}
}
