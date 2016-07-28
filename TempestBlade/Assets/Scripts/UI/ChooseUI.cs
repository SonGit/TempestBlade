using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public abstract class ChooseUI : MonoBehaviour {
	
	//public GameObject _cardPrefab;

	public GameObject _cardGrid;

	public GameObject _deckGrid;

	public CardUI[] _listOfCards;

	public CardUI[] _deck;

	CardUI _cachedCard;

	// Use this for initialization
	void Start () {
		_listOfCards = _cardGrid.GetComponentsInChildren<CardUI> ();
		_deck = _deckGrid.GetComponentsInChildren<CardUI> ();
		Init ();
	}

	protected virtual void Init()
	{

	}

	protected CardUI GetAvailableSlotInDeck()
	{
		foreach (CardUI card in _deck) {
			if (!card._isInDeck)
				return card;
		}
		return null;
	}

	protected abstract void PutToDeck (CardUI card);
		
	protected virtual void SpawnCards(Enum[] cardTypes)
	{
		//for (int i = 0; i < cardTypes.Length ; i++) {

		//GameObject go = (GameObject)Instantiate (_cardPrefab);
		//go.transform.parent = _grid.transform;

		//Button btn = go.GetComponent<Button> ();
		//ButtonSetter(btn,cardTypes[i]);

		//}

		foreach (CardUI card in _listOfCards) {
			ButtonSetter(card);
		}
	}

	protected virtual void ButtonSetter(CardUI card)
	{
		Button btn = card.GetComponentInChildren<Button> ();
		btn.onClick.AddListener(() =>
			{
				ButtonAction(card);
			});

		//btn.GetComponent<CardUI> ()._cardType = type;
	}


	protected abstract void ButtonAction (CardUI cardSlot);

	protected virtual void SwapAction(CardUI card)
	{
		print (card.name);
		if (card._isInDeck) {

			if (_cachedCard == null) {
				card._isClicked = true;
				_cachedCard = card;
				return;
			}

			if (_cachedCard != null) {

				Swap (_cachedCard,card);

				card._isClicked = false;
				_cachedCard._isClicked = false;

				_cachedCard = null;
				return;
			}

		}

	}

	protected abstract void Swap (CardUI card1,CardUI card2);

	//Call by a button when player have done choosing
	public virtual void NextUI()
	{
		AddToDeck ();
		gameObject.SetActive (false);
	}

	protected abstract void AddToDeck();
}
