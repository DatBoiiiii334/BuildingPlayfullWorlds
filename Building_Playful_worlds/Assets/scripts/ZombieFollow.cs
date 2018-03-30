using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollow : MonoBehaviour {

	public GameObject ThePlayer;


	//public GameObject nigga;

	public float TargetDistance ;
	public float AllowedRange = 100f;
	public GameObject TheEnemy;
	private float EnemySpeed = 0.02f;
	public int AttackTrigger;
	public RaycastHit Shot;

	public int IsAttacking;
	public GameObject ScreenFlash;
	public AudioSource Hurt01;
	public AudioSource Hurt02;
	public AudioSource Hurt03;
	public int PainSound;

	void Start(){
		gameObject.tag = "Player";
	}


	void Update(){
		transform.LookAt (ThePlayer.transform);
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out Shot)) 
		{
			TargetDistance = Shot.distance;
			if (TargetDistance < AllowedRange) {
				EnemySpeed = 0.07f;
				if (AttackTrigger == 0) {
					TheEnemy.GetComponent<Animation> ().Play ("Walking");
					transform.position = Vector3.MoveTowards (transform.position, ThePlayer.transform.position, EnemySpeed);
				}
			} else {
				EnemySpeed = 0;
				TheEnemy.GetComponent<Animation> ().Play ("Idle");
			}
		}
		if (AttackTrigger == 1) 
		{
			if (IsAttacking == 0) 
			{
				StartCoroutine (EnemyDamage ());
			}
			EnemySpeed = 0;
			TheEnemy.GetComponent<Animation> ().Play ("Attacking");
		}
	}

	void OnTriggerEnter(){
		AttackTrigger = 1;
	}

	void OnTriggerExit(){
		AttackTrigger = 0;
	}

	IEnumerator EnemyDamage(){
		IsAttacking = 1;
		PainSound = Random.Range (1, 4);
		yield return new WaitForSeconds (0.9f);
		ScreenFlash.SetActive (true);
		GlobalHealth.PlayerHealth -= 1;
		if (PainSound == 1) {
			Hurt01.Play ();
		}
		if (PainSound == 2) {
			Hurt02.Play ();
		}
		if (PainSound == 3) {
			Hurt03.Play ();
		}

		yield return new WaitForSeconds (0.05f);
		ScreenFlash.SetActive (false);
		yield return new WaitForSeconds (1);
		IsAttacking = 0;
	}
}
