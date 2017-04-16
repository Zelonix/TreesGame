using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour {

	public int planksOnBarrier = 0;
	List<GameObject> planks = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.childCount; i++) {
			planks.Add (transform.GetChild (i).gameObject);
			planksOnBarrier++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RemovePlank() {
		Destroy (planks [transform.childCount - 1].gameObject);
		planks.RemoveAt (transform.childCount - 1);
		planksOnBarrier--;
	}
}
