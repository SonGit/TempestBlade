using UnityEngine;
using System.Collections;

public abstract class SkyfallObject : MonoBehaviour {
	
	public float Speed;

	public bool _isFalling;

	protected Vector3 _origin;

	protected Transform t;

	protected Animator animator;

	protected Vector3 dest;

	SquadLeader target;

	Vector3 offset;

	// Use this for initialization
	void Awake () {
		t = transform;
		_origin = t.position;
		animator = this.GetComponent<Animator> ();
	}

	public void Fall(SquadLeader target)
	{
		this.target = target;
		t.position = GetPos (target);
		_isFalling = true;

		if (target._allegiance == Allegiance.ALLY)
			offset = new Vector3 (7, -15, 0);
		else
			offset = new Vector3 (-7, -15, 0);
	}

	public void Fall()
	{
		t.position = GetPos (target);
		_isFalling = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_isFalling)
			return;

		dest = t.position + offset;
		t.position = Vector3.MoveTowards (t.position,dest,Speed * Time.deltaTime);

		if (t.position.y < -5f) {
			_isFalling = false;
			OnTouchGround ();
		}
	}

	protected Vector3 GetPos(SquadLeader target)
	{
		float desiredHeight = 100;
		float radius = 60;
		Vector3 someRandomPoint;
		Vector3 offset2;

		if(target._allegiance == Allegiance.ALLY)
			offset2 = new Vector3 (-10,0,0);
		else
			offset2 = new Vector3 (10,0,0);
		
		Vector3 groundZero = target.transform.position + offset2;
		someRandomPoint = groundZero + Random.insideUnitSphere * radius;
		someRandomPoint.y = Random.Range(desiredHeight - 20, desiredHeight + 20);

		return someRandomPoint;
	}

	protected virtual void OnTouchGround()
	{

	}

}
