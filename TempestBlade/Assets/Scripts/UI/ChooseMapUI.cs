using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChooseMapUI : MonoBehaviour {

	public GameObject _mapPrefab;

	public GridLayoutGroup _grid;

	// Use this for initialization
	void Start () {
		Populate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Populate()
	{
		for( int i = 0 ; i < 20; i++)
		{
			GameObject go = (GameObject)Instantiate (_mapPrefab);
			go.transform.SetParent (_grid.transform);
			go.transform.localScale = Vector3.one;

			go.GetComponent<MapUI> ()._mapProperties = Cache.instance.levelData[i];

			ButtonSetter(go);
		}

	}

	void ButtonSetter(GameObject go)
	{
		Button button = go.GetComponent<MapUI> ().button;

		if (button == null)
			return;
		
		button.onClick.AddListener(() =>
			{
				OnClick(go.GetComponent<MapUI>());
			});
	}

	void OnClick(MapUI map)
	{
		PlayerInfoCache.instance._currentMap = map._mapProperties;
		Application.LoadLevel ("TestScene");
	}
}
