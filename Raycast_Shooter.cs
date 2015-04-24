/*======================
 * Raycast Shooter v0.1
 * =====================
 * Initial GIT commit 2015.APR.23




 */

using UnityEngine;
using System.Collections;

public class Raycast_Shooter : MonoBehaviour {
	
	Color myColor = new Color(0.0f, 0.5f, 0.0f);
	float rayLength = 25.0f;
	
	RaycastHit myHit;	//reference to the hit object 

	// Update is called once per frame
	void Update () {
		
		GameObject whatWasHit;	//object hit
		
		Vector3 myForward = transform.TransformDirection(Vector3.forward) * rayLength;	//10 units forward
		Debug.DrawRay(transform.position, myForward, myColor);	//draw debug ray

		//click button raycast
		if (Input.GetButtonDown("Fire1")) {

			if (Physics.Raycast(transform.position, myForward, out myHit, rayLength)) {

				//debug lines of info
				Debug.Log ("I've hit " + myHit.collider.gameObject);
				whatWasHit = myHit.collider.gameObject;
				Debug.Log ("What was hit: " + whatWasHit);

				//find the enemy object that was hit
				GameObject enemy = myHit.collider.gameObject;

				//find damage script
				ColorChanger eh = enemy.GetComponent<ColorChanger>();

				//decrement health
				if (eh.health >= 0) {
					eh.health -= 10;

					//if health is less than zero, reset to zero.
					if (eh.health < 0) {
						eh.health = 0;
					}
				}



			}
		}


		
		
	}
}
