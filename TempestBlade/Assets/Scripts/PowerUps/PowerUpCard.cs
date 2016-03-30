using UnityEngine;
using System.Collections;

public abstract class PowerUpCard : MonoBehaviour {

	public CardType _type;
	public GameObject _prefab;

	public abstract void Init();
	public abstract void Execute(SquadLeader target);

}
