using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

	int selectedWeapon = 0;
	int weaponCount = 0;
	public int maxWeaponAmount = 2;

	public List<GameObject> weaponsList = new List<GameObject> ();
	//public GameObject[] weaponsList = new GameObject[2];
	private List<GameObject> weaponsReal = new List<GameObject> ();
	//private GameObject[] weaponsReal;

	// Use this for initialization
	void Start () {

		/*for (int i = 0; i < weaponsList.Length; i++) {
			weaponsReal [i] = Instantiate(weaponsList[i]) as GameObject;
			weaponsReal [i].transform.parent = this.transform;
			weaponsReal [i].transform.localPosition = weaponsReal [i].GetComponent<WeaponScript> ().posRelToPlayer;
			weaponsReal [i].SetActive (false);
			weaponCount += 1;
		}*/
		weaponsReal.Add(Instantiate(weaponsList[0]) as GameObject);
		weaponsReal [0].transform.parent = this.transform;
		weaponsReal [0].transform.localPosition = weaponsReal [0].GetComponent<WeaponScript> ().posRelToPlayer;
		weaponCount += 1;
		weaponsReal [0].SetActive (true);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetAxis("Mouse ScrollWheel") > 0.01f) {
			cycleWeapon (-1);
		}
		if (Input.GetAxis("Mouse ScrollWheel") < -0.01f) {
			cycleWeapon (1);
		}
	}

	public bool AddWeapon(GameObject weapon, int price) {
		for (int i = 0; i < weaponsReal.Count; i++) {
			if (weaponsReal [i].name == weapon.name + "(Clone)") {
				if (weaponsReal [i].GetComponent<WeaponScript> ().gunAmmo == weaponsReal [i].GetComponent<WeaponScript> ().maxGunAmmo) {
					return false;
				}
				else if (GetComponentInParent<PlayerScript>().score >= price) {
					weaponsReal [i].GetComponent<WeaponScript> ().gunAmmo = weaponsReal [i].GetComponent<WeaponScript> ().maxGunAmmo;
					GetComponentInParent<PlayerScript> ().score -= price;
				}					
				return false;
			}
		}
		if (weaponCount == maxWeaponAmount && GetComponentInParent<PlayerScript>().score >= price) {
			Destroy (weaponsReal [selectedWeapon]);
			weaponsReal [selectedWeapon] = Instantiate (weapon) as GameObject;
			weaponsReal [selectedWeapon].transform.parent = this.transform;
			weaponsReal [selectedWeapon].transform.localPosition = weapon.GetComponent<WeaponScript> ().posRelToPlayer;
			weaponsReal [selectedWeapon].transform.localEulerAngles = weapon.GetComponent<WeaponScript> ().rotationRelToPlayer;
			GetComponentInParent<PlayerScript> ().score -= price;
		} 
		else if (GetComponentInParent<PlayerScript>().score >= price) {
			weaponsReal [selectedWeapon].SetActive (false);
			weaponsReal.Add (Instantiate (weapon) as GameObject);
			weaponCount++;
			selectedWeapon = weaponCount-1;
			weaponsReal [selectedWeapon].transform.parent = this.transform;
			weaponsReal [selectedWeapon].transform.localPosition = weapon.GetComponent<WeaponScript> ().posRelToPlayer;
			weaponsReal [selectedWeapon].transform.localEulerAngles = weapon.GetComponent<WeaponScript> ().rotationRelToPlayer;
			GetComponentInParent<PlayerScript> ().score -= price;
		}
		return true;
	}

	void cycleWeapon(int dir) {
		if (selectedWeapon + dir >= 0 && selectedWeapon + dir < weaponsReal.Count) {
			weaponsReal [selectedWeapon].SetActive (false);
			selectedWeapon += dir;
			weaponsReal [selectedWeapon].SetActive (true);
		}
	}
}
