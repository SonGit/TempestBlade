using UnityEngine;
using System.Collections;

public class AttributeBuffUIEffect : MonoBehaviour {

	Animation anim;
	_2dxFX_DestroyedFX fx;

	// Use this for initialization
	void Awake () {
		anim = this.GetComponent<Animation> ();
		fx = this.GetComponent<_2dxFX_DestroyedFX> ();
	}
	
	public void Play()
	{
		anim.Play ();
		StartCoroutine (PlayFX());
	}

	IEnumerator PlayFX()
	{
		yield return new WaitForSeconds (0.5f);

		while (fx.Destroyed < 1) {
			fx.Destroyed += 0.1f;
			yield return new WaitForSeconds (0.1f);
		}

		OnEnd ();
	}

	void OnEnd()
	{
		fx.Destroyed = 0;
		gameObject.SetActive (false);
	}
}
