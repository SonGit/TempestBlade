using UnityEngine;
using System.Collections;

public class AnimationHelper : MonoBehaviour {

	public Soldier soldierScript;

	void Awake()
	{
		soldierScript = this.GetComponentInParent<Soldier> ();
	}

	public void OnEndAttackAnimation()
	{
		soldierScript.OnEndAttack ();
	}
}
