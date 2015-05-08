#region DESCRIPTION
/*===================
 * Reticule 2.0 v0.71
 *===================
 *
 *ChangeLog:
 *
 * 2015.MAY.07 (v0.71):
 * 	- attempting to replace debug Highlighting 
 * 	with object 'Grabbing'. Might incorporate a 
 * 	"pickup" script.
 * 2015.MAY.06 (v0.7);
 * 	- adding a timer and integrating Grabber.cs
 * 	functionality. If it becomes too unweildly,
 * 	then I'll consider breaking it out to it's own script.
 * 	- added code folding with regions
 * 2015.MAY.06 (v0.6):
 *	- attempting to add rotation control for reticule here,
 *		instead of external Rotator_Y script
 * 2015.MAY.06 (v0.5):
 	- adding external control for reticule 'lerp
 * 2015.MAY.02: cleaning up, 
 * 		moving reticule back to surface of object, 
 * 		adding new reticule shader
 * 2015.APR.29: adding lerping
 * 2015.APR.27: updating on macbook air
 * 	- apply this one to FPS controller
 *
 * 2015.APR.27: initial GIT commit

 */
#endregion

using UnityEngine;
using System.Collections;

public class Reticule_2 : MonoBehaviour {

	#region VARIABLES

	//DEBUG VARS
	public bool debugOn = false; // if this switch is on, all the debug messages will fire to the console
	
	// == STATIC VARS ==
	private Color myColor = new Color(0f, 1.0f, 00f, 0.75f);	//myColor
	private Vector3 origin = new Vector3(0,0,0);		//origin transform
	
	// == RAYCAST VARS ==
	public float rayLength = 100f;	//ray distance
	private RaycastHit myHit;	// store the hit location's data

	// == RETICULE VARS ==
	public GameObject reticuleHolder;		//Editor reference to "reticule_holder" object
	public GameObject reticule;		//Editor reference to actual reticule geometry (for spinning)
	public float reticuleSize = 1.0f;		//size of reticule in relation to imported geo (declaration)
	public float reticuleDepth = 0.5f;	//0.5 is halfway between hit point and cast point
	public float rotationSpeed = 100f;	//default reticule rotation speed (when highlighting Pickup object)
	//private float reticuleCurrentSpeed;	//stores the current rotation
	private Vector3 originalRotation;

	// == GRABBER VARS ==
	private Vector3 targetOriginalLocation;	// buffers original target location (updated per frame)
	public GameObject handObject;	// what is the hand object
	private Vector3 handLocation;	// vector3 position of hand object
	public float targetHoldTime;	// target hold time
	private float currentPickupHoldTime;	// buffer current hold time (for pickup)
	private float currentDropHoldTime;		// buffer current hold time (for drop)

	public bool handsFull = false;	//am i already holding something?
	public GameObject objectInHand;	//what am i holding?

	// == DEBUG VARS ==

	#endregion

	// Use this for initialization
	void Start () {

		if(debugOn == true) {
			Debug.Log ("Debug mode ON");
		}
		else {
			Debug.Log ("Debug mode OFF");
		}

		//set reticule size
		reticule.transform.localScale = new Vector3(reticuleSize, reticuleSize, reticuleSize);
		originalRotation = new Vector3 (270, 180,0);	//set default reticule rotation

		//initialize timers to zero
		currentPickupHoldTime = 0.0f;
		currentDropHoldTime = 0.0f;


		//Initialize Grabber variables
		handLocation = handObject.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		//visible ray
		Debug.DrawRay(this.transform.position, this.transform.forward * rayLength, myColor);

		//fire the lay-sers!
		if(Physics.Raycast (this.transform.position, this.transform.forward, out myHit, rayLength)) {

			if(debugOn == true) {
				Debug.Log ("Point: " + myHit.point + "Distance: " + myHit.distance + ", ObjectHit: " + myHit.collider);
			}

			// change the position of the reticule
			reticuleHolder.SetActive(true);
			//reticule.transform.position = myHit.point; //deprecated
			reticuleHolder.transform.LookAt(this.transform.position);

			// switch this on as a test
			reticuleHolder.transform.position = Vector3.Lerp(this.transform.position, myHit.point, reticuleDepth);

			//test to see if there's something in myHit
			if(myHit.collider.gameObject.tag == "Pickup"){

				spinReticule(rotationSpeed);
				//increase timer
				increasePickupTimer();
			}

			else if(myHit.collider.gameObject.tag == "DropArea" && handsFull == true) {
				spinReticule(rotationSpeed);
				increaseDropTimer();
				dropItem();
			}

			else {
				resetReticule();
				resetPickupTimer();
				resetDropTimer();
			}
		}

		else {
			//if raycast hits nothing, move this guy back to origin
			reticuleHolder.transform.position = origin;
			reticuleHolder.SetActive(false);	//disable reticuleHolder
			resetReticule();	//resets reticule rotation

			//resets all hold times
			currentPickupHoldTime = 0.0f;
			currentDropHoldTime = 0.0f;
		}

		//see if this works
		//testGrab();
		grabItem();
	}

