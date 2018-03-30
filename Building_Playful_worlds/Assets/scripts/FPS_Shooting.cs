using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(AudioSource))]
public class FPS_Shooting : MonoBehaviour {

	public TimeManager timeManager;
	public float damage = 30.0f;
	public float range = 100.0f;
	public float FireRate = 15f;
	public float ImpactForce = 10f;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;
	private bool isReloading = false;
	private bool isAiming = false;

	public AudioClip GunShot;
	public AudioClip Reloading;

	public AudioClip Slo;
	public AudioClip Re;

	public int maxAmmo = 10;
	public int  currentAmmo;
	private float reloadTime = 1f;

	public GameObject AmmoDisplay;
	public bool ShowDisplay;


	private float nextTimeToFire = 0f;

	private AudioSource source;
	public Animator animator;
	private float Timer = 0f;


	//Testting
	private Vector3 originalPostion;
	public Vector3 aimPosition;
	public float aodSpeed = 8f;


	void Start(){

	//testing
		originalPostion = transform.localPosition;



		currentAmmo = maxAmmo;
		source = GetComponent<AudioSource> ();
		ShowDisplay = AmmoDisplay;
		AmmoDisplay.SetActive (true);

		animator.SetBool ("AimRifle", false);

	}

	void OnEnable(){
		isReloading = false;
		animator.SetBool ("Reloading", false);
	}

	// Update is called once per frame
	void Update () 
	{



		//Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		//Debug.DrawRay(transform.position, forward, Color.green);

		AmmoDisplay.GetComponent<Text> ().text = "Assalt Rifle:" + currentAmmo;

		if (isReloading)
			return;

		if(currentAmmo <=0)
		{
			StartCoroutine(Reload());
			return;
		}

		if (Input.GetMouseButton (1)) {
		///	Aiming ();
		} else {
			//animator.SetBool ("AimRifle", false);
		}



		if (Input.GetMouseButton (0) && Time.time >= nextTimeToFire) {
			source.PlayOneShot (GunShot);
			nextTimeToFire = Time.time + 1 / FireRate;
			Shoot ();
			animator.SetBool ("RifleShoot", true);
		} else {
			animator.SetBool ("RifleShoot", false);
		}

		if (Input.GetKeyDown(KeyCode.R)) 
		{
			StartCoroutine(Reload());
			return;
		} 

		if (Input.GetKeyDown(KeyCode.E) && Timer <= 0) 
		{
			//Timer--;
			source.PlayOneShot (Slo);
			BulletTime();
			Sound ();
		}

		AimDownSights ();
	}//End update


	private void AimDownSights()
	{
		if (Input.GetMouseButton (1) && !isReloading) {
			
			transform.localPosition = Vector3.Lerp (transform.localPosition, aimPosition, Time.deltaTime * aodSpeed);
			//animator.SetBool ("AimRifle", true);
		} else {
			transform.localPosition = Vector3.Lerp (transform.localPosition, originalPostion, Time.deltaTime * aodSpeed);
			//animator.SetBool ("AimRifle", false);
		}
	}


	void Sound(){
		StartCoroutine (TimeOn ());
			Debug.Log ("timer");
	}

	IEnumerator TimeOn(){
		Debug.Log ("TimeON");
		yield return new WaitForSeconds (0.2f);
		Debug.Log ("TimeOFF");
		source.PlayOneShot (Re);
	}


	void Aiming(){
		animator.SetBool ("AimRifle", true);

	}

	IEnumerator Reload()
	{
		isReloading = true;
		Debug.Log("Reloading....");
		source.PlayOneShot (Reloading);
		animator.SetBool ("Reloading", true);

		yield return new WaitForSeconds (reloadTime -.25f);
		animator.SetBool ("Reloading", false);
		yield return new WaitForSeconds (-.25f);

		currentAmmo = maxAmmo;
		isReloading = false;

	}

	void BulletTime(){
		timeManager.DoSlowMotion();
	}

	void Shoot()
	{
		
		//Debug.Log ("shot");
		currentAmmo--;
		//
		RaycastHit hit;
		if(Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
		{
			
			Debug.Log (hit.transform.name);

			Target target = hit.transform.GetComponent<Target> ();
			if (target != null) 
			{
				target.TakeDamege (damage);
			}

			if (hit.rigidbody != null) 
			{
				hit.rigidbody.AddForce (-hit.normal * ImpactForce);
			}

			GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, 0.2f);
		}
	}


}

