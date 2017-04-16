using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour {

	NavMeshAgent agent;

    Animator animator;

	GameObject player;
	GameObject generalObject;
	public float speed = 5.0f;
	public float health = 100.0f;
	public float armReach = 1.0f;
	public float swipeTime = 1.0f;
	public float hitDamage = 1.0f;
    bool dead = false;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player1");
		generalObject = GameObject.Find ("General Game Object");
		agent = GetComponent<NavMeshAgent> ();
        agent.destination = player.transform.position;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	float swipeTimeStamp = 0.0f;
	bool isSwiping = false;
	void Update () {
        if (!dead) {
            float distanceToPlayer = agent.remainingDistance;
            animator.SetFloat("DistanceToPlayer", distanceToPlayer);
            agent.destination = player.transform.position;
            if (distanceToPlayer <= armReach && distanceToPlayer > 0.0f)
            {
				agent.isStopped = true;
                isSwiping = true;
                if (swipeTimeStamp + swipeTime < Time.time)
                {
                    player.GetComponent<PlayerScript>().gotHit(hitDamage);
                    swipeTimeStamp = Time.time;
                }
            }
            else if (isSwiping)
            {
				agent.isStopped = false;
            }
        }/*
		else {
			transform.localPosition += (distanceToPlayer.normalized * Time.deltaTime * speed);
			transform.rotation = Quaternion.LookRotation (distanceToPlayer.normalized);
		}*/
	}

	public void beenHit(float damage) {
		health -= damage;
		player.GetComponent<PlayerScript> ().score += 10;
		if (health <= 0.0f) {
			//recursiveChildren (gameObject);
			player.GetComponent<PlayerScript> ().score += 90;
			player.GetComponent<PlayerScript> ().kills += 1;
            dead = true;
			agent.isStopped = true;
            StartCoroutine(killZombie());
        }
	}

    IEnumerator killZombie() {
        //animator.SetBool("Dead", true);
		generalObject.GetComponent<GeneralScript>().zombiesLeftToKill--;
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<NavMeshAgent>());
        switch (Random.Range(0, 3)) {
            case 0:
                animator.Play("back_fall");
                break;
            case 1:
                animator.Play("right_fall");
                break;
            case 2:
                animator.Play("left_fall");
                break;
        }
        //transform.DetachChildren();
        yield return new WaitForSeconds(1.433f);
        yield return new WaitForSeconds(20.0f);
        Object.Destroy(this.gameObject);
    }

	IEnumerator BreakBarrier(Collider collider) {
		while (collider.GetComponent<BarrierScript> ().planksOnBarrier > 0) {
			agent.isStopped = true;
			collider.GetComponent<BarrierScript> ().RemovePlank ();
			if (collider.GetComponent<BarrierScript> ().planksOnBarrier > 0) {
				yield return new WaitForSeconds (3);
			}
		} 
		agent.isStopped = false;
		//yield return true;
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.name.StartsWith ("Barrier")) {
			StartCoroutine (BreakBarrier (collider));
		}
	}
	/*bool recursiveChildren(GameObject current) {
		for (int i = 0; i < current.transform.GetChildCount(); i++) {
			recursiveChildren(current.transform.GetChild(i).gameObject);
		}
		current.AddComponent<Rigidbody> ();
		//current.AddComponent<CapsuleCollider> ();
		current.transform.DetachChildren ();
		return true;
	}*/
}
