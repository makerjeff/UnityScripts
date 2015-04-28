/*===================
 * Reticule 2.0 v0.2
 *===================
 * 2015.APR.27: initial GIT commit
 * 2015.APR.27: updating on macbook air
 * apply this one to FPS controller

 */

using UnityEngine;
using System.Collections;

public class Reticule_2 : MonoBehaviour {

	// if this switch is on, all the debug messages will fire to the console
	public bool debugOn = false;


	// == static variables ==

	//FPS controller
	public GameObject reticule;
	

	//my color
	private Color myColor = new Color(0f, 1.0f, 00f, 0.75f);

	//origin transform
	private Vector3 origin = new Vector3(0,0,0);


	// == RAYCAST VARIABLES ==
	//ray distance
	public float rayLength = 100f;

	// store the hit location's data
	private RaycastHit myHit;
	
	// Use this for initialization
	void Start () {

		if(debugOn == true) {
			Debug.Log ("Debug mode ON");
		}
		else {
			Debug.Log ("Debug mode OFF");
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		//visible ray
		Debug.DrawRay(this.transform.position, this.transform.forward * rayLength, myColor);

		//fire the lay-sers!
		if(Physics.Raycast (this.transform.position, this.transform.forward, out myHit, rayLength)) {

			if(debugOn == true) {
				Debug.Log ("Point: " + myHit.point + "Distance: " + myHit.distance);
			}

			// change the position of the reticule
			reticule.SetActive(true);
			reticule.transform.position = myHit.point;
			reticule.transform.LookAt(this.transform.position);
		}
		else {
			//if raycast hits nothing, move this guy back to origin
			reticule.transform.position = origin;
			reticule.SetActive(false);
		}
	
	}
}