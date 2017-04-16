using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	Animator animator;
	GameObject player;
	public int price;
	bool inside = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player1");
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.B) && inside && player.GetComponent<PlayerScript>().score >= price) {
			player.GetComponent<PlayerScript> ().score -= price;
			StartCoroutine (OpenDoor ());
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.name == "Player1") {
			inside = true;
			GameObject.Find ("Canvas").GetComponent<CanvasScript> ().buyText.text = "Buy door for " + price.ToString();
			GameObject.Find ("Canvas").GetComponent<CanvasScript> ().buyText.enabled = true;
		}
	}
	void OnTriggerExit(Collider collider) {
		if (collider.name == "Player1") {
			inside = false;
			GameObject.Find ("Canvas").GetComponent<CanvasScript> ().buyText.enabled = false;
		}
	}

	IEnumerator OpenDoor() {
		animator.SetBool ("isOpen", true);
		animator.Play ("DoorOpen", 0);
		GameObject.Find ("Canvas").GetComponent<CanvasScript> ().buyText.enabled = false;
		GameObject.Destroy (GetComponent<BoxCollider> ());
		Destroy (GetComponent<DoorScript> ());
		yield return null;
	}
}
