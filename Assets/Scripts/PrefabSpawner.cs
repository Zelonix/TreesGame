using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

	public GameObject prefab;
	public float spawnChance;
	public int maxPrefabs;
	private List<GameObject> prefabs = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		prefabs.RemoveAll (item => item == null);
		if (Random.value < spawnChance && prefabs.Count < maxPrefabs) {
			GameObject instantiatedPrefab = Instantiate (prefab, transform.position, transform.rotation) as GameObject;
			prefabs.Add (instantiatedPrefab);
		}
	}
}
