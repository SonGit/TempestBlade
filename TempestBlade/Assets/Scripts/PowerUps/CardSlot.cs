using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Shared;

public class CardSlot : MonoBehaviour {
	public Image clickedEffect;
	public Image cooldown;
	public bool coolingDown;
	public float waitTime = 30.0f;
	public bool isAvailable = true;

	private CardType cardType;

	public CardType _cardType
	{
		get { return cardType; }
		set 
		{
			transform.GetChild(0).GetComponent<Image>().sprite =  Resources.Load<Sprite>( Cache.instance.cardIconPaths[value]);
			cardType = value;
		}
	}

	Callback callback;

	//These vars are only for UI in MapScene
	//public bool _isChosen;
	public bool isClicked;

	public bool _isClicked
	{
		get { return isClicked; }
		set 
		{
			clickedEffect.enabled = value;
			isClicked = value;
		}
	}

	void Start()
	{
		cooldown.fillAmount = 0;
	}

	// Update is called once per frame
	void Update () 
	{
		if (coolingDown == true)
		{
			//Reduce fill amount over 30 seconds
			cooldown.fillAmount -= 1.0f/waitTime * Time.deltaTime;

			if (cooldown.fillAmount <= 0) {
				coolingDown = false;
				isAvailable = true;
				callback ();
			}
		}
	}

	public void StartCooldown(float waitTime,Callback callback)
	{
		this.callback = callback;
		this.waitTime = waitTime;
		cooldown.fillAmount = 1;
		coolingDown = true;
		isAvailable = false;
	}
		
}
