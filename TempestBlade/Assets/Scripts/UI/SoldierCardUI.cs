using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoldierCardUI : CardUI
{
	private SoldierType soldierType;

	public SoldierType _soldierType
	{
		get { return soldierType; }
		set 
		{
			//transform.GetChild(0).GetComponent<Image>().sprite =  Resources.Load<Sprite>( Cache.instance.soldierIconPaths[value]);
			soldierType = value;
		}
	}

	public override void Reset()
	{
		_soldierType = SoldierType.NULL;
		base.Reset ();
	}
}


