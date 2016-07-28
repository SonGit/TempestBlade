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
		_stackSlots [0].IncrementStack (skill);
	}

	public StackableSkill GetStackSkill()
	{
		return _stackSlots [0].stackSkill;
	}

	public void ResetStack()
	{
		if (_stackSlots [0] != null)
			_stackSlots [0].Reset ();
	}


}
