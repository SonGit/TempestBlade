using UnityEngine;
using System.Collections;

public class PlayerInfoCache {

	static readonly PlayerInfoCache _instance = new PlayerInfoCache();

	public MapProperties _currentMap;

	public static PlayerInfoCache instance
	{
		get { return _instance; }
	}

	Player player;

	private PlayerInfoCache()
	{
		player = new Player ();
	}

	public void AddToDeck(CardType[] cards)
	{
		player.AddToDeck (cards);
	}
		
	public void AddToDeck(SoldierType[] cards)
	{
		for (int i = 0; i < cards.Length; i++) {
			if (cards[i] == SoldierType.NULL) {
				cards[i] = SoldierType.KNIGHT;
			}
		}
		player.AddToDeck (cards);
	}

	public CardType[] GetMagicCardDeck()
	{
		return player.cardDeck;
	}

	public SoldierType[] GetSoldierCardDeck()
	{
		return player.soldierDeck;
	}
}
