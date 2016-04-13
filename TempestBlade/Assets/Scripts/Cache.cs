using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Cache {

	static readonly Cache _instance = new Cache();

	public static Cache instance
	{
		get { return _instance; }
	}

	public Dictionary<StackType,string> stackableSkillIconPaths = new Dictionary<StackType,string>
	{
		{StackType.NULL,""},
		{StackType.SOLDIER_ATTACK_UP,"Sprites/StackSkill/SoldierAttackUp_Icon"},
		{StackType.MAGIC_ATTACK_UP,"Sprites/StackSkill/MagicAttackUp_Icon"},
	};

	public Dictionary<CardType,string> cardIconPaths = new Dictionary<CardType,string>
	{
		{CardType.NULL,""},
		{CardType.SCORCHED_EARTH,"Sprites/Cards/ScorchedEarth_Icon"},
		{CardType.DEFENSE_BUFF,"Sprites/Cards/DefenseBuff_Icon"},
		{CardType.ATTACK_BUFF,"Sprites/Cards/AttackBuff_Icon"},
		{CardType.MORALE_DEBUFF,"Sprites/Cards/MoraleDebuff_Icon"},
		{CardType.ATTACK_RATE_DEBUFF,"Sprites/Cards/AttackRateDebuff_Icon"},
		{CardType.ATTACK_RATE_BUFF,"Sprites/Cards/AttackRateBuff_Icon"},
		{CardType.WIND_OF_RUST,"Sprites/Cards/WindOfRust_Icon"},
		{CardType.FLAMING_ARROW,"Sprites/Cards/FlamingArrow_Icon"},
		{CardType.ELECTRIC_GROUND,"Sprites/Cards/ElectricGround_Icon"},
		{CardType.FRENZY,"Sprites/Cards/Frenzy_Icon"},
	};

	public Dictionary<SoldierType,string> soldierIconPaths = new Dictionary<SoldierType,string>
	{
		{SoldierType.NULL,""},
		{SoldierType.FOOTMAN,"Sprites/Soldiers/Footman_Icon"},
		{SoldierType.KNIGHT_TIER_1,"Sprites/Soldiers/Knight_Tier1_Icon"},
	};

	public const int MAX_CARDS_PER_BATTLE = 5;
	public const int MAX_SOLDIER_CARDS_PER_BATTLE = 5;

	private Cache()
	{
		// Initialize members here.
	}

	public const int MORALE_MAX = 100;
	public const int MORALE_RESTORE_RATE = 2;
	public const float MORALE_CHECK_RATE = 1.5f;
	public const int MORALE_PENALTY_LOST_UNIT= 2;
	public const float MORALE_PENALTY_CHECK_RATE= 0.5f;

	public const int STACK_MAX = 5;
}
