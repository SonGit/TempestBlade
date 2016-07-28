using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StackableSkillSlot : MonoBehaviour {
	
	public Image cooldown;

	public float _duration = 30.0f;

	public StackableSkill stackSkill;

	public Text _text;

	private bool coolingDown;

	private bool _coolingDown
	{
		get {
			return coolingDown;
		}
		set
		{
			coolingDown = value;
			cooldown.enabled = value;
			if (value) {
				cooldown.fillAmount = 1;
			} 
		}
	}

	public int currentStacks = 0;

	public int _currentStacks
	{
		get{ return currentStacks;}
		set
		{
			currentStacks = value;

			if(stackSkill != null)
			stackSkill.numStack = value;

			if (value != 0) 
				_text.enabled = true;
			 else
				_text.enabled = false;

			_text.text = currentStacks.ToString ();
		}
	}

	// Use this for initialization
	void Start () {
		cooldown.fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_coolingDown == true)
		{
			//Reduce fill amount over 30 seconds
			cooldown.fillAmount -= 1.0f/_duration * Time.deltaTime;

			if (cooldown.fillAmount <= 0) {
				Reset ();
				transform.GetComponent<Image> ().enabled = false;
			}
		}
	}

	public void Reset()
	{
		stackSkill = null;
		_coolingDown = false;
		_currentStacks = 0;
	}

	void StartCooldown()
	{
		_coolingDown = true;
		_duration = 15;
	}

	void MakeNewStack(StackableSkill skill)
	{
		stackSkill = skill;
		_currentStacks = 1;
		StartCooldown ();
	}

	public void IncrementStack(StackableSkill skill)
	{
		if (stackSkill == null) {
			MakeNewStack (skill);
			return;
		}
		
		if (stackSkill.Equals (skill)) 
		{
			_currentStacks++;
			StartCooldown ();
		}
		else 
		{
			MakeNewStack(skill);
		}
	}

}
