using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

	[SerializeField] Text nameInputText;
	[SerializeField] GameObject nameScreen;
	[SerializeField] GameObject classScreen;
	[SerializeField] GameObject finishScreen;

	// Use this for initialization
	void Start () {
		nameScreen.SetActive (true);
		classScreen.SetActive (false);
		finishScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void nameScreenButton () {
		PlayerData.playerName = nameInputText.text;
		nameScreen.SetActive (false);
		classScreen.SetActive (true);
	}

	public void selectClassWoodcutter () {
		PlayerData.skillCutting = 2;
		PlayerData.skillFinagling = 3;
		PlayerData.skillPlanting = 1;
		classScreen.SetActive (false);
		finishScreen.SetActive (true);
	}
	public void selectClassLogger () {
		PlayerData.skillCutting = 3;
		PlayerData.skillFinagling = 1;
		PlayerData.skillPlanting = 2;
		classScreen.SetActive (false);
		finishScreen.SetActive (true);
	}
	public void selectClassLumberJack () {
		PlayerData.skillCutting = 2;
		PlayerData.skillFinagling = 1;
		PlayerData.skillPlanting = 3;
		classScreen.SetActive (false);
		finishScreen.SetActive (true);
	}
	public void finishButton () {
		SceneManager.LoadScene ("main");
	}
}
