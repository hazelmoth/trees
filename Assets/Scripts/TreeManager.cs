using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour {
	
	[SerializeField] GameObject saplingPrefab;

	public static TreeManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlantSapling(Vector3 location) {
		GameObject newSapling = GameObject.Instantiate (instance.saplingPrefab, location, Quaternion.identity);
		float scale = 0.1f + 0.05f * (PlayerData.skillPlanting - 1);
		newSapling.transform.localScale = new Vector3 (scale, scale, scale);
	}
}
