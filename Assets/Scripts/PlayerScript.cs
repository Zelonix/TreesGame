using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	AudioSource audioS;

	public float health;
	public float healthRecoverySpeed;
	public float healthRecoveryTime;
	public AudioClip hitSound;
	public float score;
	public int kills;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (health < 100.0f && hitTimeStamp + healthRecoveryTime < Time.time) {
			health += healthRecoverySpeed;
			health = Mathf.Clamp (health, 0.0f, 100.0f);
		}
	}

	float hitTimeStamp = 0.0f;
	public void gotHit(float damage) {
		health -= damage;
		hitTimeStamp = Time.time;
		if (health <= 0) {
			Application.Quit ();
		}
		audioS.clip = hitSound;
		audioS.Play ();
	}

}
