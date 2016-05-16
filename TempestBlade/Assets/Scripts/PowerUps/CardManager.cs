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
	ATTACK_RATE_DEBUFF,
	ATTACK_RATE_BUFF,
	WIND_OF_RUST,
	FLAMING_ARROW,
	ELECTRIC_GROUND,
	FRENZY
};

public class CardManager : MonoBehaviour {

	public static CardManager instance;

	public StackManager _stackManager;

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
		foreach (PowerUpCard card in _cards) {
			card.Init ();
		}
	}

	// Use this for initialization
	void Start () {

		CardType[] cards = new CardType[] {
			CardType.SCORCHED_EARTH,
			CardType.FLAMING_ARROW,
			CardType.ELECTRIC_GROUND,
			CardType.FRENZY
		};

		SetCards(cards);
		//SetCards(PlayerInfoCache.instance.GetMagicCardDeck());
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

	public void ActivateAttackRateDebuff(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;

		ActivateCard(CardType.ATTACK_RATE_DEBUFF,TacticalAnalyst.instance._enemyLeader);
		StartCooldown (10,slot);
	}


	public void ActivateAttackRateBuff(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;

		ActivateCard(CardType.ATTACK_RATE_BUFF,TacticalAnalyst.instance._allyLeader);
		StartCooldown (10,slot);
	}

	void StartCooldown(float amount,CardSlot clickedCard)
	{
		foreach (Button button in _cardButtons) {
			CardSlot cardSlot = button.GetComponent<CardSlot>();
			cardSlot.StartCooldown (5,OnEndCooldown);
		}

		clickedCard.StartCooldown(amount,OnEndCooldown);
	}

	public void ActivateWindOfRust(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;

		ActivateCard(CardType.WIND_OF_RUST,TacticalAnalyst.instance._allyLeader);
		StartCooldown (10,slot);
	}

	public void ActivateFlamingArrow(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;

		ActivateCard(CardType.FLAMING_ARROW,TacticalAnalyst.instance._enemyLeader);
		StartCooldown (10,slot);
	}

	public void ActivateElectricGround(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;

		ActivateCard(CardType.ELECTRIC_GROUND,TacticalAnalyst.instance._enemyLeader);
		StartCooldown (10,slot);
	}

	public void ActivateFrenzy(CardSlot slot)
	{
		if (!slot.isAvailable)
			return;

		ActivateCard(CardType.FRENZY,TacticalAnalyst.instance._enemyLeader);
		StartCooldown (10,slot);
	}

	void OnEndCooldown()
	{

	}

	public void ActivateCard(CardType type,SquadLeader target)
	{
		foreach (PowerUpCard card in _cards) {
			if (card._type == type) {
				card.Activate (target);
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

		case CardType.ATTACK_RATE_DEBUFF:
		btn.onClick.AddListener(() =>
			{
				ActivateAttackRateDebuff(btn.GetComponent<CardSlot>());
			});
		break;

		case CardType.ATTACK_RATE_BUFF:
			btn.onClick.AddListener(() =>
				{
					ActivateAttackRateBuff(btn.GetComponent<CardSlot>());
				});
			break;

		case CardType.WIND_OF_RUST:
			btn.onClick.AddListener(() =>
				{
					ActivateWindOfRust(btn.GetComponent<CardSlot>());
				});
			break;

		case CardType.FLAMING_ARROW:
			btn.onClick.AddListener(() =>
				{
					ActivateFlamingArrow(btn.GetComponent<CardSlot>());
				});
			break;
		case CardType.ELECTRIC_GROUND:
			btn.onClick.AddListener(() =>
				{
					ActivateElectricGround(btn.GetComponent<CardSlot>());
				});
			break;
		case CardType.FRENZY:
			btn.onClick.AddListener(() =>
				{
					ActivateFrenzy(btn.GetComponent<CardSlot>());
				});
			break;
	}


		btn.GetComponent<CardSlot> ()._cardType = type;
	}
}
