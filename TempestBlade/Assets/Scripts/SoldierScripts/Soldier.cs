using UnityEngine;
using System.Collections;
using System.Linq;
using Shared;

public enum SoldierStatus
{
	IDLE,
	STAND_BY,
	WALKING,
	ATTACKING,
	DEAD
};

public class Soldier : MonoBehaviour {
	
	public Soldier _enemySoldier;

	public Animator _animator;

	public float _speed;

	public SoldierStatus _status;

	public int _health;

	public SquadLeader _leader;

	public bool _isFree;

	Transform t;

	public SpriteRenderer _spriteRenderer;

	public float _lane;

	public bool _hasArrivedAtFightLocation;

	bool _walking;

	public GameObject _defenseBuffParticleEffect;

	public ParticleSystem _attackBuffParticleEffect;

	void Awake()
	{
		t = transform;
		_isFree = true;
		_hasArrivedAtFightLocation = false;
	}

	void Start()
	{
		_spriteRenderer = this.GetComponentInChildren<SpriteRenderer> ();
		_defenseBuffParticleEffect.SetActive (false);
		Init ();
	}

	public virtual void Init()
	{
		
	}

	public void Attack(Soldier enemy,float lane = 0)
	{
		_isFree = false;
		_enemySoldier = enemy;
		_lane = lane;
		StartCoroutine (Attack_async(enemy));
	}

	public void GotHit(int damage,Callback callback)
	{
		if (_status == SoldierStatus.DEAD)
			return;
		
		_health -= damage;

		if (_health <= 0) {
			ChangeStatus (SoldierStatus.DEAD);
			if (callback != null)
				callback ();
			_leader.UnitLost (_lane);
			gameObject.SetActive (false);
		} else {
			//StartCoroutine (Flashing());
		}
	}

	Vector3 _originPoint;//Sometimes the soldier walks too far awat due to random generated vectors.THis aims to fix that
	public void StandBy(bool calledAgain = false)
	{
		if(!calledAgain)
			_originPoint = t.position;
		if (_status == SoldierStatus.STAND_BY) //pREVENT MULTIPLE CALLINGS
			return;
		StopAllCoroutines ();
		StartCoroutine (StandBy_Async(OnArrived_StandBy));
	}

	void Update()
	{
		if (_enemySoldier != null) {
			if (_enemySoldier._status == SoldierStatus.DEAD) {
				StandBy ();
				_isFree = true;
				_enemySoldier = null;
			}
		}

		if (_enemySoldier == null && _status == SoldierStatus.ATTACKING) {
			StandBy ();
		}
	}

	IEnumerator Attack_async(Soldier enemy)
	{
		//Approach the enemy
		Pursue (enemy.gameObject,OnArrived_EnemyPosition);
		yield return null;
	}

	void ChangeStatus(SoldierStatus status)
	{
		_status = status;
		if (_status == SoldierStatus.WALKING) {
			_animator.SetBool ("Attacking",false);
			_animator.SetBool ("Walking",true);
		}
		if (_status == SoldierStatus.ATTACKING) {
			//_animator.SetBool ("Attacking",true);
			_animator.SetBool ("Walking",false);
		}
		if (_status == SoldierStatus.DEAD) {
			_animator.SetBool ("Attacking",false);
			_animator.SetBool ("Walking",false);
		}
		if (_status == SoldierStatus.STAND_BY) {
			_animator.SetBool ("Attacking",false);
			_animator.SetBool ("Walking",true);
		}
		if (_status == SoldierStatus.IDLE) {
			_animator.SetBool ("Attacking",false);
			_animator.SetBool ("Walking",false);
		}

		if(_spriteRenderer != null)
		_spriteRenderer.material.SetFloat("_FlashAmount",0f); 
	}

	public void MoveUp()
	{
		if (_leader._allegiance == Allegiance.ALLY)
			_originPoint = t.position + new Vector3 (-5, 0, 0);
		else
			_originPoint = t.position + new Vector3 (5, 0, 0);
	}

	public void Advance()
	{
		Vector3 destination;
		ChangeStatus (SoldierStatus.WALKING);

		if (_leader._allegiance == Allegiance.ALLY)
			destination = t.position + new Vector3 (-100, 0, 0);
		else
			destination = t.position + new Vector3 (100, 0, 0);
		
		destination.y = t.position.y;//Make sure soldier does not go up to the sky

		StartCoroutine (GoTo_Async(destination,OnArrived_MoveUp));
	}

