using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWeaponScript : MonoBehaviour {

	public GameObject weapon;
	GameObject player;
	public int price;
	public string displayText;
	bool inside = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player1");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.B) && inside) {
			player.GetComponentInChildren<ShootingScript>().AddWeapon(weapon, price);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.name == "Player1") {
			inside = true;
			GameObject.Find ("Canvas").GetComponent<CanvasScript> ().buyText.text = displayText;
			GameObject.Find ("Canvas").GetComponent<CanvasScript> ().buyText.text.Replace ("\\n", "\n");
			GameObject.Find ("Canvas").GetComponent<CanvasScript> ().buyText.enabled = true;
		}
	}
	void OnTriggerExit(Collider collider) {
		if (collider.name == "Player1") {
			inside = false;
			GameObject.Find ("Canvas").GetComponent<CanvasScript> ().buyText.enabled = false;
		}
	}
}
