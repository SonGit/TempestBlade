using UnityEngine;
using System.Collections;

public class Squad : MonoBehaviour {

	Vector3[] _squadPos = new Vector3[]{
		new Vector3 (0 , -12 , 10),
		new Vector3 (0 , -12 , 5),
		new Vector3 (0 , -12 , 0),
		new Vector3 (0 , -12 , -5),
		new Vector3 (0 , -12 , -10),
		new Vector3 (5 , -12 , 10),
		new Vector3 (5 , -12 , 5),
		new Vector3 (5 , -12 , 0),
		new Vector3 (5 , -12 , -5),
		new Vector3 (5 , -12 , -10),
	};

	public Soldier[] _soldiers;

	// Use this for initialization
	void Start () {
		_soldiers = this.GetComponentsInChildren<Soldier> ();
	}

	public void Advance()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
