
using System.Collections;

public abstract class StackableSkill {

	public float duration;

	public int numStack = 0;

	public abstract void ModifyBuff (ref AttributeBuff buff);

}
