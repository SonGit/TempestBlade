using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Music
{
	Realm_Of_Fantasy,
	Dragon_Mystery,
	Clock_Maker
}

public class MusicPlayer : MonoBehaviour {

	private static MusicPlayer _instance;

	public static MusicPlayer instance
	{
		get {
			if (_instance == null) {
				GameObject go = (GameObject) Instantiate (Resources.Load ("Prefabs/MusicPlayer"));
				_instance = go.GetComponent<MusicPlayer> ();
			}
			return _instance;
		}

		set 
		{
			_instance = value;
		}
	}

	public AudioSource MusicSource;

	public Dictionary<Music,AudioClip> MusicCache = new Dictionary<Music,AudioClip>();

	void Awake()
	{
		instance = this;
		Load ();
	}

	void Load()
	{
		Dictionary<Music,string> musicPaths = Cache.instance.musicPaths;

		foreach(KeyValuePair<Music,string> pair in musicPaths)
		{
			MusicCache.Add (pair.Key , (AudioClip)Resources.Load( pair.Value ));
		}

	}

	void Start()
	{

	}

	public void Play(Music music)
	{
		StartCoroutine (Play_Async(music));	
	}

	public void Stop()
	{
		StartCoroutine (SlowFade());
	}

	IEnumerator Play_Async(Music music)
	{
		AudioClip clip = MusicCache[music];

		//If another audio is playing, fade it and replace audio
		if (MusicSource.isPlaying) {
			StartCoroutine (Fade());
			yield return new WaitForSeconds (2);
			MusicSource.clip = clip;
			MusicSource.Play ();
		} else {
			MusicSource.clip = clip;
			MusicSource.Play ();
		}
		yield return null;
	}

	IEnumerator Fade()
	{
		while (MusicSource.volume > 0) {
			MusicSource.volume -= 0.05f;
			yield return new WaitForSeconds (0.1f);
		}
		MusicSource.volume = 1;
	}

	IEnumerator SlowFade()
	{
		while (MusicSource.volume > 0) {
			MusicSource.volume -= 0.025f;
			yield return new WaitForSeconds (0.1f);
		}
		MusicSource.Stop ();
		MusicSource.volume = 1;
	}
}

