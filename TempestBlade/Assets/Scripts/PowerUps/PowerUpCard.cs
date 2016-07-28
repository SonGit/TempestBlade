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
		ConsumeStack ();
		PlayEffect (target);
		BuildStack ();
		ApplyEffect (target);
	}

	public virtual void  ConsumeStack()
	{
		StackableSkill currentStack = StackManager.instance.GetStackSkill ();

		if (currentStack == null) 
			return;
		
		if (!currentStack.Equals(_stackSkill)) {
			_effectBuff.ApplyStack (  currentStack );
			StackManager.instance.ResetStack ();
		}
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

		//Reset
		_effectBuff = Cache.instance.cardEffect [_type];
	}
}
