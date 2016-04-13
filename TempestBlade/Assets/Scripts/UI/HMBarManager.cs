using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HMBarManager : MonoBehaviour {

	public Image HP_bar;

	public Image Morale_bar;

	int _startHP;

	int _startMorale = Cache.MORALE_MAX;

	bool start;

	public Allegiance _allegiance;

	// Use this for initialization
	void Start () {
		//HP_bar.fillAmount = 
	}

	public void Init()
	{
		if (_allegiance == Allegiance.ALLY) {
			_startHP = TacticalAnalyst.instance.GetPlayerHP();
		} else {
			_startHP =  TacticalAnalyst.instance.GetEnemyHP ();
		}

		_startMorale = 100;//Morale have max amount of 100

		start = true;
	}

	int currentHP;
	int currentMorale;

	// Update is called once per frame
	void Update () {
		if (!start)
			return;

		if (_allegiance == Allegiance.ALLY) {
			currentHP = TacticalAnalyst.instance.GetPlayerHP();
			currentMorale = TacticalAnalyst.instance.GetPlayerMorale ();
		} else {
			currentHP = TacticalAnalyst.instance.GetEnemyHP ();
			currentMorale = TacticalAnalyst.instance.GetEnemyMorale ();
		}
			
		HP_bar.fillAmount = (((currentHP * 100) / _startHP) / 100f);
		Morale_bar.fillAmount = (((currentMorale * 100) / _startMorale) / 100f);
	}
}
