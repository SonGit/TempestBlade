using UnityEngine;
using System.Collections;

public enum BuffType
{
	BUFF,
	DEBUFF
}

public abstract class AttributeBuff {

	public float DmgMultiplier = 1;
	public float DfMultiplier = 1;
	public float AttackRate = 1;
	public int MoraleRate = 1;
	public float BuffLength = 1;
	public int NumStack = 0;

	public BuffType type;

	public AttributeBuff()
	{

	}

	public void ApplyStack(StackableSkill skill)
	{
		if (skill == null)
			return;

		Debug.Log ("BuffLength1  " + BuffLength);
		if (skill != null) {
			var currentInstance = this;
			skill.ModifyBuff (ref currentInstance);
		}

		Debug.Log ("BuffLength2 " + BuffLength);
	}

	public void ApplyTo(Soldier soldier)
	{
		AttributeEffect(soldier);
		SpecialEffect  (soldier);
	}

	public void ApplyTo(SquadLeader leader)
	{
		LeaderEffect (leader);
	}

	protected virtual void AttributeEffect(Soldier soldier)
	{
		if(DmgMultiplier != 1)
			soldier._DmgMultiplier = DmgMultiplier ;
		
		if(DfMultiplier != 1)
			soldier._DfMultiplier = DfMultiplier ;
		
		if(AttackRate != 1)
			soldier._AttackRate = AttackRate ;
	}
	protected virtual void SpecialEffect (Soldier soldier)
	{
		AttributeBuffUIEffect effect = ObjectPool.instance.GetBuffObject ().GetComponent<AttributeBuffUIEffect>();

		effect.transform.SetParent(soldier.transform);

		effect.transform.position = new Vector3 (0,5.5f,0);
			
		if (effect != null) {
			effect.Play ();
		}
	}

	public abstract void OnEndEffect (Soldier soldier);

	protected virtual void LeaderEffect(SquadLeader leader)
	{

	}
}
