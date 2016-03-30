using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ChooseMagicCardUI : ChooseUI {
	
	void Start()
	{
		_deck = _deckGrid.GetComponentsInChildren<CardUI> ();
		Enum[] cards = new Enum[] {
			CardType.SCORCHED_EARTH,
			CardType.DEFENSE_BUFF,
			CardType.ATTACK_BUFF,
			CardType.MORALE_DEBUFF,
			CardType.ATTACK_BUFF,
			CardType.ATTACK_BUFF,
			CardType.ATTACK_BUFF,
			CardType.ATTACK_BUFF,
			CardType.ATTACK_BUFF,
			CardType.ATTACK_BUFF,
			CardType.ATTACK_BUFF,
			CardType.ATTACK_BUFF,
			CardType.ATTACK_BUFF,
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

		MagicCardUI cardUI = btn.GetComponent<CardUI> () as MagicCardUI;
		cardUI._cardType = (CardType) type;
	}

	protected override void ButtonAction(CardUI card)
	{
		if (IsDuplicate (card)) {
			print ("This card is already in your deck");
			return;
		}

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

}
