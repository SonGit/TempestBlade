using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum CardType
{
	NULL,
	SCORCHED_EARTH,
	DEFENSE_BUFF,
	ATTACK_BUFF,
	MORALE_DEBUFF,
};

public class CardManager : MonoBehaviour {

	public static CardManager instance;

	public GameObject _cardSlotPrefab;

	public GridLayoutGroup _grid;

	public PowerUpCard[] _cards;

	public float _timeout;

	public List<Button> _cardButtons;

	const int MAX_CARD = 6;

	public Button _lastClickedCard;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {

		CardType[] cards = new CardType[] {
			CardType.SCORCHED_EARTH,
			CardType.DEFENSE_BUFF,
			CardType.NULL,
			CardType.ATTACK_BUFF,
			CardType.MORALE_DEBUFF
		};

		//SetCards(cards);
		SetCards(PlayerInfoCache.instance.GetMagicCardDeck());
	}

	public void SetCards(CardType[] cards)
	{
		for (int i = 0; i < cards.Length ; i++) {

			if (cards [i] != CardType.NULL) {
				GameObject go = (GameObject)Instantiate (_cardSlotPrefab);
				go.transform.parent = _grid.transform;

				Button btn = go.GetComponent<Button> ();
				ButtonSetter(btn,cards[i]);

				_cardButtons.Add (btn);
			}

		}
	}

	public void ActivateScorchedEarth(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;
		
		ActivateCard(CardType.SCORCHED_EARTH,TacticalAnalyst.instance._enemyLeader);
		StartCooldown (10,slot);
	}

	public void ActivateDefenseBuff(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;
		
		ActivateCard(CardType.DEFENSE_BUFF,TacticalAnalyst.instance._allyLeader);
		StartCooldown (10,slot);
	}

	public void ActivateAttackBuff(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;
		
		ActivateCard(CardType.ATTACK_BUFF,TacticalAnalyst.instance._allyLeader);
		StartCooldown (10,slot);
	}

	public void ActivateMoraleDebuff(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;
		
		ActivateCard(CardType.MORALE_DEBUFF,TacticalAnalyst.instance._enemyLeader);
		StartCooldown (10,slot);
	}

	void StartCooldown(float amount,CardSlot clickedCard)
	{
		print ("COOLING");
		foreach (Button button in _cardButtons) {
			CardSlot cardSlot = button.GetComponent<CardSlot>();
			cardSlot.StartCooldown (5,OnEndCooldown);
		}

		clickedCard.StartCooldown(amount,OnEndCooldown);
	}

	void OnEndCooldown()
	{

	}

	public void ActivateCard(CardType type,SquadLeader target)
	{
		foreach (PowerUpCard card in _cards) {
			if (card._type == type) {
				card.Execute (target);
				return;
			}
		}
		Debug.Log ("No suitable card found!!!");
	}

	void ButtonSetter(Button btn,CardType type)
	{
		switch (type) {

		case CardType.ATTACK_BUFF:
			btn.onClick.AddListener(() =>
				{
					ActivateAttackBuff(btn.GetComponent<CardSlot>());
				});
			break;
		case CardType.DEFENSE_BUFF:
			btn.onClick.AddListener(() =>
				{
					ActivateDefenseBuff(btn.GetComponent<CardSlot>());
				});
			break;
		case CardType.MORALE_DEBUFF:
			btn.onClick.AddListener(() =>
				{
					ActivateMoraleDebuff(btn.GetComponent<CardSlot>());
				});
			break;
		case CardType.SCORCHED_EARTH:
			btn.onClick.AddListener(() =>
				{
					ActivateScorchedEarth(btn.GetComponent<CardSlot>());
				});
			break;
		}

		btn.GetComponent<CardSlot> ()._cardType = type;
	}
}
