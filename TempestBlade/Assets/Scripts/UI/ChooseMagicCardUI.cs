using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ChooseMagicCardUI : ChooseUI {
	public GameObject _lastUI;

	public int _currentPage = 0;

	Dictionary<int,CardType[]> _Library = new Dictionary<int, CardType[]>
	{
		{0, new CardType[]{CardType.ATTACK_BUFF,CardType.ATTACK_RATE_BUFF,CardType.ATTACK_RATE_DEBUFF,CardType.DEFENSE_BUFF}},
		{1, new CardType[]{CardType.ATTACK_BUFF,CardType.ATTACK_RATE_BUFF,CardType.ATTACK_RATE_DEBUFF,CardType.DEFENSE_BUFF}},
	};

	protected override void Init()
	{
		Enum[] cards = new Enum[] {
			SoldierType.KNIGHT,
			SoldierType.KNIGHT,
		};

		SpawnCards(cards);

		//Allow swaps
		foreach (CardUI card in _deck) {

			Button btn = card._swapBtn;

			if (btn != null) {
				DeckButtonSetter(btn);
			}
		}

		CreatePage (_Library[0]);
	}

	void DeckButtonSetter(Button btn)
	{
		btn.onClick.AddListener(() =>
			{
				SwapAction(btn.GetComponentInParent<CardUI>());
			});
	}

	protected override void ButtonSetter(CardUI card)
	{
		base.ButtonSetter (card);
	}

	protected override void ButtonAction(CardUI card)
	{
		if (!card._isInDeck) {
			PutToDeck(card);
		}
	}

	protected override void PutToDeck(CardUI card)
	{
		MagicCardUI availableSlot = (MagicCardUI)GetAvailableSlotInDeck ();
		MagicCardUI magicCard = (MagicCardUI)card;

		if (availableSlot == null)
			return;

		availableSlot._isInDeck = true;
		availableSlot.EnableRemoveCardButton ();

		availableSlot._cardType = magicCard._cardType;
	}


	bool IsDuplicate(CardUI card_inQuestion)
	{
		MagicCardUI cardToLookFor = card_inQuestion as MagicCardUI;

		foreach (CardUI card in _deck) {
			MagicCardUI magicCard = card as MagicCardUI;
			if (magicCard != null) {
				if (magicCard._isInDeck && magicCard._cardType == cardToLookFor._cardType)
					return true;
			}
		}
		return false;
	}

	protected override void Swap (CardUI card1,CardUI card2)
	{
		MagicCardUI magicCard1 = (MagicCardUI)card1; 
		MagicCardUI magicCard2 = (MagicCardUI)card2; 

		CardType tmp = magicCard1._cardType;

		magicCard1._cardType = magicCard2._cardType;
		magicCard2._cardType = tmp;
	}

	protected override void AddToDeck()
	{
		CardType[] cards = new CardType[5];

		for (int i = 0; i < cards.Length; i++) {

			MagicCardUI card = (MagicCardUI)_deck [i];

			if(card != null)
				cards [i] = card._cardType;

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

	private void CreatePage(CardType[] types)
	{

		for (int i = 0; i < types.Length; i++) {
			MagicCardUI soldierCard = (MagicCardUI)_listOfCards[i]; 

			if (soldierCard != null) {
				soldierCard.Setup (types[i]);
			}
		}

	}

	public void OnClickNextPage()
	{
		//If reach mex page
		if (_currentPage + 1 >= _Library.Count) {
			return;
		}

		_currentPage++;

		CreatePage (_Library[_currentPage]);
	}

	public void OnClickPrevPage()
	{
		//If reach mex page
		if (_currentPage - 1 < 0) {
			return;
		}

		_currentPage--;

		CreatePage (_Library[_currentPage]);
	}
}
