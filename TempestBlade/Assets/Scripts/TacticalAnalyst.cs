using UnityEngine;
using System.Collections;

public enum CameraMove
{
	UP,
	DOWN,
	LEFT,
	RIGHT
}

public class TacticalAnalyst : MonoBehaviour {
	
	public SquadLeader _allyLeader;
	public SquadLeader _enemyLeader;
	public float _speed;
	public GameObject _chatBox;
	public GameObject _AfterIntroUI;
	public static TacticalAnalyst instance;
	public HMBarManager _barAlly;



	Vector3 enemyGeneralPos = new Vector3(-140,0,-18);
	Vector3 allyGeneralPos = new Vector3(66.6f,0,-18);

	Transform t;

	bool _isAllowed;

	void Awake()
	{
		instance = this;
		_isAllowed = true;
	}

	// Use this for initialization
	void Start () {
		t = transform;
		//Intro ();
	}

	bool _cameraGoLeft;
	bool _cameraGoRight;
	bool _cameraGoUp;
	bool _cameraGoDown;

	void Update()
	{
		if (_cameraGoLeft && _isAllowed) {
			Vector3 destination = t.position;
			destination.x -= 5f;

			if (destination.x < -130)
				return;

			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed * 2);
		}
		if (_cameraGoRight && _isAllowed) {
			Vector3 destination = t.position;
			destination.x += 5f;

			if (destination.x > 60)
				return;

			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed * 2);
		}
		if (_cameraGoUp && _isAllowed) {
			Vector3 destination = t.position;
			destination.z += 5f;

			if (destination.z > 5)
				return;

			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed * 2);
		}
		if (_cameraGoDown && _isAllowed) {
			Vector3 destination = t.position;
			destination.z -= 5f;

			if (destination.z < -30)
				return;

			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed * 2);
		}

	}
		
	
	public void StartBattle()
	{
		_allyLeader.Advance ();
		_enemyLeader.Advance ();
		_isAllowed = true;

		_AfterIntroUI.SetActive (true);
		_barAlly.Init ();
	}

	public void Intro()
	{
		_isAllowed = false;
		_AfterIntroUI.SetActive (false);
		StartCoroutine (Camera_ZoomInGeneral());
	}

	IEnumerator Camera_ZoomInGeneral()
	{
		Vector3 destination = Vector3.zero;
		destination.x = -140;
		destination.y = t.position.y;
		destination.z = t.position.z;

		while ( Vector3.Distance(t.position,destination) > 0.5f )
		{
			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed);
			yield return new WaitForEndOfFrame ();
		}

		//Zoom in
		destination.z += 5;

		while ( Vector3.Distance(t.position,destination) > 0.5f )
		{
			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed);
			yield return new WaitForEndOfFrame ();
		}

		_chatBox.SetActive (true);
		yield return new WaitForSeconds (2);
		_chatBox.SetActive (false);

		destination.x = 66.6f;
		destination.y = t.position.y;
		destination.z = t.position.z;

		while ( Vector3.Distance(t.position,destination) > 0.5f )
		{
			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed * 2);
			yield return new WaitForEndOfFrame ();
		}

		_chatBox.SetActive (true);
		yield return new WaitForSeconds (2);
		_chatBox.SetActive (false);

		destination.x = -37;
		destination.y = 10.6f;
		destination.z = -18f;

		while ( Vector3.Distance(t.position,destination) > 0.5f )
		{
			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed * 2);
			yield return new WaitForEndOfFrame ();
		}
			
		StartBattle ();
	}

	public void CameraToDefaultPos()
	{
		_isAllowed = false;
		StopAllCoroutines ();
		StartCoroutine (MoveToDefaultPos());
	}

	IEnumerator MoveToDefaultPos()
	{
		Vector3 destination = new Vector3(-37,10.5f,-18);

		while ( Vector3.Distance(t.position,destination) > 0.5f )
		{
			t.position = Vector3.MoveTowards(t.position, destination, Time.deltaTime * _speed * 5);
			yield return new WaitForEndOfFrame ();
		}
		_isAllowed = true;
	}

	public void MoveCamera_FireballSkill()
	{
		StartCoroutine (MoveCamera_FireballSkill_Async());
	}

	IEnumerator MoveCamera_FireballSkill_Async()
	{
		_isAllowed = false;
		float goalX = 315;

		do {
			t.eulerAngles = new Vector3 (t.eulerAngles.x - 2, 0, 0);
			yield return new WaitForEndOfFrame ();
		} while
			(t.eulerAngles.x > goalX);
		yield return new WaitForSeconds (1);
		do {
			t.eulerAngles = new Vector3 (t.eulerAngles.x + 1, 0, 0);
			yield return new WaitForEndOfFrame ();
		} while
			(t.eulerAngles.x < 355);
		
		_isAllowed = true;
	}

	public void MoveCamera_Left_Pressed()
	{
		_cameraGoLeft = true;
	}

	public void MoveCamera_Left_Up()
	{
		_cameraGoLeft = false;
	}

	public void MoveCamera_Right_Pressed()
	{
		_cameraGoRight = true;
	}

	public void MoveCamera_Right_Up()
	{
		_cameraGoRight = false;
	}

	public void MoveCamera_Up_Pressed()
	{
		_cameraGoUp = true;
	}

	public void MoveCamera_Up_Up()
	{
		_cameraGoUp = false;
	}

	public void MoveCamera_Down_Pressed()
	{
		_cameraGoDown = true;
	}

	public void MoveCamera_Down_Up()
	{
		_cameraGoDown = false;
	}

	public void ResetLevel()
	{
		Application.LoadLevel ("TestScene");
	}

	public int GetPlayerMorale()
	{
		return _allyLeader._morale;
	}

	public int GetEnemyMorale()
	{
		return _enemyLeader._morale;
	}

	public int GetPlayerHP()
	{
		return _allyLeader.HP;
	}

	public int GetEnemyHP()
	{
		return _enemyLeader.HP;
	}

}
