using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Shared;
using System.Linq;

namespace Shared
{
	public delegate void Callback();
	public delegate void Callback_WithStackSkill(StackableSkill skill);
}

public enum Allegiance
{
	ALLY,
	ENEMY
};

public enum SoldierType
{
	NULL,
	FOOTMAN,
	FOOTMAN_SKELETON,
	KNIGHT_TIER_1
};

public class SquadLeader : MonoBehaviour {

	public Soldier[] _soldiers;

	public SquadLeader _enemySquad;

	public GameObject group;

	const int MAX_ENGAGEMENT = 10;

	public int _currentEngagement = 0;

	public Allegiance _allegiance;

	public int _unitLost = 0;

	public int _morale;

	public int _moraleRestoreRate;

	public int HP = 0;

	float _moraleCheckTick;

	private AttributeBuff activeBuff;

	public AttributeBuff _activeBuff
	{
		get{ return activeBuff;}
		set 
		{
			activeBuff = value;
		}
	}

	private AttributeBuff activeDebuff;

	public AttributeBuff _activeDebuff
	{
		get{ return activeDebuff;}
		set 
		{
			activeDebuff = value;
		}
	}

	private float _buffCountdown;
	private float _debuffCountdown;

	List<SquadLeader> _enemySquadInSight = new List<SquadLeader> ();

	Dictionary<float,bool> _lanes = new Dictionary<float, bool>()
	{
		{5, false},
		{10,false},
		{15,false},
		{20,false},
		{25,false},
		{35,false},
		{40,false},
		{45,false},
		{50,false},
		{55,false},
	};

	Transform t;

	void Awake()
	{
		_soldiers = new Soldier[100];

		SoldierType[] test = new SoldierType[] {
			SoldierType.FOOTMAN,
			SoldierType.FOOTMAN,
			SoldierType.FOOTMAN,
			SoldierType.FOOTMAN,
			SoldierType.FOOTMAN,
		};

		if (_allegiance == Allegiance.ALLY) {
			SpawnSoldiers (PlayerInfoCache.instance.GetSoldierCardDeck());
		} else {
			
			MapProperties mapProperties = PlayerInfoCache.instance._currentMap; 
			if (mapProperties != null) {
				print ("aliofuhalohfalhfwqiuwqrfqwqwqwqwqwewwe");
				SpawnSoldiers(mapProperties._soldiers);
			}
				
			else
				SpawnSoldiers(test);
		}
	}

	void Start()
	{
		t = transform;

		_morale = Cache.MORALE_MAX;

		_moraleRestoreRate = Cache.MORALE_RESTORE_RATE;

		_moraleCheckTick = Cache.MORALE_CHECK_RATE;
	}

	void Update()
	{
		//COuntdown buff
		if (_activeBuff != null) {

			_buffCountdown -= Time.deltaTime;

			if (_buffCountdown < 0) {
				OnBuffEnd ();
			}
		}

		//COuntdown debuff
		if (_activeDebuff != null) {

			_debuffCountdown -= Time.deltaTime;

			if (_debuffCountdown < 0) {
				OnDebuffEnd ();
			}
		}

		//tICK FOR MORALE Restore
		_moraleCheckTick -= Time.deltaTime;

		if (_moraleCheckTick <= 0) {
			if (_morale + _moraleRestoreRate >= Cache.MORALE_MAX) {
				_morale = Cache.MORALE_MAX;
			} else {
				_morale += _moraleRestoreRate;
				_moraleCheckTick = Cache.MORALE_CHECK_RATE;
			}
		}
	}

	public void Advance()
	{
		for (int i = 0; i < _soldiers.Length; i++) {
				_soldiers [i].Advance ();
		}
		StartCoroutine (MoveSquadLeader_Async());
	}



	public void SpawnSoldiers(SoldierType[] orders)
	{
		if (orders.Length != 5) {
			print ("Invalid orders");
			return;
		}

		float x;
		float offset = 5;
		int index = 0;
		int maxCollum = _soldiers.Length / 10;
		int maxRow = 10;
		int currentType = 0;

		if (_allegiance == Allegiance.ALLY)
			x = 0;
		else
			x = -120;

		for (int i = 0; i < maxCollum; i++) {

			SoldierType type = orders[currentType];

			for (int j = 0; j < maxRow; j++) {
				Vector3 position = new Vector3 ( (offset * i) + x, 0 ,offset * j);
				GameObject go = SoldierMaker(position,type);

				_soldiers [index] = go.GetComponent<Soldier> ();
				_soldiers [index]._leader = this;

				if (_allegiance == Allegiance.ALLY) {
					HP += _soldiers [index]._health;
					go.name = "Ally";
				} else {
					go.name = "Enemy";
					HP += _soldiers [index]._health;
				}
					
				index++;
			}

			//Every soldier type only have 20 units
			if (index % 20 == 0) {
				if(currentType != 4)
				currentType++;
			}
				
		}

		if (_allegiance == Allegiance.ALLY)
			_soldiers = _soldiers.OrderBy (item => item.transform.position.x).ToArray();
		else
			_soldiers = _soldiers.OrderByDescending (item => item.transform.position.x).ToArray();

	}

	GameObject SoldierMaker(Vector3 pos,SoldierType type)
	{
		string appendedString;

		if (_allegiance == Allegiance.ALLY)
			appendedString = string.Empty;
		else
			appendedString = "_Skeleton";
			
		switch(type)
		{
		case SoldierType.FOOTMAN:
			return (GameObject)Instantiate (Resources.Load("Prefabs/Soldiers/Footman" + appendedString),pos,transform.rotation);
		case SoldierType.KNIGHT_TIER_1:
			return (GameObject)Instantiate (Resources.Load("Prefabs/Soldiers/Knight_Tier1"+ appendedString),pos,transform.rotation);
		default:
			print (type);
			return (GameObject)Instantiate (Resources.Load("Prefabs/Soldiers/Footman"+ appendedString),pos,transform.rotation);
		}
	}

