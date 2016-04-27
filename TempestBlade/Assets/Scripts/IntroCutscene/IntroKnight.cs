using UnityEngine;
using System.Collections;

public class IntroKnight : MonoBehaviour {

	Animator animator;

	public int type;

	// Use this for initialization
	void Awake () {
		animator = this.GetComponent<Animator> ();
		animator.SetInteger ("Type",type);
	}

}
