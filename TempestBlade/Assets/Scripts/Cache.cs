using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SceneryType
{
	DESERT,
}

public enum WeatherType
{
	NONE,
	RAIN
}

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

	public Dictionary<CardType,AttributeBuff> cardEffect = new Dictionary<CardType,AttributeBuff>
	{
		{CardType.NULL,null},
		{CardType.SCORCHED_EARTH,null},
		{CardType.DEFENSE_BUFF,    new DefenseBuffEffect ( 0.24f , 15f )   },
		{CardType.ATTACK_BUFF,     new AttackBuffEffect  ( 0.24f , 25f )   },
		{CardType.MORALE_DEBUFF,   new MoraleDebuffEffect  ( 2 )   },
		{CardType.ATTACK_RATE_DEBUFF, new AttackRateDebuffEffect  ( 1f , 15f )   },
		{CardType.ATTACK_RATE_BUFF, new AttackRateBuffEffect  ( 1f , 15f )   },
		{CardType.WIND_OF_RUST,new AttackRateDebuffEffect  ( 0.24f , 5f )    },
		{CardType.FLAMING_ARROW,null},
		{CardType.ELECTRIC_GROUND,null},
		{CardType.FRENZY,new AttackRateBuffEffect  ( 1f , 15f ) },
	};


	public Dictionary<SoldierType,string> soldierIconPaths = new Dictionary<SoldierType,string>
	{
		{SoldierType.NULL,""},
		{SoldierType.KNIGHT,"Sprites/Soldiers/Footman_Icon"},
	};

	public Dictionary<SoldierType,string> soldierPrefabPaths = new Dictionary<SoldierType,string>
	{
		{SoldierType.NULL,"Prefabs/Soldiers/Knight"},
		{SoldierType.KNIGHT,"Prefabs/Soldiers/Knight"},
		{SoldierType.VINDICATOR,"Prefabs/Soldiers/Vindicator"},
		{SoldierType.BRUTE,"Prefabs/Soldiers/Brute"},
		{SoldierType.BERSERKER,"Prefabs/Soldiers/Berserker"},
		{SoldierType.BONE_DRAGON,"Prefabs/Soldiers/BoneDragon"},
		{SoldierType.IRON_GOLEM,"Prefabs/Soldiers/IronGolem"},
		{SoldierType.DEFENDER, "Prefabs/Soldiers/Defender"},
		{SoldierType.ROGUE,"Prefabs/Soldiers/Rogue"},
		{SoldierType.MINOTAUR,"Prefabs/Soldiers/Minotaur"},
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


	public Dictionary<Music,string> musicPaths = new Dictionary<Music,string>
	{
		{Music.Realm_Of_Fantasy,"Musics/Realm-of-Fantasy"},
		{Music.Dragon_Mystery,"Musics/Dragon-Mystery"},
		{Music.Clock_Maker,"Musics/Clock-Maker-the-Hero"},
	};

	public Dictionary<SceneryType,SceneryData> backgroundData = new Dictionary<SceneryType,SceneryData>
	{
		{SceneryType.DESERT,new SceneryData("Sprites/Sceneries/Desert/BG","Sprites/Sceneries/Desert/TR")},
	};

	public Dictionary<int,MapProperties> levelData = new Dictionary<int,MapProperties>
	{
		{ 0,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 1,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 2,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 3,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 4,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 5,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 6,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT}, SceneryType.DESERT , WeatherType.NONE )},

		{ 7,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 8,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 9,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 10,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 11,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 12,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 13,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 14,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 15,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 16,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 17,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},

		{ 18,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT}, SceneryType.DESERT , WeatherType.NONE )},

		{ 19,new MapProperties( new SoldierType[] {SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT,SoldierType.KNIGHT} , SceneryType.DESERT , WeatherType.NONE )},
	};
}
