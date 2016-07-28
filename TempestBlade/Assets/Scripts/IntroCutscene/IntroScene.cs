using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour {

	public float _camSpeed;

	public float _textSpeed;

	public Text _textScene1;

	public Text _textScene2;

	public Text _textScene3;

	public Image _fade;

	public Image _logo;

	public GameObject Scene2_BG;

	public Text[] _credits;

	public IntroUI _introUI;

	Transform t;

	bool _started = false;

	void Awake()
	{
		t = transform;
		Application.targetFrameRate = 60;
		MusicPlayer.instance.Init ();
	}

	public void StartGame()
	{
		if (_started)
			return;
		
		_started = true;
		_introUI.EffectOn ();
		StartCoroutine (Start2());
	}

	// Use this for initialization
	IEnumerator Start2 () {
		StartCoroutine (Scene1());
		yield return new WaitForSeconds (17);
		StartCoroutine (Scene2());
		yield return new WaitForSeconds (15);
		StartCoroutine (FadeOut());
		yield return new WaitForSeconds (1);
		StartCoroutine (Scene3());
		yield return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Scene1()
	{
		MusicPlayer.instance.Play (Music.Clock_Maker);

		//StartCoroutine (FadeIn());
		StartCoroutine (RollText());

		Vector3 destination = Vector3.zero;
		destination.x = 170;
		destination.y = t.position.y;
		destination.z = t.position.z;

		while (Vector3.Distance (t.position, destination) > 0.5f) {
			t.position = Vector3.MoveTowards (t.position, destination, Time.deltaTime * _camSpeed);
			yield return new WaitForEndOfFrame ();
		}

		StartCoroutine (FadeOut());
		_textScene1.enabled = false;
	}

	IEnumerator Scene2()
	{
		transform.position = new Vector3 (350,transform.position.y,transform.position.z);
		Scene2_BG.SetActive (true);
		StartCoroutine (FadeIn());
		yield return new WaitForSeconds (1);
		StartCoroutine (FadeInOutText(_textScene2,12f));
		yield return new WaitForSeconds (3);
		yield return null;
	}

	IEnumerator Scene3()
	{
		StartCoroutine (FadeIn());
		transform.position = new Vector3 (3267,transform.position.y,transform.position.z);
		StartCoroutine (FadeInOutText(_textScene3,10f));

		yield return new WaitForSeconds (3f);
		MusicPlayer.instance.Stop ();
		yield return new WaitForSeconds (3f);
		//MusicPlayer.instance.Stop ();
		yield return new WaitForSeconds (3);
		MusicPlayer.instance.Play (Music.Dragon_Mystery);
		yield return new WaitForSeconds (3);

		Color color = _logo.color;
		float alpha =  0;

		while (alpha < 1) {

			alpha += 0.1f;
			_logo.color = new Color ( color.r , color.g , color.b , alpha);
			yield return new WaitForSeconds (0.1f);
		}

		yield return new WaitForSeconds (7);

		while (alpha > 0) {

			alpha -= 0.1f;
			_logo.color = new Color ( color.r , color.g , color.b , alpha);
			yield return new WaitForSeconds (0.1f);
		}

		yield return new WaitForSeconds (1);

		StartCoroutine (ShowCredit(_credits));
	}

	IEnumerator RollText()
	{
		Vector3 destination = Vector3.zero;
		destination.x = _textScene1.transform.localPosition.x;
		destination.y = 120;
		destination.z = _textScene1.transform.localPosition.z;

		while (Vector3.Distance (t.position, destination) > 0.5f) {
			_textScene1.transform.localPosition = Vector3.MoveTowards (_textScene1.transform.localPosition, destination, Time.deltaTime * _textSpeed);
			yield return new WaitForEndOfFrame ();
		}
	}
		
	IEnumerator FadeIn()
	{
		Color color = _fade.color;
		float alpha =  1;

		color.a = alpha;

		while (alpha > 0) {
			
			alpha -= 0.1f;
			_fade.color = new Color ( color.r , color.g , color.b , alpha);
			yield return new WaitForSeconds (0.1f);
		}
	}

	IEnumerator FadeOut()
	{
		Color color = _fade.color;
		float alpha =  0;

		while (alpha < 1) {

			alpha += 0.1f;
			_fade.color = new Color ( color.r , color.g , color.b , alpha);
			yield return new WaitForSeconds (0.1f);
		}
	}

	IEnumerator FadeInOutText(Text text,float Length)
	{
		Color color = text.color;
		float alpha =  0;

		color.a = alpha;

		while (alpha < 1) {
			alpha += 0.1f;
			text.color = new Color ( color.r , color.g , color.b , alpha);
			yield return new WaitForSeconds (0.1f);
		}

		yield return new WaitForSeconds (Length);

		while (alpha > 0) {

			alpha -= 0.1f;
			text.color = new Color ( color.r , color.g , color.b , alpha);
			yield return new WaitForSeconds (0.1f);
		}

	}

	IEnumerator ShowCredit(Text[] credits)
	{
		print ("RED");
		for (int i = 0; i < credits.Length; i++) {
			StartCoroutine (FadeInOutText(credits[i],3f));
			yield return new WaitForSeconds (6);
		}
		yield return null;
	}

}
