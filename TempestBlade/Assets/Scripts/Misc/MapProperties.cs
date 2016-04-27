using UnityEngine;
using System.Collections;

public class MapProperties  {

	public SoldierType[] _soldiers;
	public SceneryType _scenery;
	public WeatherType _weather;

	public MapProperties( SoldierType[] soldiers , SceneryType sceneryType , WeatherType weather)
	{
		_soldiers = soldiers;
		_scenery = sceneryType;
		_weather = weather;
	}

}
