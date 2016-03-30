using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public abstract class ChooseUI : MonoBehaviour {
	
	public GameObject _cardPrefab;

	public GridLayoutGroup _grid;

	public GameObject _deckGrid;

	public GameObject _linkedUI;


	CardUI _cachedCard;

	public CardUI[] _deck;
	// Use this for initialization
	void Start () {
		
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
		for (int i = 0; i < cardTypes.Length ; i++) {

			GameObject go = (GameObject)Instantiate (_cardPrefab);
			go.transform.parent = _grid.transform;

			Button btn = go.GetComponent<Button> ();
			ButtonSetter(btn,cardTypes[i]);

		}
	}

	protected virtual void ButtonSetter(Button btn,Enum type)
	{
		btn.onClick.AddListener(() =>
			{
				ButtonAction(btn.GetComponent<CardUI>());
			});

		//btn.GetComponent<CardUI> ()._cardType = type;
	}


	protected abstract void ButtonAction (CardUI cardSlot);

	protected virtual void SwapAction(CardUI card)
	{

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
		_linkedUI.gameObject.SetActive (true);
		gameObject.SetActive (false);
	}

	protected abstract void AddToDeck();
}
