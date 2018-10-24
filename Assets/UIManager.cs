using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class UIManager : MonoBehaviour {

	public static UIManager manager;

	[SerializeField] Text interactText;
	[SerializeField] GameObject dialoguePanel;
	[SerializeField] Text dialogueCharacterNameText;
	[SerializeField] Text dialogueText;
	[SerializeField] GameObject dialogueChoice1;
	[SerializeField] GameObject dialogueChoice2;
	[SerializeField] GameObject dialogueChoice3;
	[SerializeField] GameObject characterMenu;
	[SerializeField] Text ducatCount;
	[SerializeField] Text skillCutting;
	[SerializeField] Text skillFinagling;
	[SerializeField] Text skillPlanting;
	[SerializeField] Text playerNameText;
	Text dialogueChoiceText1;
	Text dialogueChoiceText2;
	Text dialogueChoiceText3;

	// Use this for initialization
	void Start () {
		manager = this;
		dialogueChoiceText1 = dialogueChoice1.GetComponentInChildren<Text> ();
		dialogueChoiceText2 = dialogueChoice2.GetComponentInChildren<Text> ();
		dialogueChoiceText3 = dialogueChoice3.GetComponentInChildren<Text> ();
		SwitchToMainUI ();
		CloseCharacterMenu (false);
		playerNameText.text = PlayerData.playerName;
		skillCutting.text = "Cutting: " + PlayerData.skillCutting;
		skillFinagling.text = "Finagling: " + PlayerData.skillFinagling;
		skillPlanting.text = "Planting: " + PlayerData.skillPlanting;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetInteractText() {
		interactText.text = "";
	}
	public void SetInteractTextToLookingAtTree () {
		interactText.text = "E to cut";
	}
	public void SetInteractTextToCarryingATree () {
		interactText.text = "You are carrying a tree";
	}
	public void SetInteractTextToLookingAtAHuman (string name) {
		interactText.text = name + " - E to talk";
	}
	public void SetInteractTextToLookingAtASawmillWhileHoldingALog (int treeValue) {
		interactText.text = "E to sell your tree for " + treeValue + " ducats";
	}
	public void SetInteractTextToLookingAtASawmillWithoutHoldingALog () {
		interactText.text = "You have no wood to sell";
	}
	public void SetInteractTextToLookingAtAShop(int saplingCost, int currentNumOfSaplings, bool canAfford) {
		if (canAfford)
			interactText.text = "E to buy a sapling for " + saplingCost + " ducats\n(Right now you have " + currentNumOfSaplings + " saplings)";
		else
			interactText.text = "You can't afford to buy a sapling for " + saplingCost + " ducats\n(Right now you have " + currentNumOfSaplings + " saplings)";
	}
	public void SetInteractTextToPlantingASapling(int saplingsAvailable) {
		interactText.text = "Click to plant a sapling | F to cancel\n" + saplingsAvailable + " saplings available";
	}
	public void SwitchToDialogueUI () {
		interactText.gameObject.SetActive (false);
		dialoguePanel.SetActive (true);
		DisableCursorLock ();
	}
	public void SwitchToMainUI () {
		interactText.gameObject.SetActive (true);
		dialoguePanel.SetActive (false);
		EnableCursorLock ();
	}
	public void SetCharacterNameText (string text) {
		dialogueCharacterNameText.text = text;
	}
	public void SetDialogueText (string text) {
		dialogueText.text = text;
	}
	public void SetTextDialogueChoice1 (string text) {
		dialogueChoiceText1.text = text;
	}
	public void SetTextDialogueChoice2 (string text) {
		dialogueChoiceText2.text = text;
	}
	public void SetTextDialogueChoice3 (string text) {
		dialogueChoiceText3.text = text;
	}
	public void SetEnabledDialogueChoice1 (bool enabled) {
		dialogueChoice1.SetActive (enabled);
	}
	public void SetEnabledDialogueChoice2 (bool enabled) {
		dialogueChoice2.SetActive (enabled);
	}
	public void SetEnabledDialogueChoice3 (bool enabled) {
		dialogueChoice3.SetActive (enabled);
	}
	public void OnPressButton1 () {
		EventSystem.current.SetSelectedGameObject(null); // Stop the button from being highlighted after it is pressed
		DialogueController.controller.onDialogueButton1();
	}
	public void OnPressButton2 () {
		EventSystem.current.SetSelectedGameObject(null);
		DialogueController.controller.onDialogueButton2();
	}
	public void OnPressButton3 () {
		EventSystem.current.SetSelectedGameObject(null);
		DialogueController.controller.onDialogueButton3();
	}
	public void EnableCursorLock () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	public void DisableCursorLock () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	public void OpenCharacterMenu () {
		interactText.gameObject.SetActive (false);
		dialoguePanel.SetActive (false);
		characterMenu.SetActive (true);
		DisableCursorLock ();
	}
	public void CloseCharacterMenu (bool openDialoguePanel) {
		characterMenu.SetActive (false);
		if (openDialoguePanel) {
			SwitchToDialogueUI ();
		} else {
			SwitchToMainUI();
		}
	}
	public void UpdateDucatDisplay (int ducats, int saplings) {
		ducatCount.text = ducats + " Ducats | " + saplings + " Saplings";
	}
}