	public void Engage(SquadLeader enemySquad)
	{
		_enemySquad = enemySquad;
		Soldier[] enemySoldiers = GetAvailableSoldiers (enemySquad);
		Soldier[] ourSoldiers = GetAvailableSoldiers (this);

		print (gameObject.name + " A: " + ourSoldiers.Length + " B: " + enemySoldiers.Length);

		for (int i = 0; i < ourSoldiers.Length; i++) {
			if (i >= enemySoldiers.Length)
				break;
			ourSoldiers [i].Attack (enemySoldiers[i],GetLane());
			_currentEngagement++;
		}
		StandBy ();
	}

	public void StandBy()
	{
		for (int i = 0; i < _soldiers.Length; i++) {
			if (_soldiers [i]._isFree) {
				_soldiers [i].StandBy ();
			}
		}
	}
		
	Soldier[] GetAvailableSoldiers(SquadLeader squad)
	{
		int i = 0;
		List<Soldier> returnees = new List<Soldier> ();

		foreach (Soldier soldier in squad._soldiers) {
			if (i >= MAX_ENGAGEMENT)
				break;
			if (CheckSoldierAvailability (soldier)) {
				returnees.Add (soldier);
				i++;
			}
		}
		return returnees.ToArray();
	}

	Soldier GetAvailableSoldier(SquadLeader squad)
	{
		foreach (Soldier soldier in squad._soldiers) {
			if (soldier._isFree) {
				return soldier;
			}
		}
		return null;
	}

	bool CheckSoldierAvailability (Soldier soldier)
	{
		SoldierStatus status = soldier._status;

		if (status == SoldierStatus.IDLE || status == SoldierStatus.STAND_BY || status == SoldierStatus.WALKING)
			return true;
		
		return false;
	}
		
	IEnumerator MoveSquadLeader_Async(Callback callback = null)
	{
		
		Vector3 destination;

		if (_allegiance == Allegiance.ALLY)
			destination = t.position + new Vector3 (-100, 0, 0);
		else
			destination = t.position + new Vector3 (100, 0, 0);
		
		destination.y = t.position.y;//Make sure soldier does not go up to the sky

		while (true)
		{
			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * 5);

			// If the object has arrived, stop the coroutine
			if (Vector3.Distance(t.position,destination) < 0.5f)
			{
				if (callback != null)
					callback ();
				yield break;
			}

			// If 2 armies clashed, start fightting
			if (Vector3.Distance(t.position,_enemySquad.transform.position) < 25)
			{
				Engage (_enemySquad);
				yield break;
			}
				
			// Otherwise, continue next frame
			yield return null;
		}
	}

	//Lost a soldier.Now get another from soldier pool to fight 
	public void UnitLost(float atLane)
	{
		_currentEngagement--;
		ExitLane(atLane);

		Soldier enemySoldier = GetAvailableSoldier (_enemySquad);
		Soldier ourSoldier = GetAvailableSoldier (this);

		if (ourSoldier == null || enemySoldier == null)
			return;
			
		float lane = GetLane ();
		ourSoldier.Attack (enemySoldier,lane);
		enemySoldier.Attack (ourSoldier,lane);
		_unitLost++;

		if (_unitLost % MAX_ENGAGEMENT == 0)
			MoveUp ();

		//Morale config
		_morale -= Cache.MORALE_PENALTY_LOST_UNIT;
		_moraleCheckTick =+ Cache.MORALE_PENALTY_CHECK_RATE;

		CheckMorale ();
	}

	void CheckMorale()
	{
		if (_unitLost % 30 == 0) {
			if(_moraleRestoreRate != 0)
			_moraleRestoreRate -= 1;
		}
	}

	public void MoveUp()
	{
		for (int i = 0; i < _soldiers.Length; i++) {
			if (_soldiers [i]._isFree) {
				_soldiers [i].MoveUp ();
			}
		}
	}

	float GetAvailableLane()
	{
		foreach (KeyValuePair<float, bool> lane in _lanes)
		{
			if (lane.Value == false)
				return lane.Key;
		}
		return -1;
	}

	float GetLane()
	{
		
		float lane = GetAvailableLane ();
		if (_lanes.ContainsKey (lane)) {
			_lanes [lane] = true;
			return lane;
		}
		return 5;
	}

	void ExitLane(float laneKey)
	{
		_lanes [laneKey] = false;
	}
		
	public void ReceiveBuff(AttributeBuff buff)
	{
		
		if (buff.type == BuffType.BUFF) {
			OnBuffEnd ();
			_activeBuff = buff;
			_buffCountdown = buff.BuffLength;
		}

		if (buff.type == BuffType.DEBUFF) {
			OnDebuffEnd ();
			_activeDebuff = buff;
			_debuffCountdown = buff.BuffLength;
		}

		for (int i = 0; i < _soldiers.Length; i++) {
			if (_soldiers [i]._status != SoldierStatus.DEAD) {
				buff.ApplyTo (_soldiers [i]);
			}
		}
	}

	void OnBuffEnd()
	{
		if (_activeBuff == null)
			return;
		
		Reset(_activeBuff );
		_activeBuff = null;
	}

	void OnDebuffEnd()
	{
		if (_activeDebuff == null)
			return;
		
		Reset(_activeDebuff );
		_activeDebuff = null;
	}

	void Reset(AttributeBuff buff)
	{
		for (int i = 0; i < _soldiers.Length; i++) {
			if (_soldiers [i]._status != SoldierStatus.DEAD) {
				_soldiers [i].ResetMultipliers (buff);
			}
		}
	}
}


