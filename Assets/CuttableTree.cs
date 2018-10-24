using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableTree : MonoBehaviour {

	[SerializeField] float growthPerSecond = 0.1f;
	[SerializeField] float oldGrowthPerSecond = 0.05f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(gameObject.transform.localScale.x < 1f) {
			float amountToGrow = growthPerSecond * Time.fixedDeltaTime;
			transform.localScale = transform.localScale + new Vector3 (amountToGrow, amountToGrow, amountToGrow);
		}
		else if(gameObject.transform.localScale.x < 10f) {
			float amountToGrow = oldGrowthPerSecond * Time.fixedDeltaTime;
			transform.localScale = transform.localScale + new Vector3 (amountToGrow, amountToGrow, amountToGrow);
		}
	}

	public void SetSizeToSapling () {
		gameObject.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
	}

	public float GetCurrentSize() {
		return gameObject.transform.localScale.x;
	}
}
