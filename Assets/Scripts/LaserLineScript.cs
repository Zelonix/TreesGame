using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLineScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.name == "Player") {
			collider.GetComponent<PlayerScript> ().gotHit (1000);
		} 
		else {
			Object.Destroy (collider.gameObject);
		}
	}
}
