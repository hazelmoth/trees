using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour {

	Woodcutter player;
	UIManager uiManager;
	CharacterDialogue currentCharacterInDialogue;
	DialogueNode currentDialogueNode;
	public static DialogueController controller;

	void Start () {
		uiManager = UIManager.manager;
		controller = this;
		player = Woodcutter.player;
	}

	public void ActivateDialogue(CharacterDialogue characterEnteringDialogue)
	{

		currentCharacterInDialogue = characterEnteringDialogue;
		currentDialogueNode = characterEnteringDialogue.conversation[0];

		UpdateDialogue();
	}

	public void ExitDialogue()
	{
		player.OnDialogueDeactivate ();

		currentCharacterInDialogue = null;
		currentDialogueNode = null;
	}

	public void UpdateDialogue()
	{
		uiManager.SetCharacterNameText (currentCharacterInDialogue.characterName);
		uiManager.SetDialogueText(currentDialogueNode.dialogueText);


		// Set buttons as active and set their text depending on how many dialogue responses are available.
		// (Hopefully never more than 3; there are only 3 buttons.)


		if (currentDialogueNode.dialogueResponses.Count >= 1) {
			uiManager.SetTextDialogueChoice1(currentDialogueNode.dialogueResponses [0].responseText);
		}
		if (currentDialogueNode.dialogueResponses.Count >= 2) {
			uiManager.SetTextDialogueChoice2(currentDialogueNode.dialogueResponses [1].responseText);
		}
		if (currentDialogueNode.dialogueResponses.Count == 3) {
			uiManager.SetTextDialogueChoice3(currentDialogueNode.dialogueResponses [2].responseText);
		}



		uiManager.SetEnabledDialogueChoice1(currentDialogueNode.dialogueResponses.Count >= 1);
		uiManager.SetEnabledDialogueChoice2(currentDialogueNode.dialogueResponses.Count >= 2);
		uiManager.SetEnabledDialogueChoice3(currentDialogueNode.dialogueResponses.Count == 3);


	}


	public void onDialogueButton1()
	{
		EventSystem.current.SetSelectedGameObject(null); // Stop the button from being highlighted after it is pressed

		if (currentDialogueNode.dialogueResponses[0].isExitResponse == true)
		{
			ExitDialogue();
		}
		else
		{
			currentDialogueNode = currentDialogueNode.dialogueResponses[0].dialogueLink;
			UpdateDialogue();
		}
	}

	public void onDialogueButton2()
	{
		EventSystem.current.SetSelectedGameObject(null);

		if (currentDialogueNode.dialogueResponses[1].isExitResponse == true)
		{
			ExitDialogue();
		}
		else
		{
			currentDialogueNode = currentDialogueNode.dialogueResponses[1].dialogueLink;
			UpdateDialogue();
		}
	}

	public void onDialogueButton3()
	{
		EventSystem.current.SetSelectedGameObject(null);

		if (currentDialogueNode.dialogueResponses[2].isExitResponse == true)
		{
			ExitDialogue();
		}
		else
		{
			currentDialogueNode = currentDialogueNode.dialogueResponses[2].dialogueLink;
			UpdateDialogue();
		}
	}
}
