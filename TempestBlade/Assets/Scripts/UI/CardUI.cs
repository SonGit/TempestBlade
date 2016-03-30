using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class CardUI : MonoBehaviour {
	
	public Image clickedEffect;

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

	public bool _isInDeck;

	public Button RemoveCardButton;

	public void EnableRemoveCardButton()
	{
		RemoveCardButton.gameObject.SetActive (true);
	}

	//Called when remove button card is clicked
	public virtual void Reset()
	{
		_isInDeck = false;
		_isClicked = false;
		RemoveCardButton.gameObject.SetActive (false);
	}
}
