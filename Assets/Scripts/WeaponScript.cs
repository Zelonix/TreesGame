using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	//Gun variables
	public Vector3 posRelToPlayer;
	public Vector3 rotationRelToPlayer;
	public Vector3 aimPosition;
	public float aimSpeed = 0.0f;

	public AudioClip fireSound;
	public AudioClip reloadSound;

    public string gunName;

	public bool automatic = true;
	public float fireRate = 10.0f;
	public float reloadTime = 1.0f;

	public int maxGunAmmo = 240;
	public int gunAmmo = 240;
	public int maxMagAmmo = 30;
	public int magAmmo = 30;

	public float damage;
	public float accuracy;
	public GameObject projectile;
	private GameObject barrelEnd;
	AudioSource audio;

	private ParticleSystem particleSys;
	private Camera fpsCam;
	private Animator animator;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		fpsCam = GetComponentInParent<Camera> ();
		animator = GetComponent<Animator> ();
		barrelEnd = transform.Find ("BarrelEnd").gameObject;
		particleSys = barrelEnd.GetComponent<ParticleSystem> ();
		particleSys.Stop ();
	}

	float fireTimestamp = 0.0f;
	private bool reloading = false;
	public bool aiming = false;
	private bool semiShot = false;

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (1) && !reloading) {
			aiming = true;
			transform.localPosition = Vector3.Lerp (transform.localPosition, aimPosition, aimSpeed);
		} 
		else {
			aiming = false;
			transform.localPosition = Vector3.Lerp (transform.localPosition, posRelToPlayer, aimSpeed);
		}
			

		if (Input.GetMouseButton(0) && fireTimestamp+1.0f/fireRate < Time.time && magAmmo > 0 && !reloading && !semiShot) {

			magAmmo--;
			StartCoroutine ("ShotEffect");

			Quaternion fireVector = fpsCam.transform.rotation;
			fireVector.x += Random.Range (-accuracy, accuracy);
			fireVector.y += Random.Range (-accuracy, accuracy);
			fireVector.z += Random.Range (-accuracy, accuracy);

			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
			RaycastHit hit;

			//hit something
			if (Physics.Raycast (rayOrigin, fireVector * Vector3.forward, out hit)) {
				if (hit.collider.tag == "Zombie") {
					hit.collider.GetComponent<ZombieScript> ().beenHit (damage);
				}
				GameObject instantiatetedProjectile = Instantiate (projectile, barrelEnd.transform.position, fireVector) as GameObject;
				instantiatetedProjectile.transform.parent = this.transform;
				Vector3 temp = (hit.point - barrelEnd.transform.position).normalized;
				instantiatetedProjectile.GetComponent<Rigidbody> ().velocity = (hit.point - barrelEnd.transform.position).normalized * projectile.GetComponent<ProjectileScript> ().speed;
			} 
			else {
				GameObject instantiatetedProjectile = Instantiate (projectile, barrelEnd.transform.position, fireVector) as GameObject;
				instantiatetedProjectile.transform.parent = this.transform;
				instantiatetedProjectile.GetComponent<Rigidbody> ().velocity = fireVector * Vector3.forward * projectile.GetComponent<ProjectileScript> ().speed;
			}
				
			if (automatic == false) {
				semiShot = true;
			}

			if (magAmmo <= 0) {
				StartCoroutine ("Reload");
			}

			fireTimestamp = Time.time;
		}

		if (Input.GetMouseButtonUp (0)) {
			semiShot = false;
		}

		if (Input.GetKeyDown(KeyCode.R) && !reloading && magAmmo < maxMagAmmo) {
			StartCoroutine("Reload");
		}
	}

	IEnumerator Reload() {
		reloading = true;
		if (gunAmmo > 0) {
			aiming = false;
			animator.Play ("Reload", 0);
			animator.SetBool ("isReloading", true);
			audio.PlayOneShot (reloadSound, 0.7f);
			yield return new WaitForSeconds (reloadTime);
			int bullets = Mathf.Min (maxMagAmmo - magAmmo, gunAmmo);
			magAmmo += bullets;
			gunAmmo -= bullets;
		}
		reloading = false;
		animator.SetBool ("isReloading", false);
	}

	IEnumerator ShotEffect() {
		particleSys.Emit(10);
		audio.PlayOneShot (fireSound, 0.7f);
		yield return null;
	}

	void OnEnable() {
        reloading = false;
		if (magAmmo == 0) {
			StartCoroutine ("Reload");
		}
        particleSys.Stop();
    }
}
