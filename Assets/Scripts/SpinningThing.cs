using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningThing : MonoBehaviour {

	[SerializeField] float spinSpeed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward, spinSpeed * Time.deltaTime);
	}
}
