using UnityEngine;
using System.Collections;

public class Player  {

	public CardType[] allCards;
	public SoldierType[] allSoldierCards;

	public CardType[] cardDeck;
	public SoldierType[] soldierDeck;

	public Player()
	{
		allCards = new CardType[16];
		allSoldierCards = new SoldierType[16];

		cardDeck = new CardType[5];
		soldierDeck = new SoldierType[5];
	}

	public void AddToDeck(CardType[] cards)
	{
		cardDeck = cards;
	}

	public void AddToDeck(SoldierType[] soldiers)
	{
		soldierDeck = soldiers;
		PrintAll ();
	}

	void PrintAll()
	{
		for (int i = 0; i < cardDeck.Length; i++) {
			Debug.Log (cardDeck[i]);
		}
		for (int i = 0; i < soldierDeck.Length; i++) {
			Debug.Log (soldierDeck[i]);
		}
	}
		
}
