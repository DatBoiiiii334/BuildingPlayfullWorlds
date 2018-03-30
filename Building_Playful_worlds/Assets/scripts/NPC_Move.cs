using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Move : MonoBehaviour {



	//public GameObject TheEnemy;
	//public int AttackTrigger;
	//public int IsAttacking;
	//public GameObject ScreenFlash;
	//public AudioSource Hurt01;
	//public AudioSource Hurt02;
	//public AudioSource Hurt03;
	//public int PainSound;



	public Transform Player;
	int MoveSpeed = 4;
	int MaxDist = 10;
	int MinDist = 5;




	void Start()
	{

	}

	void Update()
	{
		transform.LookAt(Player);

		if (Vector3.Distance(transform.position, Player.position) >= MinDist)
		{
			transform.position += transform.forward * MoveSpeed * Time.deltaTime;
			if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
			{
				//Here Call any function U want Like Shoot at here or something


			}

		}
	}

}
