using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	public float speed = 1000.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(transform.position.x) > 400.0f || 
			Mathf.Abs(transform.position.y) > 100.0f || 
			Mathf.Abs(transform.position.z) > 400.0f)
			Object.Destroy (this.gameObject);
	}

	void OnTriggerEnter() {
		Object.Destroy (this.gameObject);
	}


}
