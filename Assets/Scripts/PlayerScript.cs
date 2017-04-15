using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float health;
	public float score;
	public int kills;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void gotHit(float damage) {
		health -= damage;
		if (health <= 0) {
			Application.Quit ();
		}
	}

}
