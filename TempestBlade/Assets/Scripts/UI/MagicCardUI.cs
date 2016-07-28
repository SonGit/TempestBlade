using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagicCardUI : CardUI {
	
	private CardType cardType;

	public Image _img;

	public Text _desc;

	public Text _name;

	public CardType _cardType
	{
		get { return cardType; }
		set 
		{
			_img.sprite =  Resources.Load<Sprite>( Cache.instance.cardIconPaths[value]);
			cardType = value;
		}
	}

	public override void Reset()
	{
		_cardType = CardType.NULL;
		base.Reset ();
	}

	public void Setup(CardType type)
	{
		_cardType = type;
		//_desc.text = Cache.instance.UnitDescription [type];
		_name.text = _img.sprite.name;
	}

	public void OnRemove()
	{
		Reset ();
	}
}
