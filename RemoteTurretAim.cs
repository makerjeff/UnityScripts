/* ===========================
 *  Remote Turret Aiming v0.3
 * ===========================
 * 
 * Add this to your Camera. Remotely points designated turret
 * to the point you're aiming.
 * 
 * Change Log:
 * 2015.MAY.31 (v0.3);
 * 	- making it VR friendly (no instantiated bulletholes)
 * 	- removing animation references
 * 
 * 2015.MAY.30 (v0.2):
 * 	- modifying from MISO
 * 2015.MAY.29:
 * 	- Start of file
 */

using UnityEngine;
using System.Collections;

public class RemoteTurretAim : MonoBehaviour {

	//Turret Controller Vars
	public GameObject turretBase;	// lock all axis
	public GameObject y_Axis;		// lock x and z axis
	public GameObject Gun;			// lock y and Z axis

	//Turret Target Variables
	public GameObject targetObject;	//object to point at
	private Vector3 target;	//swap this later

	//Raycast Variables
	private Ray myRay;		//ray to shoot
	private RaycastHit myHit;	//hit to store info

	//Particle Effects Variables
	public GameObject gunEffect;	//gun effect particle system
	public GameObject gunBarrelTip; 	// where the gun barrel tip is
	public GameObject impact;	//holds impact particles GameObject to pull impact particles array from
	private ParticleSystem shootingParticles;	//shooting particles
	private ParticleSystem[] impactParticles;	//impact particles array

	//Bullethole Vars
	//public GameObject bulletHole;

	//Turret Animation Varibles
	//public GameObject animHolder;
	//private Animator anim;


	// Use this for initialization
	void Start () {
		Debug.Log (turretBase);
		Debug.Log (y_Axis);
		Debug.Log (Gun);

		shootingParticles = gunEffect.GetComponentInChildren<ParticleSystem>();
		impactParticles = impact.GetComponentsInChildren<ParticleSystem>();

		foreach(ParticleSystem part in impactParticles) {
			Debug.Log ("Impact Particle Smoke/Impact: " + part);
		}

		//anim = animHolder.GetComponent<Animator>();



	}
	
	// Update is called once per frame
	void Update () {


		castingRays();
		aimTurret();
		bangBang();


		if(Input.GetButtonDown("Fire1")) {
			shootingParticles.Play();
		}

	}

	#region CUSTOM FUNCTIONS

	void aimTurret() {
		
		//update target every frame
		//target = targetObject.transform.position;

		y_Axis.transform.LookAt (target);
		y_Axis.transform.eulerAngles = new Vector3(0,y_Axis.transform.eulerAngles.y,0);
		
		Gun.transform.LookAt(target);
		Gun.transform.eulerAngles = new Vector3(Gun.transform.eulerAngles.x, y_Axis.transform.eulerAngles.y, 0);
	}

	void castingRays () {
		
		//repeatedly update this ray
		myRay = new Ray(transform.position, transform.forward);
		
		if(Physics.Raycast(myRay, out myHit, 1000f)) {
			//Debug.Log (myHit.collider.gameObject.name);
			target = myHit.point;
		}
	}

	void bangBang() {
		myRay = new Ray(transform.position, transform.forward);

		if(Physics.Raycast (myRay, out myHit, 1000f)) {

			target = myHit.point;
			impact.transform.position = myHit.point;

			if(Input.GetButtonDown ("Fire1")) {
				foreach(ParticleSystem part in impactParticles) {
					part.Play();


					//Instantiate(bulletHole, myHit.point, Quaternion.FromToRotation(Vector3.up, myHit.normal));

					//anim.SetTrigger ("fireShot");
				}
			}

		}
	}

	#endregion
}
