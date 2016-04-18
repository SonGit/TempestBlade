using UnityEngine;
using System.Collections;
using Shared;

public abstract class PowerUpCard : MonoBehaviour {

	public CardType _type;
	public GameObject _prefab;
	public StackableSkill _stackSkill;
	public StackableSkill _consumedStack;
	public AttributeBuff _effectBuff;

	public abstract void Init();
	protected abstract void PlayEffect(SquadLeader target);

	public void Activate(SquadLeader target)
	{
		PlayEffect (target);
		BuildStack ();
		ApplyEffect (target);
	}

	public virtual int GetNumStack()
	{
		if (_consumedStack != null) {
			return StackManager.instance.ConsumeStack (_consumedStack);
		}
		return 0;
	}

	public virtual void BuildStack()
	{
		if (_stackSkill != null) {
			StackManager.instance.AddStack (_stackSkill);
		}
	}

	protected virtual void ApplyEffect(SquadLeader target)
	{
		if(_effectBuff != null)
		target.ReceiveBuff(_effectBuff);
	}
}
