using UnityEngine;
using System.Collections;

public class IntroUI : MonoBehaviour {
	
	_2dxFX_DestroyedFX[] fx;

	// Use this for initialization
	void Start () {
		fx = this.GetComponentsInChildren<_2dxFX_DestroyedFX> ();
	}
	
	public void EffectOn()
	{
		foreach (_2dxFX_DestroyedFX effect in fx) {
			StartCoroutine (PlayFX(effect));
		}
		Invoke ("Disable",2);
	}

	IEnumerator PlayFX(_2dxFX_DestroyedFX fx)
	{
		//yield return new WaitForSeconds (0.25f);

		while (fx.Destroyed < 1) {
			fx.Destroyed += 0.1f;
			yield return new WaitForSeconds (0.1f);
		}
	}

	void Disable()
	{
		this.gameObject.SetActive (false);
	}
}