	// == CUSTOM FUNCTIONS ==

	#region RETICULE_FUNCTIONS

	//spin the reticule at designated speed
	void spinReticule(float maxSpeed) {

		reticule.gameObject.transform.Rotate(Vector3.up, Time.deltaTime * maxSpeed);
	}

	//reset to default rotation
	void resetReticule() {
		reticule.gameObject.transform.localEulerAngles = originalRotation;
		//reset global speed
		//reticuleCurrentSpeed = 0.0f;
	}
	#endregion

	#region GRABBER_FUNCTIONS
	//== GRABBER FUNCTIONS ==

	void increasePickupTimer() {
		//count up the Pickup Timer
		currentPickupHoldTime += Time.deltaTime;
		Debug.Log ("currentPickupHoldTime: " + currentPickupHoldTime);	//visualize in console
	}

	void resetPickupTimer() {
		//reset Pickup timer to 0.0f
		currentPickupHoldTime = 0.0f;
	}

	void increaseDropTimer() {
		//count up the Drop timer
		currentDropHoldTime += Time.deltaTime;
		Debug.Log ("CurrentDropHoldTime: " + currentDropHoldTime);
	}

	void resetDropTimer() {
		//reset Drop timer to 0.0f
		currentDropHoldTime = 0.0f;
	}



	void testGrab() {
		if (currentPickupHoldTime >= targetHoldTime && myHit.collider.gameObject.tag == "Pickup") {
			Debug.Log ("Timer has been hit!");

			//if object isn't already highlighted, highlight the object we've been "hitting on"
			if(myHit.collider.gameObject.GetComponent<SimpleHighlight>().isHighlighted == false) {
				myHit.collider.gameObject.GetComponent<SimpleHighlight>().isHighlighted = true;
				resetPickupTimer();
			}
			//if the object is already highlighted, return it back to it's original shader
			else if (myHit.collider.gameObject.GetComponent<SimpleHighlight>().isHighlighted == true) {
				myHit.collider.gameObject.GetComponent<SimpleHighlight>().isHighlighted = false;
				resetPickupTimer();
			}
		}
	}

	void grabItem() {
		//if timer builds up to
		if (currentPickupHoldTime >= targetHoldTime && myHit.collider.gameObject.tag == "Pickup") {
			Debug.Log ("Timer has been hit!");
			
			if(handsFull == false) {
				//store current location of target pick up object
				targetOriginalLocation = myHit.collider.transform.position;

				// move current Pickup object to 'hand' position
				myHit.collider.transform.position = handObject.transform.position;

				// set parent to handLocation
				myHit.collider.transform.parent = handObject.transform;

				//set handsFull to 'true'
				handsFull = true;

				//update what's in my hand
				objectInHand = myHit.collider.gameObject;
				Debug.Log ("Grabbed " + objectInHand);

				resetDropTimer();
			}

			else if(handsFull == true) {
				//error message"
				Debug.Log ("Your hands are full! Drop your object before attempting to pickup another");
			}
		}
	}

	void dropItem() {

		if (currentDropHoldTime >= targetHoldTime && myHit.collider.gameObject.tag == "DropArea") {
			// message output
			Debug.Log ("Dropping the shit.");

			//return Picked up object to where you picked it up
			objectInHand.transform.position = targetOriginalLocation;

			// return parent to game world
			objectInHand.transform.parent = null;

			// nothing is in hand anymore
			objectInHand = null;

			//set 'hands full' to false
			handsFull = false;

			resetPickupTimer();	// to mitigate the flashing

		}
	}
	// -- END GRABBER FUNCTIONS --
	#endregion

	#region HELPER_FUNCTIONS
	//== HELPER FUNCTIONS ==
	//debug every thing
	void debugAll(GameObject obj) {
		Debug.Log ("GameObject:" + obj);
		Debug.Log ("Position: " + obj.transform.position);
		Debug.Log ("LocalPosition: " + obj.transform.localPosition);
		Debug.Log ("LocalRotation: " + obj.transform.localRotation);
		Debug.Log ("EulerAngles: " + obj.transform.eulerAngles);
		Debug.Log ("LocalEulerAngles: " + obj.transform.localEulerAngles);
		Debug.Log ("LocalScale: " + obj.transform.localScale);
	}

	//debug position
	void debugPosition(GameObject obj) {
		Debug.Log ("GameObject: " + obj + "Position: " + obj.transform.position);
	}

	//debug rotation
	void debugRotation(GameObject obj) {
		Debug.Log ("GameObject: " + obj + "LocalRotation: " + obj.transform.localRotation);
		Debug.Log ("EulerAngles: " + obj.transform.localEulerAngles);
	}

	//debug scale
	void debugScale (GameObject obj) {
		Debug.Log ("GameObject: " + obj + ", LocalScale: " + obj.transform.localScale);
	}
	// -- END HELPER FUNCTIONS --
	#endregion

	 















} // END OF SCRIPT