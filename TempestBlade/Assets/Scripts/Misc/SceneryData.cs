using UnityEngine;
using System.Collections;

public class SceneryData {

	public Sprite BackgroundPath;
	public Sprite TerrainPath;

	public SceneryData(string bgPath , string terrainPath)
	{
		BackgroundPath =  Resources.Load<Sprite> (bgPath);
		TerrainPath =  Resources.Load<Sprite>(terrainPath);
	}
}
