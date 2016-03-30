using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagicCardUI : CardUI {


	private CardType cardType;

	public CardType _cardType
	{
		get { return cardType; }
		set 
		{
			transform.GetChild(0).GetComponent<Image>().sprite =  Resources.Load<Sprite>( Cache.instance.cardIconPaths[value]);
			cardType = value;
		}
	}

	public override void Reset()
	{
		_cardType = CardType.NULL;
		base.Reset ();
	}
}
