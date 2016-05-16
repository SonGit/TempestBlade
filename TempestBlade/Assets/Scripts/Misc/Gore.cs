using UnityEngine;
using System.Collections;

public class Gore : MonoBehaviour {

	Vector3 startPosition  ;  //The starting position in world space
	Vector3 endPosition ;   //The ending position in world space
	Vector3 bending  = Vector3.up;                //Bend factor (on all axes)
	float timeToTravel  = 1f;                //The total time it takes to move from start- to end position

	void Start () {
		startPosition = transform.position;
		endPosition = startPosition + Vector3.left * 5;
		StartCoroutine (MoveToPosition());
	}

	IEnumerator MoveToPosition () {
		float timeStamp  = Time.time;
		while (Time.time<timeStamp+timeToTravel) {
			Vector3 currentPos  = Vector3.Lerp(startPosition, endPosition, (Time.time - timeStamp)/timeToTravel);

			currentPos.x += bending.x*Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp)/timeToTravel) * Mathf.PI);
			currentPos.y += bending.y*Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp)/timeToTravel) * Mathf.PI);
			currentPos.z += bending.z*Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp)/timeToTravel) * Mathf.PI);

			transform.position = currentPos;

			yield return new WaitForEndOfFrame();
		}
	}

	void Kick()
	{
		//foreach( Rigidbody gore in gores)
		//{
			//Vector3 randDir = new Vector3( (float)Random.Range(-2,2),(float)Random.Range(0,2),0);
			//float randForce = (float)Random.Range (450,600);

			//gore.AddForce( randDir * randForce );
		//}
	}

}
