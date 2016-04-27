using UnityEngine;
using System.Collections;

public class Scenery : MonoBehaviour {

	public SpriteRenderer BG;
	public SpriteRenderer TR;

	// Use this for initialization
	void Start () {
	
		MapProperties mapProperties = PlayerInfoCache.instance._currentMap;

		if (mapProperties != null) {
			SceneryType type = mapProperties._scenery;
			SceneryData data = Cache.instance.backgroundData[type];
			BG.sprite = data.BackgroundPath;
			TR.sprite = data.TerrainPath;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
