using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "character_dialogue", menuName = "Character Dialogue")]
public class CharacterDialogue : ScriptableObject {
	
	public string characterName;
	public List<DialogueNode> conversation = new List<DialogueNode>();
}