	IEnumerator GoTo_Async(Vector3 destination,Callback callback = null)
	{
		while (true)
		{
			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed);

			// If the object has arrived, stop the coroutine
			if (Vector3.Distance(t.position,destination) < 0.5f)
			{
				if (callback != null)
					callback ();
				yield break;
			}

			//Update anim rotation
			RotateAnimation(destination);
			// Otherwise, continue next frame
			yield return null;
		}
	}

	void Pursue(GameObject pursued,Callback callback = null)
	{
		ChangeStatus (SoldierStatus.WALKING);
		StopAllCoroutines ();
		StartCoroutine (Pursue_Async(pursued,callback));
	}

	IEnumerator Pursue_Async(GameObject pursued,Callback callback = null)
	{
		Vector3 pursued_pos;
		_hasArrivedAtFightLocation = false;
		while (true)
		{
			pursued_pos = pursued.transform.position;

			//Make sure that soldiers always face each other on a same z plane
			if (pursued_pos.x < t.position.x) {
				pursued_pos += (Vector3.right * 5);
			} else {
				pursued_pos += (Vector3.left * 5);
			}
			pursued_pos.z = _lane;
			_enemySoldier._lane = _lane;

			t.position = Vector3.MoveTowards(t.position, pursued_pos, Time.deltaTime * _speed);
			Debug.DrawLine( t.position, pursued_pos, Color.green);
			// If the object has arrived, stop the coroutine
			if (Vector3.Distance(t.position,pursued_pos) < 0.5f)
			{
				_hasArrivedAtFightLocation = true;
			}

			//THe fight only take place when 2 soldiers have arrived at the fighting point
			if (_hasArrivedAtFightLocation && _enemySoldier._hasArrivedAtFightLocation) {
				if (callback != null)
					callback ();
				yield break;
			}

			//Update anim rotation
			RotateAnimation(pursued_pos);
			// Otherwise, continue next frame
			yield return null;
		}
	}

	IEnumerator StandBy_Async(Callback callback)
	{
		ChangeStatus (SoldierStatus.IDLE);
		yield return new WaitForSeconds (Random.Range(1,3));
		ChangeStatus (SoldierStatus.STAND_BY);

		Vector3 someRandomPoint = _originPoint + Random.insideUnitSphere * 5f;
		someRandomPoint.y = t.position.y;

		while (_status == SoldierStatus.STAND_BY)
		{
			t.position = Vector3.MoveTowards(t.position, someRandomPoint , Time.deltaTime * _speed);

			// If the object has arrived, stop the coroutine
			if (Vector3.Distance(t.position,someRandomPoint ) < 0.5f)
			{
				_status = SoldierStatus.IDLE;
				if (callback != null)
					callback ();
				yield break;
			}

			//Update anim rotation
			RotateAnimation(someRandomPoint );
			// Otherwise, continue next frame
			yield return new WaitForEndOfFrame();
		}
	}

	void OnArrived_MoveUp()
	{
		ChangeStatus (SoldierStatus.IDLE);
		StandBy ();
	}

	void OnArrived_EnemyPosition()
	{
		StartCoroutine (AttackAnimation());
	}

	void OnArrived_StandBy()
	{
		StandBy (true);
	}

	public void OnEndAttack()
	{
		if(_enemySoldier != null && _status == SoldierStatus.ATTACKING)
		_enemySoldier.GotHit (25,OnKilledEnemy);
	}

	void RotateAnimation(Vector3 vect)
	{
		Vector3 normalized = (vect - t.position).normalized;
		_animator.SetFloat ("PosX",normalized.x);
		_animator.SetFloat ("PosY",normalized.z);
	}

	void OnKilledEnemy()
	{
		_isFree = true;
		_enemySoldier = null;
		StopAllCoroutines ();
		StandBy ();
	}

	IEnumerator AttackAnimation()
	{		
		ChangeStatus (SoldierStatus.ATTACKING);
		while (_status == SoldierStatus.ATTACKING && _enemySoldier != null ) {
			
			_animator.SetBool ("Attacking",true);
			RotateAnimation (_enemySoldier.transform.position);
			yield return new WaitForSeconds (0.9f);
			OnEndAttack ();
			_animator.SetBool ("Attacking",false);
		}
		yield return null;
	}

	public void DefenseBuffed_On(float defenseValue)
	{
		_defenseBuffParticleEffect.SetActive (true);
	}

	public void DefenseBuffed_Off()
	{
		_defenseBuffParticleEffect.SetActive (false);
	}

	public void AttackBuffed_On(float defenseValue)
	{
		_attackBuffParticleEffect.Play ();
		_spriteRenderer.color = Color.red;
	}

	public void AttackBuffed_Off()
	{
		_spriteRenderer.color = Color.white;
	}

}
