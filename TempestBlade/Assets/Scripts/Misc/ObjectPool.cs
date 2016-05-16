using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	public static ObjectPool instance;

	public GameObject _attributeBuffPrefab;

	public GameObject _bloodSplashPrefab;

	List<GameObject> _buffs = new List<GameObject>();
	List<GameObject> _bloods = new List<GameObject>();
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
		int initialbuffs = 100;
		int initialbloods = 25;

		for (int i = 0; i < initialbuffs; i++) {
			GameObject go = (GameObject)Instantiate (_attributeBuffPrefab);
			_buffs.Add (go);
			go.transform.SetParent (transform);
			go.SetActive (false);
		}

	}
	
	public GameObject GetBuffObject()
	{
		for (int i = 0; i < _buffs.Count ; i++) {

			if (!_buffs [i].activeInHierarchy) {
				_buffs [i].SetActive (true);
				return _buffs [i];
			}
		}

		GameObject go = (GameObject)Instantiate (_attributeBuffPrefab);
		_buffs.Add (go);
		return go;
	}
}
