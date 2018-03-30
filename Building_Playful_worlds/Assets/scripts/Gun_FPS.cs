using UnityEngine;

public class Gun_FPS : MonoBehaviour {

	public float damage = 10.0f;
	public float range = 100.0f;
	public float FireRate = 5f;
	public float ImpactForce = 10f;

	//public int bulletsPerMag = 30; //Bullets per mag
	//public int bulletsLeft = 200; // Total bullets left
	//public int currentBullets; // current bullets in mag

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;

	Animator anim;

	private float nextTimeToFire = 5f;


	void Start(){
		//currentBullets = bulletsPerMag;
		anim = GetComponent<Animator>();
		anim.Play("PistolGrab");
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire) 
		{
			nextTimeToFire = Time.time + 1 / FireRate;
			Shoot();

		}
	}

	void Shoot()
	{
		//currentBullets--;
		muzzleFlash.Play();

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
			Destroy (impactGO, 2f);
		}
	}
}
