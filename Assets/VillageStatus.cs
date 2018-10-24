using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageStatus : MonoBehaviour {

	public static VillageStatus instance;

	int currentTreeExchangeRate;
	int currentSaplingExchangeRate;
	int baseTreeExchangeRate = 10; // The core value that the exchange rate will fluctuate around
	int baseSaplingExchangeRate = 3;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		currentTreeExchangeRate = baseTreeExchangeRate + Mathf.RoundToInt(2f * Mathf.Sin (Time.time/100));
		currentSaplingExchangeRate = baseSaplingExchangeRate + (currentTreeExchangeRate - baseTreeExchangeRate);
		currentTreeExchangeRate += PlayerData.skillFinagling - 1;
	}

	public int GetCurrentTreeExchangeRate () {
		return currentTreeExchangeRate;
	}
	public int GetCurrentSaplingExchangeRate() {
		return currentSaplingExchangeRate;
	}
}
