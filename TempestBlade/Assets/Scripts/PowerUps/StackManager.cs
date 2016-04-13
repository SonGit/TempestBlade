using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum StackType
{
	NULL,
	SOLDIER_ATTACK_UP,
	MAGIC_ATTACK_UP,
};

public class StackManager : MonoBehaviour {

	public static StackManager instance;

	public GridLayoutGroup _grid;

	StackableSkillSlot[] _stackSlots;

	// Use this for initialization
	void Awake () {
		instance = this;
		_stackSlots = _grid.GetComponentsInChildren<StackableSkillSlot> ();
	}

	public void AddStack(StackableSkill skill)
	{
		foreach (StackableSkillSlot slot in _stackSlots) {
			if (slot.CanAddStack (skill)) {
				return;
			}
		}
	}

	public int ConsumeStack(StackableSkill skill)
	{
		int numStack = 0;
		foreach (StackableSkillSlot slot in _stackSlots) {
			if (slot.CanConsume (skill)) {
				numStack = slot._currentStacks;
				slot.ConsumeStack ();
				return numStack;
			}
		}
		return numStack;
	}
}
