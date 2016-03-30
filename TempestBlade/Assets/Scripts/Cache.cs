using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Cache {

	static readonly Cache _instance = new Cache();

	public static Cache instance
	{
		get { return _instance; }
	}

	public Dictionary<CardType,string> cardIconPaths = new Dictionary<CardType,string>
	{
		{CardType.NULL,""},
		{CardType.SCORCHED_EARTH,"Sprites/Cards/ScorchedEarth_Icon"},
		{CardType.DEFENSE_BUFF,"Sprites/Cards/DefenseBuff_Icon"},
		{CardType.ATTACK_BUFF,"Sprites/Cards/AttackBuff_Icon"},
		{CardType.MORALE_DEBUFF,"Sprites/Cards/MoraleDebuff_Icon"},
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
}
