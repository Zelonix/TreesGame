using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

	GameObject player;
	public Text healthText;
	public Text scoreText;
	public Text killsText;
	public Text magAmmoText;
	public Text gunAmmoText;
	public Text buyText;
    public Text gunNameText;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player1");
		killsText.enabled = false;
		buyText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			killsText.enabled = true;
		}

		healthText.text = "Health: " + player.GetComponent<PlayerScript> ().health.ToString();
		scoreText.text = player.GetComponent<PlayerScript> ().score.ToString();
		killsText.text = "Kills: " + player.GetComponent<PlayerScript> ().kills.ToString();
		magAmmoText.text = player.GetComponentInChildren<WeaponScript> ().magAmmo.ToString ();
		gunAmmoText.text = "/" + player.GetComponentInChildren<WeaponScript> ().gunAmmo.ToString ();
        gunNameText.text = player.GetComponentInChildren<WeaponScript>().gunName.ToString();
        

		if (Input.GetKeyUp (KeyCode.Tab)) {
			killsText.enabled = false;
		}
	}
}
