//==================================
// 		RAYCAST POINTER
//==================================
/*
	Version 0.1 GIT, 2015.APR.23:
	- uses right mouse button press for activation, orients to the surface normal of the object.
 */


using UnityEngine;
using System.Collections;

public class Raycast_Laser : MonoBehaviour {

	//laser dot
	public GameObject laserDot;

	//seperate target object as a location orienter (experimental)
	public GameObject target;

	private RaycastHit myHit;

	public float raycastDist = 100f;

	// Update is called once per frame
	void FixedUpdate () {

		//press RMB to cast the object
		if (Input.GetButton ("Fire2")) {

			if (Physics.Raycast(transform.position, transform.forward, out myHit, raycastDist)) {
				print("Yes, I hit something hit-able by the name of " + myHit.collider.gameObject.name);

				laserDot.transform.position = myHit.point;
				//laserDot.transform.rotation = Quaternion.LookRotation(myHit.normal);
				laserDot.transform.rotation = Quaternion.LookRotation(myHit.normal);

				//debug rotation of target object to see if it's moving
				Debug.Log (target.transform.position);
			}


		}

		else { 
			//returns to world origin
			laserDot.transform.position = new Vector3(0,0,0);
		}
	}

	//CUSTOM FUNCTIONS
}
