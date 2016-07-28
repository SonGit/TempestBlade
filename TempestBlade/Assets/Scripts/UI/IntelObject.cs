using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntelObject : MonoBehaviour {

	public Text _text;

	public SoldierType _soldierType;

	// Use this for initialization
	void Start () {
		_soldierType = SoldierType.BERSERKER;
		_text.text = Cache.instance.UnitDescription [_soldierType];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
