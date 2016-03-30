using UnityEngine;
using System.Collections;

public class Fireball : SkyfallObject {

	public int times_beforeDestroy = 1;
	int curr = 0;
	float desiredHeight = 100;
	float radius = 60;
	Vector3 someRandomPoint;
	 
	protected override void OnTouchGround()
	{
		StartCoroutine (Explode());
	}

	IEnumerator Explode()
	{
		animator.SetBool ("Boom",true);
		yield return new WaitForSeconds (1);
		animator.SetBool ("Boom",false);

		curr++;

		if (curr >= times_beforeDestroy) {
			Destroy (this.gameObject);
		} else {
			Fall ();
		}
	}
}
