using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter : MonoBehaviour {

	Camera camera;
	UIManager uiManager;
	[SerializeField] GameObject saplingHologramPrefab;
	GameObject saplingHologramInstance;

	public static Woodcutter player;

	string name;
	float rayCastDist = 3f;
	bool isCarryingATree = false;
	float sizeOfCurrentTree;
	bool isInDialogue = false;
	bool characterMenuOpen = false;
	bool isPlantingASapling = false;

	int currentSaplings = 0;
	int currentDucats = 0;

	// Use this for initialization
	void Start () {
		camera = GetComponentInChildren<Camera> ();
		uiManager = UIManager.manager;
		player = this;
		uiManager.UpdateDucatDisplay (currentDucats, currentSaplings);

		saplingHologramInstance = GameObject.Instantiate (saplingHologramPrefab, Vector3.zero, Quaternion.identity);
		saplingHologramInstance.SetActive (false);

		float saplingScale = 0.1f + 0.05f * (PlayerData.skillPlanting - 1);
		saplingHologramInstance.transform.localScale = new Vector3 (saplingScale, saplingScale, saplingScale);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isInDialogue) {
			CastRayForInteractions ();
			if (Input.GetKeyDown(KeyCode.F)) {
				if (currentSaplings > 0 && !isPlantingASapling) {
					ActivateSaplingPlanting ();
				}
				else if (isPlantingASapling) {
					DeactivateSaplingPlanting ();
				} else {
					// no saplings to plant
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Tab)) {
			if (!characterMenuOpen) {
				uiManager.OpenCharacterMenu ();
				characterMenuOpen = true;
			} else {
				uiManager.CloseCharacterMenu (isInDialogue);
				characterMenuOpen = false;
			}
		} 
	}

	void CastRayForInteractions () {
		RaycastHit hit;
		Physics.Raycast (camera.transform.position, camera.transform.forward, out hit, rayCastDist);
		if (isPlantingASapling) {
			uiManager.SetInteractTextToPlantingASapling (currentSaplings);
			if (hit.collider && hit.collider.GetComponent<Terrain> ()) {
				saplingHologramInstance.transform.position = hit.point;
				if (!saplingHologramInstance.activeInHierarchy)
					saplingHologramInstance.SetActive (true);
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					PlantSapling (hit.point);
				}
			} else {
				saplingHologramInstance.SetActive (false);
			}
		} else {
			saplingHologramInstance.SetActive (false);
			if (hit.collider && hit.collider.GetComponent<CuttableTree> () && !isCarryingATree) {
				uiManager.SetInteractTextToLookingAtTree ();
				if (Input.GetKeyDown (KeyCode.E)) {
					GrabATree (hit.collider.GetComponent<CuttableTree> ().GetCurrentSize ());
					Destroy (hit.collider.gameObject);
				}
			} else if (hit.collider && hit.collider.GetComponent<NPC> ()) {
				uiManager.SetInteractTextToLookingAtAHuman (hit.collider.GetComponent<NPC> ().dialogue.characterName);
				if (Input.GetKeyDown (KeyCode.E)) {
					// Talk to the human
					ActivateDialogue (hit.collider.GetComponent<NPC> ());
				}
			} else if (hit.collider && hit.collider.GetComponentInParent<Sawmill> ()) {
				if (isCarryingATree) {
					uiManager.SetInteractTextToLookingAtASawmillWhileHoldingALog (Mathf.RoundToInt (VillageStatus.instance.GetCurrentTreeExchangeRate () * sizeOfCurrentTree));
					if (Input.GetKeyDown (KeyCode.E)) {
						//Sell the log
						AddDucats (Mathf.RoundToInt (VillageStatus.instance.GetCurrentTreeExchangeRate () * sizeOfCurrentTree));
						isCarryingATree = false;
					}
				} else
					uiManager.SetInteractTextToLookingAtASawmillWithoutHoldingALog ();
			} else if (hit.collider && hit.collider.GetComponentInParent<Shop> () && !isCarryingATree) {
				uiManager.SetInteractTextToLookingAtAShop (VillageStatus.instance.GetCurrentSaplingExchangeRate (), currentSaplings, (currentDucats >= VillageStatus.instance.GetCurrentSaplingExchangeRate ()));
				if (Input.GetKeyDown (KeyCode.E)) {
					//Get a sapling
					if (currentDucats >= VillageStatus.instance.GetCurrentSaplingExchangeRate ()) {
						AddSaplings (1);
						AddDucats (-1 * VillageStatus.instance.GetCurrentSaplingExchangeRate ());
					}
				}
			} else {
				if (isCarryingATree)
					uiManager.SetInteractTextToCarryingATree ();
				else
					uiManager.ResetInteractText ();
			}
		}
	}

	void ActivateDialogue (NPC npc) {
		isInDialogue = true;
		uiManager.SwitchToDialogueUI ();
		DialogueController.controller.ActivateDialogue (npc.dialogue);
		saplingHologramInstance.SetActive (false);
	}

	public void OnDialogueDeactivate () {
		isInDialogue = false;
		uiManager.SwitchToMainUI ();
	}

	void GrabATree (float size) {
		isCarryingATree = true;
		sizeOfCurrentTree = size;
		uiManager.SetInteractTextToCarryingATree ();
	}

	void AddDucats (int amount) {
		currentDucats += amount;
		uiManager.UpdateDucatDisplay (currentDucats, currentSaplings);
	}

	void AddSaplings (int amount) {
		currentSaplings += amount;
		uiManager.UpdateDucatDisplay (currentDucats, currentSaplings);
	}

	void ActivateSaplingPlanting () {
		if (currentSaplings > 0)
			isPlantingASapling = true;
	}

	void DeactivateSaplingPlanting () {
		isPlantingASapling = false;
	}

	void PlantSapling (Vector3 location) {
		TreeManager.PlantSapling (location);
		currentSaplings--;
		if (currentSaplings >= 0) {
			DeactivateSaplingPlanting ();
		}
	}
}
