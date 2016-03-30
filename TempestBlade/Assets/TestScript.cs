using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {
	public GameObject go;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			var someRandomPoint = Random.insideUnitSphere * 4 + transform.position;
			someRandomPoint.y = transform.position.y;
			Instantiate (go,someRandomPoint,go.transform.rotation);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
