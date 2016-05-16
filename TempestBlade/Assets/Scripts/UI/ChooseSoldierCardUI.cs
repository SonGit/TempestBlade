using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ChooseSoldierCardUI : ChooseUI {

	public GameObject _lastUI;

	void Start()
	{
		_deck = _deckGrid.GetComponentsInChildren<CardUI> ();
		Enum[] cards = new Enum[] {
			SoldierType.KNIGHT,
			SoldierType.KNIGHT,
		};

		SpawnCards(cards);

		//Allow swaps
		Button[] deckBtns = _deckGrid.GetComponentsInChildren<Button> ();
		foreach (Button button in deckBtns) {
			DeckButtonSetter(button);
		}
	}

	void DeckButtonSetter(Button btn)
	{
		btn.onClick.AddListener(() =>
			{
				SwapAction(btn.GetComponent<CardUI>());
			});
	}

	protected override void ButtonSetter(Button btn,Enum type)
	{
		base.ButtonSetter (btn,type);

		SoldierCardUI cardUI = btn.GetComponent<CardUI> () as SoldierCardUI;
		cardUI._soldierType = (SoldierType) type;
	}

	protected override void ButtonAction(CardUI card)
	{
		if (!card._isInDeck) {
			PutToDeck(card);
		}
	}

	protected override void PutToDeck(CardUI card)
	{
		SoldierCardUI availableSlot = (SoldierCardUI)GetAvailableSlotInDeck ();
		SoldierCardUI magicCard = (SoldierCardUI)card;

		if (availableSlot == null)
			return;

		availableSlot._isInDeck = true;
		availableSlot.EnableRemoveCardButton ();

		availableSlot._soldierType = magicCard._soldierType;
	}


	bool IsDuplicate(CardUI card_inQuestion)
	{
		SoldierCardUI cardToLookFor = card_inQuestion as SoldierCardUI;

		foreach (CardUI card in _deck) {
			SoldierCardUI magicCard = card as SoldierCardUI;
			if (magicCard != null) {
				if (magicCard._isInDeck && magicCard._soldierType == cardToLookFor._soldierType)
					return true;
			}
		}
		return false;
	}

	protected override void Swap (CardUI card1,CardUI card2)
	{
		SoldierCardUI magicCard1 = (SoldierCardUI)card1; 
		SoldierCardUI magicCard2 = (SoldierCardUI)card2; 
		SoldierType tmp = magicCard1._soldierType;

		magicCard1._soldierType = magicCard2._soldierType;
		magicCard2._soldierType = tmp;
	}

	protected override void AddToDeck()
	{
		SoldierType[] cards = new SoldierType[5];

		for (int i = 0; i < cards.Length; i++) {

			SoldierCardUI card = (SoldierCardUI)_deck [i];

			if(card != null)
				cards [i] = card._soldierType;

		}

		PlayerInfoCache.instance.AddToDeck (cards);
	}

	//Call by a button when player have done choosing
	public void OnLastUI()
	{
		_lastUI.SetActive (true);
		gameObject.SetActive (false);
	}

	public void GoToBattle()
	{
		AddToDeck ();
		Application.LoadLevel ("TestScene");
	}
}
