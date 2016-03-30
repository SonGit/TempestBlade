using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Shared;
using System.Linq;

namespace Shared
{
	public delegate void Callback();
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

		SoldierType[] orders = new SoldierType[] {
			SoldierType.FOOTMAN,
			SoldierType.KNIGHT_TIER_1,
			SoldierType.FOOTMAN,
			SoldierType.KNIGHT_TIER_1,
			SoldierType.FOOTMAN,
		};

		if (_allegiance == Allegiance.ALLY) {
			SpawnSoldiers (PlayerInfoCache.instance.GetSoldierCardDeck());
		} else {
			SpawnSoldiers(orders);
		}
	}

	void Start()
	{
		t = transform;
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
		int maxCollum = 10;
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

				if (_allegiance == Allegiance.ALLY)
					go.name = i +"";
				else
					go.name = "Enemy";

				_soldiers [index] = go.GetComponent<Soldier> ();
				_soldiers [index]._leader = this;
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
			print ("smt wrong");
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

	public void DefenseBuff(float length,float defenseValue)
	{
		for (int i = 0; i < _soldiers.Length; i++) {
			if (_soldiers [i]._status != SoldierStatus.DEAD) {
				_soldiers [i].DefenseBuffed_On (defenseValue);
			}
		}
		StartCoroutine (OffDefenseBuff(length));
	}

	IEnumerator OffDefenseBuff(float length)
	{
		yield return new WaitForSeconds (length);
		for (int i = 0; i < _soldiers.Length; i++) {
			if (_soldiers [i]._status != SoldierStatus.DEAD) {
				_soldiers [i].DefenseBuffed_Off ();
			}
		}
	}

	public void AttackBuff(float length,float attackValue)
	{
		for (int i = 0; i < _soldiers.Length; i++) {
			if (_soldiers [i]._status != SoldierStatus.DEAD) {
				_soldiers [i].AttackBuffed_On (attackValue);
			}
		}
		StartCoroutine (OffAttackBuff(length));
	}

	IEnumerator OffAttackBuff(float length)
	{
		yield return new WaitForSeconds (length);
		for (int i = 0; i < _soldiers.Length; i++) {
			if (_soldiers [i]._status != SoldierStatus.DEAD) {
				_soldiers [i].AttackBuffed_Off ();
			}
		}
	}
}


