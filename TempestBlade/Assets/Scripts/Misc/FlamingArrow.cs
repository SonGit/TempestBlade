using UnityEngine;
using System.Collections;

public class FlamingArrow : SkyfallObject {

	public int times_beforeDestroy = 4;
	int curr = 0;

	Vector3 someRandomPoint;

	void Start()
	{
	 	ally_target_offset = new Vector3 (120, -50, 0);
		enemy_target_offset= new Vector3 (-120, -50, 0);
	}

	protected override void OnTouchGround()
	{
		StartCoroutine (Explode());
	}

	IEnumerator Explode()
	{
		//animator.SetBool ("Boom",true);
		yield return new WaitForSeconds (0.25f);
		//animator.SetBool ("Boom",false);

		curr++;

		if (curr >= times_beforeDestroy) {
			Destroy (this.gameObject);
		} else {
			Fall ();
		}
	}
}
