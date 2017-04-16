using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralScript : MonoBehaviour {

	public GameObject mainCanvas;

	public AudioClip startRoundAudio;
	public AudioClip endRoundAudio;
	AudioSource audioSource;

	public int currentRound = 1;
	public int zombiesLeftToSpawn = 0;
	public int zombiesLeftToKill = 0;
	public int[] zombiesPerRound = new int[100];
	public int healthToSpawn = 150;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = startRoundAudio;
		audioSource.Play ();
		//mainCanvas = GameObject.Find ("Canvas");
		zombiesLeftToSpawn = zombiesPerRound [currentRound - 1];
		zombiesLeftToKill = zombiesLeftToSpawn;
	}
	
	// Update is called once per frame
	bool passingRound = false;
	void Update () {
		if (zombiesLeftToKill == 0 && !passingRound) {
			StartCoroutine (NextRound ());
		}
		if (Input.GetKey ("escape")) {
			Application.Quit ();
		}
	}

	IEnumerator NextRound() {
		passingRound = true;
		audioSource.clip = endRoundAudio;
		audioSource.Play ();
		yield return new WaitForSeconds (10);
		audioSource.clip = startRoundAudio;
		audioSource.Play ();
		currentRound++;
		mainCanvas.GetComponent<CanvasScript> ().roundText.text = "Round: " + currentRound.ToString();
		zombiesLeftToSpawn = zombiesPerRound [currentRound - 1];
		zombiesLeftToKill = zombiesLeftToSpawn;
		if (currentRound < 10)
			healthToSpawn += 100;
		else
			healthToSpawn = (int)(healthToSpawn * 1.1);
		passingRound = false;
	}
}
