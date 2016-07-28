using UnityEngine;
using System.Collections;

public class IntelTab : MonoBehaviour {

	public GameObject _tutorialPanel;

	public void OnClickTutorial()
	{
		if (_tutorialPanel.activeInHierarchy)
			_tutorialPanel.SetActive (false);
		else
			_tutorialPanel.SetActive (true);
	}
}
