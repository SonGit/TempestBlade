using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ChooseSoldierCardUI : ChooseUI {

	public GameObject _lastUI;

	public int _currentPage = 0;

	Dictionary<int,SoldierType[]> _Library = new Dictionary<int, SoldierType[]>
	{
		{0, new SoldierType[]{SoldierType.KNIGHT,SoldierType.VINDICATOR,SoldierType.BRUTE,SoldierType.BERSERKER}},
		{1, new SoldierType[]{SoldierType.IRON_GOLEM,SoldierType.DEFENDER,SoldierType.ROGUE,SoldierType.MINOTAUR}}
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
		SoldierCardUI availableSlot = (SoldierCardUI)GetAvailableSlotInDeck ();
		SoldierCardUI magicCard = (SoldierCardUI)card;

		if (availableSlot == null)
			return;

		availableSlot._isInDeck = true;
		availableSlot.EnableRemoveCardButton ();

		availableSlot._soldierType = magicCard._soldierType;

		print (availableSlot._soldierType);
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

		print (magicCard1._soldierType);
		print (magicCard2._soldierType);
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

	private void CreatePage(SoldierType[] types)
	{

		for (int i = 0; i < types.Length; i++) {
			SoldierCardUI soldierCard = (SoldierCardUI)_listOfCards[i]; 

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
