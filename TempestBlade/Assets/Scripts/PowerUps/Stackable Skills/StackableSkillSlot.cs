using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StackableSkillSlot : MonoBehaviour {
	
	public Image cooldown;

	public float _duration = 30.0f;

	public StackableSkill _stackSkill;

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

	void Reset()
	{
		_stackSkill = null;
		_coolingDown = false;
		_currentStacks = 0;
	}

	void ResetCooldown()
	{
		_coolingDown = true;
		_duration = 15;
	}
		
	public bool CanAddStack(StackableSkill stack)
	{
		if (_stackSkill == null) {
			MakeNewStack(stack);
			return true;
		}
			
		if (IsSameStack (stack)) {
			IncrementStack ();
			return true;
		} else
			return false;
	}

	public bool CanConsume(StackableSkill stack)
	{
		if (_stackSkill == null)
			return false;

		if (_stackSkill._type != stack._type)
		{
			return false;
		}
		else 
		{
			return true;
		}
	}

	public void ConsumeStack()
	{
		Reset ();
		transform.GetComponent<Image> ().enabled = false;
	}

	void MakeNewStack(StackableSkill stack)
	{
		Reset ();

		transform.GetComponent<Image> ().enabled = true;
		transform.GetComponent<Image>().sprite =  Resources.Load<Sprite>( Cache.instance.stackableSkillIconPaths[ stack._type ]);

		_stackSkill = stack;
		_duration = stack._duration;
		_coolingDown = true;
		++_currentStacks;
	}

	void IncrementStack()
	{
		if (_currentStacks >= Cache.STACK_MAX) {
			return;
		}

		ResetCooldown ();

		++_currentStacks;
	}

	bool IsSameStack(StackableSkill stack)
	{
		if (_stackSkill._type == stack._type)
			return true;
		else
			return false;
	}
}
