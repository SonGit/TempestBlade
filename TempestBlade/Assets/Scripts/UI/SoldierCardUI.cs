using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoldierCardUI : CardUI
{
	private SoldierType soldierType;

	public Image _img;

	public Text _desc;

	public Text _name;

	public SoldierType _soldierType
	{
		get { return soldierType; }
		set 
		{
			_img.sprite =  Resources.Load<Sprite>( Cache.instance.soldierIconPaths[value]);
			soldierType = value;
		}
	}

	public override void Reset()
	{
		_soldierType = SoldierType.NULL;
		base.Reset ();
	}

	public void Setup(SoldierType type)
	{
		_soldierType = type;
		_desc.text = Cache.instance.UnitDescription [type];
		_name.text = _img.sprite.name;
	}

	public void OnRemove()
	{
		Reset ();
	}
}


